using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitching : MonoBehaviour
{
    [SerializeField] private List<PatrolState> patrolStates = new List<PatrolState>();
    [SerializeField] private List<PlayerInteract> playerInteract = new List<PlayerInteract>();
    [SerializeField] private List<NPC_Controller> NPC_Controllers = new List<NPC_Controller>();

    [SerializeField] private bool inNormalDimension = false;

    private void Start()
    {
        SwitchDimension();
    }

    public void SwitchDimension()
    {
        inNormalDimension = !inNormalDimension;
        if(inNormalDimension)
        {
            SetWalkingState(false);
        }
        else
        {
            SetWalkingState(true);
        }
        for (int i = 0; i < NPC_Controllers.Count; i++)
        {
            NPC_Controllers[i].SwitchSprite();
        }
        //Hier nog tile maps aan en uit zetten
    }

    private void SetWalkingState(bool _setting)
    {
        for (int i = 0; i < patrolStates.Count; i++)
        {
            patrolStates[i].canWalk = _setting;
        }
        for (int i = 0; i < playerInteract.Count; i++)
        {
            playerInteract[i].fightOnContact = _setting;
        }
    }
}
