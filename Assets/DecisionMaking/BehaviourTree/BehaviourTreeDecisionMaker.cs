using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class BehaviourTreeDecisionMaker : DecisionMaker 
	{
		// Blackboard area
		public Agent currentEnemy;

		Task rootTask;

		void Start () 
		{
			// Build the tree from game objects
			rootTask = GetComponentInChildren<Task> ();
			BuildTree (rootTask);
		}

		void BuildTree(Task parentTask)
		{
			// Assign state to the node
			parentTask.myAgent = GetComponent<Agent> ();
			parentTask.btdm = this;

			// Build composites
			if (parentTask.transform.childCount > 0) 
			{
				// This is a composite task!
				Composite composite = parentTask as Composite;

				composite.children = new List<Task> ();
				foreach (Transform child in composite.transform)
				{
					Task childTask = child.GetComponent<Task> ();
					composite.children.Add (childTask);

					BuildTree (childTask);
				}
			}
		}

		public override void MakeDecision()
		{
			// Traverse the tree and execute behaviours
			rootTask.Run();
		}

	}

}