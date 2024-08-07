using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AI.PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField] private int mapSize = 11;
        [SerializeField] private int generateForSeconds = 5;
        [SerializeField] private int roomRarity = 10;
        [SerializeField] private Vector2Int minMaxRoomSize;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private GameObject cubePrefab;
        float currentSeconds = 0;


        void Start()
        {
            StartCoroutine(GenerateCO());
        }

        private void Update()
        {
            currentSeconds += Time.deltaTime;
        }

        public enum Direction
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        public List<Cell> GetNeighbours(Cell c, int step)
        {
            List<Cell> neighs = new List<Cell>();

            // N
            if (c.y < mapSize - step) { neighs.Add(grid[c.x, c.y + step]); }
            else { neighs.Add(null); }

            // E
            if (c.x < mapSize - step) { neighs.Add(grid[c.x + step, c.y]); }
            else { neighs.Add(null); }

            // S
            if (c.y > step - 1) { neighs.Add(grid[c.x, c.y - step]); }
            else { neighs.Add(null); }

            // W
            if (c.x > step - 1) { neighs.Add(grid[c.x - step, c.y]); }
            else { neighs.Add(null); }
            
            return neighs;
        }

        public Cell GetNeighbour(Cell c, Direction d)
        {
            return grid[c.x + -((int)d - 2) % 2, c.y + -((int)d - 1) % 2];
        }

        Cell[,] grid;

        IEnumerator GenerateCO()
        {
            // Create a full dungeon

            grid = new Cell[mapSize, mapSize];

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    var cellGo = Instantiate(cellPrefab);
                    cellGo.transform.position = new Vector3(x, y, 0);

                    var cell = cellGo.GetComponent<Cell>();
                    cell.x = x;
                    cell.y = y;

                    grid[x, y] = cell;
                }
            }

            // Choose initial start cell
            int startX = Random.Range(0, mapSize / 2) * 2;
            int startY = Random.Range(0, mapSize / 2) * 2;
            grid[startX, startY].spriteRenderer.color = Color.blue;
            grid[startX, startY].visited = true;

            // Visit recursively adjacent cells
            Cell currentCell = grid[startX, startY];
            Stack<Cell> backCell = new Stack<Cell>();
            backCell.Push(currentCell);

            while (currentSeconds < generateForSeconds)
            {
                // Get 'room' cells
                List<Cell> neighs = GetNeighbours(currentCell, 2);

                // Keep unvisited cells
                List<Cell> unvisitedNeighs = neighs.Where(c => c != null && !c.visited).ToList();

                if (unvisitedNeighs.Count == 0)
                {
                    // Backtracking

                    currentCell = backCell.Pop();
                }
                else
                {
                    if (Random.Range(0, roomRarity) == 0)
                    {
                        yield return StartCoroutine(RoomGeneration(currentCell));
                    }

                    // Choose a random unvisited neigh
                    Cell rndNeigh;
                    int rndIndex;

                    do
                    {
                        rndIndex = Random.Range(0, neighs.Count);
                        rndNeigh = neighs[rndIndex];
                    }
                    while (!unvisitedNeighs.Contains(rndNeigh));
                    Direction rndDir = (Direction)rndIndex;

                    rndNeigh.spriteRenderer.color = Color.black;
                    rndNeigh.visited = true;

                    Cell wallNeigh = GetNeighbour(currentCell, rndDir);
                    wallNeigh.spriteRenderer.color = Color.black;
                    wallNeigh.visited = true;

                    // Move to the next 'room' cell
                    currentCell = rndNeigh;
                    backCell.Push(currentCell);
                }

                // End when the stack is empty
                if (backCell.Count == 0)
                {
                    break;
                }
               
                yield return null;
            }

            //backCell.Pop().spriteRenderer.color = Color.green;
            MakeDungeon3D();

            yield return null;
        }

        public IEnumerator RoomGeneration(Cell currentCell)
        {
            int maxRoomSize = Random.Range(minMaxRoomSize.x, minMaxRoomSize.y);
            int currentSizeX = 0;
            int currentSizeY = 0;

            Cell cell = currentCell;
            List<Cell> neighs = GetNeighbours(cell, 1);

            while (currentSizeX < maxRoomSize || currentSizeY < maxRoomSize)
            {
                foreach (Cell neigh in neighs)
                {
                    if (neigh)
                    {
                        if (currentSizeX < maxRoomSize)
                        {
                            if (neigh.x != cell.x)
                            {
                                neigh.spriteRenderer.color = Color.red;
                                neigh.visited = true;
                                currentSizeX++;
                            }
                        }

                        if (currentSizeY < maxRoomSize)
                        {
                            if (neigh.y != cell.y)
                            {
                                neigh.spriteRenderer.color = Color.red;
                                neigh.visited = true;
                                currentSizeY++;
                            }
                        }
                    }
                }
                
                do
                {
                    cell = neighs[Random.Range(0, neighs.Count)];
                }
                while (!cell);
                
                neighs = GetNeighbours(cell, 1);
                yield return null;
            }
        }

        void MakeDungeon3D()
        {
            for (int x = -1; x < mapSize + 2; x++)
            {
                for (int y = -1; y < mapSize + 2; y++)
                {     
                    if (x == -1 || x >= mapSize || y == -1 || y >= mapSize || !grid[x, y].visited)
                    {
                        // TODO: optimize cube numbers to max 1 cube of extra size
                        Instantiate(cubePrefab, new Vector3(x, 1, y), Quaternion.identity);
                    }
                }
            }
        }
    }
}
