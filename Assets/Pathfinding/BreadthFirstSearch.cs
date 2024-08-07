using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{

    public class BreadthFirstSearch : MonoBehaviour
    {

        public Node startNode;
        public Node endNode;


        GraphMaker graphMaker;

        void Start()
        {
            graphMaker = FindObjectOfType<GraphMaker>();

            var nodes = graphMaker.nodes;
            var edges = graphMaker.edges;


            StartCoroutine(Search(nodes, edges, startNode, endNode));
        }

        public float delay = 0.25f;

        IEnumerator Search(List<Node> nodes, List<Edge> edges, Node startNode, Node endNode)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(startNode);

            List<Node> visitedNodes = new List<Node>();

            while(queue.Count > 0)
            {
                Node n = queue.Dequeue();
                visitedNodes.Add(n);

                n.color = Color.red;
                yield return new WaitForSeconds(delay);

                if (n == endNode)
                {
                    Debug.Log("We found the end node!");
                    break;
                }

                List<Node> neighs = graphMaker.GetNeighbours(n);
                foreach(var neigh in neighs)
                {
                    if (!visitedNodes.Contains(neigh) && !queue.Contains(neigh))
                    {
                        Edge e = graphMaker.GetEdge(n, neigh);
                        e.color = Color.red;
                        yield return new WaitForSeconds(delay);

                        queue.Enqueue(neigh);
                    }
                }
            }
            yield return null;
        }

    }

}