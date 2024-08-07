using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class ShootEnemy : Task
	{
		public override TaskState Run ()
		{
			SeekBehaviour seekBe = myAgent.GetComponent<SeekBehaviour> ();
			FleeBehaviour fleeBe = myAgent.GetComponent<FleeBehaviour> ();

			myAgent.maximumLinearVelocity = 0;

			if (btdm.currentEnemy != null)
			{
				seekBe.weight = 1;
				fleeBe.weight = 0;
				seekBe.targetTransform = btdm.currentEnemy.transform;
				myAgent.GetComponent<ShootAction>().Shoot();
				return TaskState.SUCCESS;
			}

			return TaskState.FAILURE;
		}
	}

}