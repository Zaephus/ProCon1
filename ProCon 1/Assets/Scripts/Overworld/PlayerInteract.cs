using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour {

    public Image prompt;

    private Vector2 promptPosition;

    IInteractable interact;

    private bool canInteract = false;

    public void Start() {

        prompt.enabled = false;

    }

    public void Update() {

        if(Input.GetButtonDown("Interact") && canInteract) {
                interact.Interact();
                prompt.enabled = false;
                canInteract = false;
            }

    }

    public void OnTriggerEnter2D(Collider2D other) {

        if(other.GetComponent<IInteractable>() != null) {

            interact = other.GetComponent<IInteractable>();

            promptPosition = other.transform.position;
            promptPosition.y += 1;

            prompt.enabled = true;
            prompt.transform.position = promptPosition;

        }

    }

    public void OnTriggerStay2D(Collider2D other) {

        if(other.GetComponent<IInteractable>() != null) {

            promptPosition = other.transform.position;
            promptPosition.y += 1;

            canInteract = true;

            prompt.transform.position = promptPosition;

        }

    }

    public void OnTriggerExit2D(Collider2D other) {

        prompt.enabled = false;
        canInteract = false;

    }

}