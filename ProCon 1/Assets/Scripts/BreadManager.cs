using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BreadManager : MonoBehaviour
{
    public static BreadManager breadManager;
    [SerializeField] private TextMeshProUGUI counterText;

    private void Awake()
    {
        breadManager = this;
        int amount = PlayerPrefs.GetInt("FrikandelBroodjes");
        counterText.text = "" + amount;
    }

    public void AddBroodje(int _amount)
    {
        int amount = PlayerPrefs.GetInt("FrikandelBroodjes");
        amount += _amount;
        PlayerPrefs.SetInt("FrikandelBroodjes", amount);
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
