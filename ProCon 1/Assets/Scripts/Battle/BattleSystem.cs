using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState {Start,PlayerTurn,EnemyTurn,Won,Lost}

public class BattleSystem : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

    Unit playerUnit;
    Unit enemyUnit;

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
        playerUnit = player.GetComponent<Unit>();

        GameObject enemy = Instantiate(enemyPrefab,enemyPosition.position,Quaternion.identity,this.transform);
        enemyUnit = enemy.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches!!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state  = BattleState.PlayerTurn;
        PlayerTurn();

    }

    public void PlayerTurn() {

        dialogueText.text = "Choose an action:";

    }

    public IEnumerator PlayerAttack() {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHealth(enemyUnit.currentHP);
        dialogueText.text = "The attack is succesful!";
        
        yield return new WaitForSeconds(2f);

        if(isDead) {
            state = BattleState.Won;
            EndBattle();
        }
        else {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }

    }

    public IEnumerator PlayerHeal() {

        playerUnit.Heal(5);

        playerHUD.SetHealth(playerUnit.currentHP);
        dialogueText.text = "You healed by 5 points!";

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());
        
    }

    public IEnumerator EnemyTurn() {

        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHealth(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if(isDead) {
            state = BattleState.Lost;
            EndBattle();
        }
        else {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    void EndBattle() {

        if(state == BattleState.Won) {
            dialogueText.text = "You won the battle!";
        }
        else if(state == BattleState.Lost) {
            dialogueText.text = "You lost.";
        }
        
    }
    public void OnAttackButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        StartCoroutine(PlayerAttack());

    }

    public void OnHealButton() {

        if(state != BattleState.PlayerTurn) {
            return;
        }

        StartCoroutine(PlayerHeal());

    }

}