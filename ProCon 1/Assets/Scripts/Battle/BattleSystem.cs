using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using TMPro;

public enum BattleState {Start,PlayerTurn,EnemyTurn,Wait,Won,Lost}

public class BattleSystem : MonoBehaviour {

    public GameObject playerPrefab;
    private GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

    public PlayerUnit playerUnit;
    private EnemyUnit currentUnit;
	public List<EnemyUnit> units = new List<EnemyUnit>();

    public Text dialogueText;

    public GameObject combatButtons;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public void Start() {

        Unit tempUnit = new Unit();
		SaveSystem.instance.LoadUnit(tempUnit,"CurrentUnit");

		foreach(EnemyUnit unit in units) {
			if(unit.unitName == tempUnit.unitName) {
				currentUnit = unit;
			}
		}

        enemyPrefab = currentUnit.unitPrefab;

        state = BattleState.Start;
        StartCoroutine(SetupBattle());

    }

    public void Update() {

        if(state != BattleState.PlayerTurn) {
            combatButtons.SetActive(false);
        }
        else {
            combatButtons.SetActive(true);
        }

    }

    public IEnumerator SetupBattle() {

        GameObject player = Instantiate(playerPrefab,playerPosition.position,Quaternion.identity,this.transform);

        GameObject enemy = Instantiate(enemyPrefab,enemyPosition.position,Quaternion.identity,this.transform);

        dialogueText.text = currentUnit.unitName + " staat in de weg!!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(currentUnit);

        yield return new WaitForSeconds(1f);

        state  = BattleState.Wait;
        StartCoroutine(PlayerTurn());

    }

    public IEnumerator EndBattle() {

        if(state == BattleState.Won) {
            dialogueText.text = "Je hebt " + currentUnit.unitName + " verslagen!!";
            yield return new WaitForSeconds(2f);
            SaveSystem.instance.SaveUnit(currentUnit,currentUnit.name);
            currentUnit.spawnInAltWorld = false;
            SaveSystem.instance.SaveUnit(playerUnit,playerUnit.name);
            LevelLoader.instance.LoadLevel("LevelOneScene");
        }
        else if(state == BattleState.Lost) {
            dialogueText.text = currentUnit.unitName + " heeft jou verslagen...";
            yield return new WaitForSeconds(2f);
            SaveSystem.instance.SaveUnit(currentUnit,currentUnit.name);
            SaveSystem.instance.SaveUnit(playerUnit,playerUnit.name);
            LevelLoader.instance.LoadLevel("GameOverScene");
        }
        
    }

    public IEnumerator EnemyTurn() {

        yield return new WaitForSeconds(1f);

        dialogueText.text = currentUnit.unitName + " probeert je te intimideren!";

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(currentUnit.currentAttack);
        playerHUD.SetHealth(playerUnit.currentHealth);

        if(isDead) {
            state = BattleState.Lost;
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.PlayerTurn;
            StartCoroutine(PlayerTurn());
        }
    }

    public IEnumerator PlayerTurn() {

        yield return new WaitForSeconds(1f);

        dialogueText.text = "Kies een actie:";
        state = BattleState.PlayerTurn;

    }

    public IEnumerator PlayerKernWaardeCombo() {

        dialogueText.text = "De tegenstanders wil om tegen te werken verminderd";
        
        yield return new WaitForSeconds(2f);

        bool isDead = currentUnit.TakeDamage(playerUnit.currentAttack);
        enemyHUD.SetHealth(currentUnit.currentHealth);

        if(isDead) {
            state = BattleState.Won;
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }

    }

    public IEnumerator PlayerInspirerendeToespraak() {

        dialogueText.text = "Je geeft een inspirerende toespraak!";

        yield return new WaitForSeconds(2f);

        currentUnit.currentDefense = (int)((float)currentUnit.currentDefense*0.9f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerEenHeleHoopLeren() {

        dialogueText.text = "Je krijgt meer inzicht in de situatie";

        yield return new WaitForSeconds(2f);

        playerUnit.currentAttack = (int)((float)playerUnit.currentAttack*1.1f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerOpenHouding() {

        dialogueText.text = "Je neemt een open houding aan ter voorbereiding!";

        yield return new WaitForSeconds(2f);

        playerUnit.currentDefense = (int)((float)playerUnit.currentDefense*1.1f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerItems() {

        dialogueText.text = "Je eet een 'frikandelbroodje' en je wilskracht gaat omhoog met 50!";

        yield return new WaitForSeconds(2f);

        if(playerUnit.currentHealth <= playerUnit.maxHealth) {
            playerUnit.Heal(50);
            playerHUD.SetHealth(playerUnit.currentHealth);
            StartCoroutine(EnemyTurn());
        }
        else {
            dialogueText.text = "Je wilskracht is al vol!";
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(PlayerTurn());
        }

    }

    public void OnKernWaardeComboButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerKernWaardeCombo());

    }

    public void OnInspirerendeToespraakButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerInspirerendeToespraak());

    }

    public void OnEenHeleHoopLerenButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerEenHeleHoopLeren());

    }

    public void OnOpenHoudingButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerOpenHouding());

    }

    public void OnItemsButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerItems());

    }

}