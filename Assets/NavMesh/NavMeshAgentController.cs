using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Navmesh
{

    public class NavMeshAgentController : MonoBehaviour {

        public Transform targetTransform;

        NavMeshAgent agent;

        void Start ()
        {
            agent = GetComponent<NavMeshAgent>();
	    }
	
	    void Update ()
        {
            agent.SetDestination(targetTransform.position);
        }
    }

}