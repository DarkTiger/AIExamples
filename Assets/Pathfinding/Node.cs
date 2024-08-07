using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{

    public class Node : MonoBehaviour
    {
        public Color color = Color.white;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            spriteRenderer.color = color;

        }


        // A* weights
        public float cost = float.MaxValue;
        public float heuristic;

    }
}
