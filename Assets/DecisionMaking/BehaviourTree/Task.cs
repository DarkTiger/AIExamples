using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public abstract class Task : MonoBehaviour
	{
		public Agent myAgent;
		public BehaviourTreeDecisionMaker btdm; // for blackboard access

		public abstract TaskState Run(); 
	}

}