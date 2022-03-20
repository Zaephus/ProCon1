using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUnit",menuName = "Unit/PlayerUnit")]
public class PlayerUnit : Unit {

    public float lastPosX;
    public float lastPosY;

    public void Heal(int amount) {

        currentHealth += amount;
        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

    }

}