using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class AttackInfo : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

    public TMP_Text infoText;

    public Button button;

    public GameObject panel;

    public string attackInfoText;

    public void Update() {
        
        panel.transform.position = Input.mousePosition;

    }

    public void SetText(string text) {
        infoText.text = text;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        
        if(button) {
            SetText(attackInfoText);
            panel.SetActive(true);
        }

    }

    public void OnPointerExit(PointerEventData eventData) {
        
        if(button) {
            panel.SetActive(false);
        }

    }

}