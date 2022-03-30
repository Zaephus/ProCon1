using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyUnit",menuName = "Unit/EnemyUnit")]
public class EnemyUnit : Unit {

    public DialogueOptions startDialogueOption;

    public int option = 0;
    public DialogueOptions firstAlternateDialogueOption;
    public DialogueOptions secondAlternateDialogueOption;

    public DialogueOptions firstAltWorldDialogueOption;
    public DialogueOptions firstMainWorldDialogueOption;
    public DialogueOptions secondAltWorldDialogueOption;
    public DialogueOptions secondMainWorldDialogueOption;

    public DialogueOptions battleDialogueOption;

    public bool inAltWorld = false;
    public bool spawnInAltWorld = false;

    public GameObject unitPrefab;
    
}