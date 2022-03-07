using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAbility",menuName = "Abilities/Attack")]
public class Attack : Ability {

    private PlayerBattleController player;
    private EnemyBattleController enemy;

    private string attacker;

    public override void Initialize(PlayerBattleController p,EnemyBattleController e) {
        
        player = p;
        enemy = e;
        
        attacker = "player";

    }
    public override void Initialize(EnemyBattleController e,PlayerBattleController p) {
        
        enemy = e;
        player = p;

        attacker = "enemy";

    }

    public override void OnUpdate() {

        if(attacker == "player") {
            if(Input.GetKeyDown("g")) {
                enemy.TakeDamage(10);
            }
        }
        if(attacker == "enemy") {
            if(Input.GetKeyDown("h")) {
                player.TakeDamage(10);
            }
        }
    }

}
