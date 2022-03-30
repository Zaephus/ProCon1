using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BroodjesCounter : MonoBehaviour {

    private Text counterText;

    private void Awake()
    {

        counterText = GetComponent<Text>();

        int amount = PlayerPrefs.GetInt("FrikandelBroodjes");
        counterText.text = "" + amount;
        
    }

    public void SubtractBroodje(int _amount)
    {
        int amount = PlayerPrefs.GetInt("FrikandelBroodjes");
        if (amount > 0)
        {
            amount -= _amount;
            PlayerPrefs.SetInt("FrikandelBroodjes", amount);
        }
        counterText.text = "" + amount;
    }

}