using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOptions", menuName = "DialogueOptions")]
public class DialogueOptions : ScriptableObject
{
    public List<string> dialogueList = new List<string>();
    public List<string> responseOptions = new List<string>();
    public int[] chosenAmount;
    public int inputBreak;
    public int combatBreak = -1;
    public int endBreak = -1;

    public int permamentEndBreak = -1;
    public bool spawnInAltWorld = false;

    public int broodjesToGive = 0;
    public List<DialogueOptions> nextDialogueOptions = new List<DialogueOptions>();

}

[System.Serializable]
public class DialogueSaver {
    public int[] chosenAmount;

}
