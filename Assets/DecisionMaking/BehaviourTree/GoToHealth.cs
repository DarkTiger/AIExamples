using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class GoToHealth : Task
	{
		public override TaskState Run ()
		{
			SeekBehaviour seekBe = myAgent.GetComponent<SeekBehaviour> ();
			FleeBehaviour fleeBe = myAgent.GetComponent<FleeBehaviour> ();

			myAgent.maximumLinearVelocity = 1;

			var pickups = FindObjectsOfType<HealthPickup> ();

			foreach (var pickup in pickups) 
			{
				if (pickup.isEnabled) 
				{
					seekBe.weight = 1;
					fleeBe.weight = 0;
					seekBe.targetTransform = pickup.transform;
					return TaskState.SUCCESS;
				}
			}

			return TaskState.FAILURE;
		}
	}

}