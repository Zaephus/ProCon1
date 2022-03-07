using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

    public virtual void Initialize(PlayerBattleController p,EnemyBattleController e) {}
    public virtual void Initialize(EnemyBattleController e,PlayerBattleController p) {}
    public abstract void OnUpdate();

}