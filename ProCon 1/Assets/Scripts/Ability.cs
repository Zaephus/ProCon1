using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

    public virtual void Initialize(BattleSystem bSystem) {}
    //public abstract IEnumerator StartAbility();
    //public abstract void OnButtonPressed();

}