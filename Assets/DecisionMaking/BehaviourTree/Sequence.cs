using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class Sequence : Composite
	{
		public override TaskState Run ()
		{
			foreach (var child in children) 
			{
				if (child.Run () == TaskState.FAILURE)
					return TaskState.FAILURE;
			}
			return TaskState.SUCCESS;
		}
	}

}