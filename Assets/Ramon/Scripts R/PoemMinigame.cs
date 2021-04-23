using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PoemMinigame : MonoBehaviour
{
    public GameObject poem;
    public GameObject exit;
    public bool inExitScreen;

    void Start()
    {
        inExitScreen = false;
    }

    void Update()
    {
        ExitPoemMinigame();
    }

    public void ExitPoemMinigame()
    {
        if (inExitScreen == false)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                inExitScreen = true;
                poem.SetActive(poem.activeSelf);
                exit.SetActive(exit.activeSelf);
            }
        }
    }

    public void YesExitButton()
    {
        exit.SetActive(exit.activeSelf);
        poem.SetActive(poem.activeSelf);
        inExitScreen = false;
        SceneManager.LoadScene("Test_Ramon");
    }

    public void NoExitButton()
    {
        exit.SetActive(exit.activeSelf);
        poem.SetActive(poem.activeSelf);
        inExitScreen = false;
    }
}