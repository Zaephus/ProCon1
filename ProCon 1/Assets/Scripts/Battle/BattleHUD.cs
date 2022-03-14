using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {

    public Text nameText;

    public Slider healthSlider;
    public Text healthText;

    public Slider manaSlider;
    public Text manaText;

    public void SetHUD(Unit unit) {

        nameText.text = unit.unitName;

        healthSlider.maxValue = unit.maxHP;
        healthSlider.value = unit.currentHP;
        healthText.text = $"{unit.currentHP}";

        manaSlider.maxValue = unit.maxMana;
        manaSlider.value = unit.currentMana;
        manaText.text = $"{unit.currentMana}";

    }

    public void SetHealth(int health) {
        healthSlider.value = health;
        healthText.text = $"{health}";
    }

    public void SetMana(int mana) {
        manaSlider.value = mana;
        manaText.text = $"{mana}";
    }

}
