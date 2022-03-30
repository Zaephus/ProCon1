using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInteractable {

    public BreadManager breadManager;

    public void Start() {}

    public void Update() {}

    public void Interact() {

        breadManager.AddBroodje(2);
        Destroy(this.gameObject);

    }

}