using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Unit",menuName = "Unit")]
public class Unit : ScriptableObject {

    public string unitName;

    public int damage;

    public int maxHealth;
    public int currentHealth;

    public int maxMana;
    public int currentMana;

    public bool TakeDamage(int dmg) {

        currentHealth -= dmg;

        if(currentHealth <= 0) {
            currentHealth = 0;
            return true;
        }
        else {
            return false;
        }

    }
    
}