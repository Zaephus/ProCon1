using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueButton : MonoBehaviour {

    public TMP_Text buttonText;
    public TMP_Text percentageText;

    public void SetButtonText(string text) {
        buttonText.text = text;
    }

    public void SetPercentageText(string text) {
        percentageText.text = text;
    }

    public void ActivePercentages(bool state) {
        percentageText.gameObject.SetActive(state);
    }

}