using UnityEngine;

namespace AI.Pathfinding
{
    public class Edge
    {
        public Node n1;
        public Node n2;

        public override bool Equals(object obj)
        {
            var other_edge = (Edge)obj;
            if (other_edge == null) return false;

            return n1 == other_edge.n1 && n2 == other_edge.n2
                || n1 == other_edge.n2 && n2 == other_edge.n1;
        }



        public Color color = Color.white;

        public void Draw()
        {
            Debug.DrawLine(n1.transform.position, n2.transform.position, color);
        }

    }

}