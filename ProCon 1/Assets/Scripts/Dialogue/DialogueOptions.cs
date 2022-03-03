using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueOptions", menuName = "ScriptableObjects/DialogueOptions", order = 0)]
public class DialogueOptions : ScriptableObject
{
    public List<string> dialogueList = new List<string>();
    public List<string> responseOptions = new List<string>();
    public int inputBreak;
    public List<DialogueOptions> nextDialogueOptions = new List<DialogueOptions>();

}
