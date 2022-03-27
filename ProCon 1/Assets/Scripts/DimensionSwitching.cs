using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitching : MonoBehaviour
{
    [SerializeField] private List<PatrolState> patrolStates = new List<PatrolState>();

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

        //Hier nog tile maps aan en uit zetten
    }

    private void SetWalkingState(bool _setting)
    {
        for (int i = 0; i < patrolStates.Count; i++)
        {
            patrolStates[i].canWalk = _setting;
        }
    }
}
