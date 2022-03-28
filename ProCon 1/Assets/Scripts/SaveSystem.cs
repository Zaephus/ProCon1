using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour {

    public string path;

    #region Singleton
    public static SaveSystem instance;

    void Awake() {
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    #endregion

    public void SetPath(string fileName) {
        if(Application.isEditor) {
            path = Application.dataPath + "/SaveData/" + fileName + ".txt";
        }
        else {
            path = Application.persistentDataPath + "/SaveData/" + fileName + ".txt";
        }
    }

    public void SaveDialogueOption(DialogueOptions option,string fileName) {

        SetPath(fileName);

        DialogueSaver targetOption = new DialogueSaver();

        targetOption.chosenAmount = option.chosenAmount;

        StreamWriter writer = new StreamWriter(path,false);

        writer.WriteLine(JsonUtility.ToJson(targetOption,true));
        writer.Close();
        writer.Dispose();

    }

    public void SaveUnit(Unit unit,string fileName) {

        SetPath(fileName);

        UnitSaver targetUnit = new UnitSaver();

        targetUnit.unitName = unit.unitName;

        targetUnit.maxHealth = unit.maxHealth;
        targetUnit.currentHealth = unit.currentHealth;

        targetUnit.baseAttack = unit.baseAttack;
        targetUnit.currentAttack = unit.currentAttack;

        targetUnit.baseDefense = unit.baseDefense;
        targetUnit.currentDefense = unit.currentDefense;

        targetUnit.lastPosX = unit.lastPosX;
        targetUnit.lastPosY = unit.lastPosY;

        StreamWriter writer = new StreamWriter(path,false);

        writer.WriteLine(JsonUtility.ToJson(targetUnit,true));
        writer.Close();
        writer.Dispose();

    }

    public void LoadDialogueOption(DialogueOptions option,string fileName) {

        SetPath(fileName);

        if(File.Exists(path)) {

            StreamReader reader = new StreamReader(path);

            DialogueSaver tempOption = JsonUtility.FromJson<DialogueSaver>(reader.ReadToEnd());

            option.chosenAmount = tempOption.chosenAmount;

            reader.Close();
            reader.Dispose();

        }
        
    }

    public void LoadUnit(Unit unit,string fileName) {
            
        SetPath(fileName);

        if(File.Exists(path)) {

            StreamReader reader = new StreamReader(path);

            UnitSaver targetUnit = JsonUtility.FromJson<UnitSaver>(reader.ReadToEnd());

            unit.unitName = targetUnit.unitName;

            unit.maxHealth = targetUnit.maxHealth;
            unit.currentHealth = targetUnit.currentHealth;

            unit.baseAttack = targetUnit.baseAttack;
            unit.currentAttack = targetUnit.currentAttack;

            unit.baseDefense = targetUnit.baseDefense;
            unit.currentDefense = targetUnit.currentDefense;

            unit.lastPosX = targetUnit.lastPosX;
            unit.lastPosY = targetUnit.lastPosY;

            reader.Close();
            reader.Dispose();

        }
        
    }

}