using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{

    public class AStarSearch : MonoBehaviour
    {
        public Node startNode;
        public Node endNode;

        GraphMaker graphMaker;

        void Start()
        {
            graphMaker = FindObjectOfType<GraphMaker>();

            var nodes = graphMaker.nodes;
            var edges = graphMaker.edges;

            //List<Node> bestPath = new List<Node>();
            //StartCoroutine(Search(nodes, edges, startNode, endNode, bestPath));
        }

        public float delay = 0.25f;



        Node GetBestNode(List<Node> set, bool useHeuristic)
        {
            Node bestNode = null;
            float bestTotal = float.MaxValue;

            foreach(Node n in set)
            {
                var totalCost = useHeuristic ? n.cost + n.heuristic : n.cost;
                if (totalCost < bestTotal)
                {
                    bestTotal = totalCost;
                    bestNode = n;
                }
            }
            return bestNode;
        }

        public IEnumerator Search(List<Node> nodes, List<Edge> edges, Node startNode, Node endNode, List<Node> bestPath)
        {
            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();


            startNode.cost = 0;
            startNode.heuristic = Vector2.Distance(startNode.transform.position, endNode.transform.position);
            openSet.Add(startNode);

            while(openSet.Count > 0)
            {
                Node n = GetBestNode(openSet, true);
                openSet.Remove(n);
                closedSet.Add(n);
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
                    if (!closedSet.Contains(neigh) && !openSet.Contains(neigh))
                    {
                        Edge e = graphMaker.GetEdge(n, neigh);
                        e.color = Color.red;
                        yield return new WaitForSeconds(delay);

                        neigh.cost = n.cost + Vector2.Distance(neigh.transform.position, n.transform.position);
                        neigh.heuristic = Vector2.Distance(neigh.transform.position, endNode.transform.position);

                        openSet.Add(neigh);
                    }
                }
            }

            Debug.Log("END");


            // Find best path
            bestPath.Add(endNode);
            var currentNode = endNode;
            endNode.color = Color.green;
            while (currentNode != startNode)
            {
                // Get the neighbours of the current node
                List<Node> neighs = graphMaker.GetNeighbours(currentNode);

                // Find the best neighbour
                Node bestNeigh = GetBestNode(neighs, false);

                Edge e = graphMaker.GetEdge(currentNode, bestNeigh);

                bestPath.Add(bestNeigh);
                currentNode = bestNeigh;

                bestNeigh.color = Color.green;
                e.color = Color.green;
                yield return new WaitForSeconds(delay);
            }

            bestPath.Reverse();

            yield return null;
        }

    }

}