using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class Flee : Task
	{
		public override TaskState Run ()
		{
			SeekBehaviour seekBe = myAgent.GetComponent<SeekBehaviour> ();
			FleeBehaviour fleeBe = myAgent.GetComponent<FleeBehaviour> ();

			myAgent.maximumLinearVelocity = 1;

			Transform baseTransform = FindObjectOfType<Base> ().transform;

			seekBe.weight = 0;
			fleeBe.weight = 1;

			fleeBe.targetTransform = baseTransform;

			return TaskState.SUCCESS;
		}
	}

}