using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class GoToBase : Task
	{
		public override TaskState Run ()
		{
			SeekBehaviour seekBe = myAgent.GetComponent<SeekBehaviour> ();
			FleeBehaviour fleeBe = myAgent.GetComponent<FleeBehaviour> ();

			myAgent.maximumLinearVelocity = 1;

			Transform baseTransform = FindObjectOfType<Base> ().transform;

			seekBe.weight = 1;
			fleeBe.weight = 0;

			seekBe.targetTransform = baseTransform;

			return TaskState.SUCCESS;
		}
	}

}