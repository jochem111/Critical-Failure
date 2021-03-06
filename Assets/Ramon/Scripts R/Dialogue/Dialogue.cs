using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public MinigameStarter.MinigameNames minigameToStartName;

    [TextArea(3, 10)]
    public string[] goodSentences;

    [TextArea(3, 10)]
    public string[] suddenSentences;

    [TextArea(3, 10)]
    public string[] goodResponses;

    [Space] public bool enableInteract;
}
