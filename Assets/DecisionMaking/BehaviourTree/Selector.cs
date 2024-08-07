using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class Selector : Composite
	{
		public override TaskState Run ()
		{
			foreach (var child in children) 
			{
				if (child.Run () == TaskState.SUCCESS)
					return TaskState.SUCCESS;
			}
			return TaskState.FAILURE;
		}
	}

}