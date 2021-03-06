using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit : ScriptableObject {

    public string unitName;

    public int maxHealth;
    public int currentHealth;

    public int baseAttack;
    public int currentAttack;

    public int baseDefense;
    public int currentDefense;

    public float startPosX;
    public float startPosY;

    public float lastPosX;
    public float lastPosY;

    public bool canSpawn;

    public bool TakeDamage(int dmg) {

        int damage = dmg-currentDefense;
        if(damage <= 0) {
            damage = 0;
        }

        currentHealth -= damage;

        if(currentHealth <= 0) {
            currentHealth = 0;
            return true;
        }
        else {
            return false;
        }

    }
    
}

public class UnitSaver {

    public string unitName;

    public int maxHealth;
    public int currentHealth;

    public int baseAttack;
    public int currentAttack;

    public int baseDefense;
    public int currentDefense;

    public float startPosX;
    public float startPosY;

    public float lastPosX;
    public float lastPosY;
    
}