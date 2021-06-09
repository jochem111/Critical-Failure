using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    MinigameStarter.minigameNames minigameToStartName;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI goodResponseText;
    public TextMeshProUGUI meanResponseText;

    public GameObject dialogueBox;
    public GameObject responses;
    public GameObject goodExit;
    public GameObject badExit;
    public GameObject suddenExit;

    private Queue<string> goodSentences;
    private Queue<string> meanSentences;
    private Queue<string> suddenSentences;
    private Queue<string> goodResponses;
    private Queue<string> meanResponses;

    public float textSpeed;
    public float time;

    private void Start()
    {
        goodSentences = new Queue<string>();
        meanSentences = new Queue<string>();
        suddenSentences = new Queue<string>();
        goodResponses = new Queue<string>();
        meanResponses = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        minigameToStartName = dialogue.minigameToStartName;
        // dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        goodSentences.Clear();
        meanSentences.Clear();
        suddenSentences.Clear();
        goodResponses.Clear();
        meanResponses.Clear();

        foreach (string goodSentence in dialogue.goodSentences)
        {
            goodSentences.Enqueue(goodSentence);
        }
        foreach (string meanSentence in dialogue.meanSentences)
        {
            meanSentences.Enqueue(meanSentence);
        }
        foreach (string suddenSentence in dialogue.suddenSentences)
        {
            suddenSentences.Enqueue(suddenSentence);
        }
        foreach (string goodResponse in dialogue.goodResponses)
        {
            goodResponses.Enqueue(goodResponse);
        }
        foreach (string meanResponse in dialogue.meanResponses)
        {
            meanResponses.Enqueue(meanResponse);
        }

        responses.SetActive(true);
        goodExit.SetActive(false);
        badExit.SetActive(false);
        suddenExit.SetActive(false);

        DisplayGoodSentence();
    }

    public void DisplayGoodSentence()
    {
        if (goodSentences.Count == 0)
        {
            FinishDialogue();
            /* if (minigameToStartName != MinigameStarter.minigameNames.None)
            {
            } */
            Manager.manager.minigameStarter.StartNamedMinigame(minigameToStartName);
            return;
        }

        string gSentence = goodSentences.Dequeue();
        dialogueText.text = gSentence;

        if (goodResponses.Count == 0)
        {
            responses.SetActive(false);
            goodExit.SetActive(true);
            return;
        }

        string gResponse = goodResponses.Dequeue();
        goodResponseText.text = gResponse;
        string mResponse = meanResponses.Dequeue();
        meanResponseText.text = mResponse;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(gSentence));

        Debug.Log("Good Sentence Displayed");
    }

    public void DisplayMeanSentence()
    {
        string mSentence = meanSentences.Dequeue();
        dialogueText.text = mSentence;
        responses.SetActive(false);
        badExit.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(mSentence));

        Debug.Log("Bad Sentence Displayed");
    }

    public void DisplaySuddenSentence()
    {
        string eSentence = suddenSentences.Dequeue();
        dialogueText.text = eSentence;
        responses.SetActive(false);
        suddenExit.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(eSentence));

        Debug.Log("Sudden Sentence Displayed");
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return textSpeed * Time.deltaTime;
        }

        Debug.Log("Sentence Typed");
    }

    //The below 3 voids are called regardless of whether the convo has more or the same amount of sentences as it has responses, so put stuff in there if you want it to happen in both.

    public void AbbruptlyEndDailogue()
    {
        dialogueBox.SetActive(false);

        Manager.manager.interact.FinishInteraction();

        Debug.Log("Sudden End of Conversation");
    }

    public void BadEndDialogue()
    {
        dialogueBox.SetActive(false);

        Manager.manager.interact.FinishInteraction();

        Debug.Log("Bad End of Conversation");
    }

    public void FinishDialogue()
    {
        dialogueBox.SetActive(false);
        Manager.manager.interact.FinishInteraction();
        Debug.Log("Good End of Conversation");
    }
}
