using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class MyHealthCondition : Task 
	{
		public float minHealth = 5;

		public override TaskState Run()
		{
			var myHealthState = myAgent.GetComponent<HealthState>();
			return myHealthState.health >= minHealth ? TaskState.SUCCESS : TaskState.FAILURE;
		}

	}

}