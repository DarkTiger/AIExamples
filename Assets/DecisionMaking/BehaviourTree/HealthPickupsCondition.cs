using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Movement;

namespace AI.BT
{

	public class HealthPickupsCondition : Task 
	{
		public override TaskState Run()
		{
			var healthPickups = FindObjectsOfType<HealthPickup> ();

			int totEnabled = 0;
			foreach (var hp in healthPickups) {
				if (hp.isEnabled)
					totEnabled++;
			}

			return totEnabled > 0 ? TaskState.SUCCESS : TaskState.FAILURE;
		}

	}

}