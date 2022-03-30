using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour, IInteractable
{

    private FSM fsm;

    public EnemyUnit unit;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    public void Start()
    {

        SaveSystem.instance.LoadUnit(unit,unit.name);

        fsm = new FSM(typeof(IdleState), GetComponents<BaseState>());

        this.transform.position = new Vector2(unit.lastPosX,unit.lastPosY);

        unit.currentHealth = unit.maxHealth;
        unit.currentAttack = unit.baseAttack;
        unit.currentDefense = unit.baseDefense;

    }

    public void Update()
    {

        fsm.OnUpdate();

        unit.lastPosX = this.transform.position.x;
        unit.lastPosY = this.transform.position.y;

    }

    public void Interact()
    {

        fsm.SwitchState(typeof(InteractingState));
        SaveSystem.instance.SaveUnit(unit,unit.name);

    }

    public void SwitchSprite()
    {
        if (spriteRenderer.sprite == sprites[0])
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (spriteRenderer.sprite == sprites[1])
        {
            spriteRenderer.sprite = sprites[0];
        }
    }

}