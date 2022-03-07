using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : MonoBehaviour {

    public int maxHealth = 100;
    public int Health {
        get;
        set;
    }

    public EnemyBattleController enemy;

    public List<Ability> abilities = new List<Ability>();

    public void Start() {

        Health = maxHealth;

        foreach(Ability ability in abilities) {
            ability.Initialize(this,enemy);
        }

    }

    public void Update() {

        foreach(Ability ability in abilities) {
            ability.OnUpdate();
        }

    }

    public void TakeDamage(int dmg) {

        Health -= dmg;

        if(Health <= 0) {
            Die();
        }

    }

    public void Die() {
        Debug.Log("You died!!");
    }

}