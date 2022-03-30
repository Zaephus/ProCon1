using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    public Unit playerUnit;
    public List<EnemyUnit> enemyUnits = new List<EnemyUnit>();

    #region Singleton
    public static LevelLoader instance;

    void Awake() {
        
        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    #endregion

    public float transitionTime = 2f;

    public void LoadLevel(string levelName) {
        StartCoroutine(LoadNamedLevel(levelName));
    }

    public IEnumerator LoadNamedLevel(string levelName) {

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

    public void StartDialogue(Unit unit) {
        SaveSystem.instance.SaveUnit(unit,"CurrentUnit");
        LoadLevel("DialogueScene");
    }

    public void StartBattle(Unit unit) {
        SaveSystem.instance.SaveUnit(unit,"CurrentUnit");
        LoadLevel("BattleScene");
    }

    public void StartGame() {

        PlayerPrefs.SetInt("FrikandelBroodjes",0);

        playerUnit.lastPosX = playerUnit.startPosX;
        playerUnit.lastPosY = playerUnit.startPosY;
        SaveSystem.instance.SaveUnit(playerUnit,playerUnit.name);

        foreach(EnemyUnit unit in enemyUnits) {

            unit.lastPosX = unit.startPosX;
            unit.lastPosY = unit.startPosY;
            SaveSystem.instance.SaveUnit(unit,unit.name);

            if(unit.unitName == "Intimi-NATOR") {
                unit.spawnInAltWorld = true;
            }

        }


        LoadLevel("LevelOneScene");
    }

}