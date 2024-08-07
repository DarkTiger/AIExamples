using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public abstract class Composite : Task
	{
		public List<Task> children;
	}

}