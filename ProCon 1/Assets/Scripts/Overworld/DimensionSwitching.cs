using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitching : MonoBehaviour
{
	//[SerializeField] private List<PatrolState> patrolStates = new List<PatrolState>();
	[SerializeField] private NPC_Controller bob;

	[SerializeField] private bool inNormalDimension = false;

	[SerializeField] private List<GameObject> normalObjects = new List<GameObject>();
	[SerializeField] private List<GameObject> monsterObjects = new List<GameObject>();
	[SerializeField] private List<NPC_Controller> controllers = new List<NPC_Controller>();

	private void Start()
	{
		SwitchDimension();
	}

	public void SwitchDimension()
	{
		inNormalDimension = !inNormalDimension;
		if (inNormalDimension)
		{
			SetSwitchedStates(false);
		}
		else
		{
			SetSwitchedStates(true);
		}

		bob.SwitchSprite();

	}

	private void SetSwitchedStates(bool _setting)
	{
		// for (int i = 0; i < patrolStates.Count; i++)
		// {
		// 	patrolStates[i].canWalk = _setting;
		// }

		if(bob.unit.currentHealth <= 0) {
			bob.unit.spawnInAltWorld = false;
			bob.unit.inAltWorld = false;
		}

		for (int i = 0; i < normalObjects.Count; i++)
		{
			normalObjects[i].SetActive(!_setting);
		}
		for (int i = 0; i < monsterObjects.Count; i++)
		{
			monsterObjects[i].SetActive(_setting);
		}

		if (_setting)
		{
			for (int i = 0; i < controllers.Count; i++)
			{
				if (controllers[i].unit.spawnInAltWorld)
				{
					controllers[i].unit.inAltWorld = true;
					controllers[i].gameObject.SetActive(true);
				}
				else
				{
					controllers[i].gameObject.SetActive(false);
				}
			}
		}
		else
		{
			for (int i = 0; i < controllers.Count; i++)
			{
				controllers[i].unit.inAltWorld = false;

				controllers[i].gameObject.SetActive(true);
			}
		}
	}
}
