using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordAbility",menuName = "Abilities/SwordAbility")]
public class Attack : Ability {

    // private BattleSystem battleSystem;

    // public override void Initialize(BattleSystem bSystem) {
    //     battleSystem = bSystem;
    // }

    // public IEnumerator StartAbility() {

    //     battleSystem.dialogueText.text = "The attack is succesful!";
        
    //     yield return new WaitForSeconds(2f);

    //     bool isDead = battleSystem.enemyUnit.TakeDamage(battleSystem.playerUnit.currentAttack);
    //     battleSystem.enemyHUD.SetHealth(battleSystem.enemyUnit.currentHealth);

    //     if(isDead) {
    //         battleSystem.state = BattleState.Won;
    //         battleSystem.StartCoroutine(battleSystem.EndBattle());
    //     }
    //     else {
    //         battleSystem.state = BattleState.EnemyTurn;
    //         battleSystem.StartCoroutine(battleSystem.EnemyTurn());
    //     }

    // }

    // public void OnButtonPressed() {

    //     if(battleSystem.state != BattleState.PlayerTurn) {
    //         return;
    //     }

    //     battleSystem.state = BattleState.Wait;
    //     StartCoroutine(StartAbility());

    // }

}
