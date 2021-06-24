using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    MinigameStarter.MinigameNames minigameToStartName;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI goodResponseText;

    public GameObject dialogueBox;
    public GameObject responses;
    public GameObject goodExit;
    public GameObject suddenExit;

    private Queue<string> goodSentences;
    private Queue<string> suddenSentences;
    private Queue<string> goodResponses;

    public float textSpeed;
    public float time;

    GameObject customer;
    bool enableInteractable;

    private void Start()
    {
        goodSentences = new Queue<string>();
        suddenSentences = new Queue<string>();
        goodResponses = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, GameObject interactingWith)
    {
        customer = interactingWith;
        enableInteractable = dialogue.enableInteract;

        Debug.Log("Starting conversation with " + dialogue.name);

        minigameToStartName = dialogue.minigameToStartName;
        // dialogueBox.SetActive(true);

        nameText.text = dialogue.name;

        goodSentences.Clear();
        suddenSentences.Clear();
        goodResponses.Clear();

        foreach (string goodSentence in dialogue.goodSentences)
        {
            goodSentences.Enqueue(goodSentence);
        }
        foreach (string suddenSentence in dialogue.suddenSentences)
        {
            suddenSentences.Enqueue(suddenSentence);
        }
        foreach (string goodResponse in dialogue.goodResponses)
        {
            goodResponses.Enqueue(goodResponse);
        }

        responses.SetActive(true);
        goodExit.SetActive(false);
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

        StopAllCoroutines();
        StartCoroutine(TypeSentence(gSentence));

        Debug.Log("Good Sentence Displayed");
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

    //The below 2 voids are called regardless of whether the convo has more or the same amount of sentences as it has responses, so put stuff in there if you want it to happen in both.

    public void AbbruptlyEndDailogue()
    {
        dialogueBox.SetActive(false);

        Manager.manager.interact.FinishInteraction();
        Manager.manager.interact.EnableInteractText(customer);

        Manager.manager.musicManager.InTavernTrack(true);

        Debug.Log("Sudden End of Conversation");
    }

    public void FinishDialogue()
    {
        if (enableInteractable)
            Manager.manager.tavernManager.EnableInteractOnCustomers();

        customer.tag = "Untagged";
        Manager.manager.interact.GetInteractables();

        Debug.Log(minigameToStartName.ToString());

        if (minigameToStartName.ToString() == "None" | minigameToStartName.ToString() == "OpenDoor")
            Manager.manager.musicManager.InTavernTrack(true);
        else
            Manager.manager.musicManager.MinigameTrack(true);

        dialogueBox.SetActive(false);

        Debug.Log("Good End of Conversation");
    }
}
