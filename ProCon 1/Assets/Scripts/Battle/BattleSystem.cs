using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState {Start,PlayerTurn,EnemyTurn,Wait,Won,Lost}

public class BattleSystem : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

    public PlayerUnit playerUnit;
    public EnemyUnit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    public void Start() {

        state = BattleState.Start;
        StartCoroutine(SetupBattle());

    }

    public IEnumerator SetupBattle() {

        GameObject player = Instantiate(playerPrefab,playerPosition.position,Quaternion.identity,this.transform);

        GameObject enemy = Instantiate(enemyPrefab,enemyPosition.position,Quaternion.identity,this.transform);

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches!!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state  = BattleState.Wait;
        StartCoroutine(PlayerTurn());

    }

    public IEnumerator PlayerTurn() {

        yield return new WaitForSeconds(1f);

        dialogueText.text = "Choose an action:";
        state = BattleState.PlayerTurn;

    }

    public IEnumerator PlayerTeachingsOfOpenness() {

        dialogueText.text = "You teach " + enemyUnit.name +" a thing or two about openness!";
        
        yield return new WaitForSeconds(2f);

        bool isDead = enemyUnit.TakeDamage(playerUnit.currentAttack);
        enemyHUD.SetHealth(enemyUnit.currentHealth);

        if(isDead) {
            state = BattleState.Won;
            StartCoroutine(EndBattle());
        }
        else {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }

    }

    public IEnumerator PlayerInspiringSpeech() {

        dialogueText.text = "You give an inspiring presentation, intimidating the enemy!";

        yield return new WaitForSeconds(2f);

        enemyUnit.currentDefense = (int)((float)enemyUnit.currentDefense*0.9f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerLearningALot() {

        dialogueText.text = "You have gained insight, making you more effective!";

        yield return new WaitForSeconds(2f);

        playerUnit.currentAttack = (int)((float)playerUnit.currentAttack*1.1f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerSelfAssurance() {

        dialogueText.text = "You remind yourself of your strengths, making you stronger!";

        yield return new WaitForSeconds(2f);

        playerUnit.currentDefense = (int)((float)playerUnit.currentDefense*1.1f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator PlayerItems() {

        dialogueText.text = "You eat a 'frikandelbroodje', which increases your health by 50!";

        yield return new WaitForSeconds(2f);

        playerUnit.Heal(50);
        playerHUD.SetHealth(playerUnit.currentHealth);

        StartCoroutine(EnemyTurn());

    }

    public IEnumerator EnemyTurn() {

        yield return new WaitForSeconds(1f);

        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.currentAttack);
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

    public IEnumerator EndBattle() {

        if(state == BattleState.Won) {
            dialogueText.text = "You won the battle!";
            yield return new WaitForSeconds(2f);
            LevelLoader.instance.LoadLevel("LevelOneScene");
        }
        else if(state == BattleState.Lost) {
            dialogueText.text = "You lost.";
            yield return new WaitForSeconds(2f);
            LevelLoader.instance.LoadLevel("GameOverScene");
        }
        
    }

    public void OnTeachingsOfOpennessButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerTeachingsOfOpenness());

    }

    public void OnInspiringSpeechButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerInspiringSpeech());

    }

    public void OnLearningALotButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerLearningALot());

    }

    public void OnSelfAssuranceButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerSelfAssurance());

    }

    public void OnItemsButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        state = BattleState.Wait;

        StartCoroutine(PlayerItems());

    }

}