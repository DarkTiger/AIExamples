using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.Pathfinding;

namespace AI.Movement
{

    public class PathFollower : MonoBehaviour
    {
        public SeekBehaviour seekBehaviour;
        public AStarSearch aStarSearch;

        public Node startNode;
        public Node endNode;

        public float nextTargetRadius = 1;

        List<Node> path;

        private void Start()
        {
            StartCoroutine(FindPath());
        }

        IEnumerator FindPath()
        {
            var graphMaker = FindObjectOfType<GraphMaker>();
            var nodes = graphMaker.nodes;
            var edges = graphMaker.edges;
            List<Node> bestPath = new List<Node>();
            yield return aStarSearch.StartCoroutine(aStarSearch.Search(nodes, edges, startNode, endNode, bestPath));
            path = bestPath;

            var nextTarget = path[0];
            seekBehaviour.targetTransform = nextTarget.transform;
        }

        void Update()
        {
            if (path != null && path.Count > 0)
            {
                var distSqr = Vector2.SqrMagnitude(seekBehaviour.targetTransform.position - transform.position);
                if (distSqr < nextTargetRadius * nextTargetRadius)
                {
                    Debug.Log("SWITCH");
                    path.RemoveAt(0);
                    var nextTarget = path[0];
                    seekBehaviour.targetTransform = nextTarget.transform;
                }

            }
        }
    }

}