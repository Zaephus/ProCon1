using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUnit",menuName = "Unit/PlayerUnit")]
public class PlayerUnit : Unit {

    public void Heal(int amount) {

        currentHealth += amount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

    }

}