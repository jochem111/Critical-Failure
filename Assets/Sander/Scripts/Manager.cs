using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager;

    [Header ("Minigame Managers")]
    public PlumbingManager plumbingManager;
    public DrinkGameManager drinkGameManager;
    public PoemMinigame poemMinigame;
    public ScreenChange shoeManager;

    [Header("Ui")]
    public ButtonManager buttonManager;
    public DialogueManager dialogueManager;
    public PlumbingUI plumbingUI;
    public DrinkUi drinkUi;

    [Header("Scene")]
    public MinigameStarter minigameStarter;
    public TavernManager tavernManager;
    public StarManager starManager;
    public FadeManager fadeManager;
    public Interact interact;

    private void Start()
    {
        if (manager != null)
        {
            if (manager != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            manager = this;
        }
    }
}
