using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

    public Image prompt;

    private Vector2 promptPosition;

    public void Start() {

        prompt.enabled = false;

    }

    public void OnTriggerEnter2D(Collider2D other) {

        promptPosition = other.transform.position;
        promptPosition.y += 1;

        prompt.enabled = true;
        prompt.transform.position = promptPosition;

    }

    public void OnTriggerStay2D(Collider2D other) {

        promptPosition = other.transform.position;
        promptPosition.y += 1;

        if(other.GetComponent<IInteractable>() != null) {
            IInteractable interact = other.GetComponent<IInteractable>();
            if(Input.GetButtonDown("Interact")) {
                interact.Interact();
                prompt.enabled = false;
            }
        }

        prompt.transform.position = promptPosition;

    }

    public void OnTriggerExit2D(Collider2D other) {

        prompt.enabled = false;

    }

}