using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardCreator2 : MonoBehaviour
{
    private GameObject boardHolder;
    // Start is called before the first frame update

    public static int ID = 0;
    public static int width = 15;
    public static int height = 15;
    public int chanceToStartAlive = 45;
    public int delete = 3;
    public int create = 3;
    public int numSteps = 0;
    public GameObject[] innerWall;
    public GameObject[] outerWall;
    public GameObject[] floor;
    public GameObject[] enemies;
    public GameObject exit;
    public static  int enemyCount;
    public bool won = false;
    public static bool[,] board;
    helper[,] flooded;
 
    void Start()
    {

    }
    private void Awake()
    {
        boardHolder = new GameObject("BoardHolder");
        CreateBoard();
        for (int i = 0; i < numSteps; i++)
        {
            board = SimStep(board);
        }
        /*
         * part of non functioning flood fill
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                    FloodFill(flooded[i, j]);

            }
        }
        Fix();
        */
        BoardTiles();
        LayoutObjectAtRandom(enemies, 1, 1);
        Win();
        
    }
    // creates a 2d array with values of either true or false depending on a random number
    public void CreateBoard() {
        board = new bool[width, height];
        flooded = new helper[width, height];

        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int filled = Random.Range(0, 100);
                if(filled < chanceToStartAlive)
                {
                    board[i, j] = true;
                }
                flooded[i, j] = new helper(i,j);
            }
        }
    }

    // takes created array board, and places tiles on game board
    public void BoardTiles()
    {
             for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        InstantiateFromArray(floor, i, j);
                        if (board[i, j] == true)
                        {
                            InstantiateFromArray(innerWall, i, j);
                        }
                    }
                }

        InstantiateOuterWalls();


    }

    // counts neighbors for purpose of cellular automata
    int CountNeighbors(bool[,] board, int x,int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int xNeighbor = x + i;
                int yNeighbor = y + j;
                if (i == 0 && j == 0)
                {

                }
                else if (xNeighbor < 0 || yNeighbor < 0 || xNeighbor >= board.GetLength(0) || yNeighbor >= board.GetLength(1)) 
                {
                    count = count + 1;
                }
              
                else if (board[xNeighbor,yNeighbor] == true)
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }
    // based on counted neighbors, switch tile to be more like its neighbors
    bool[,] SimStep(bool[,] board)
    {
        bool[,] newBoard = new bool[width, height];
        for(int i =0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j< board.GetLength(1); j++)
            {
                int num = CountNeighbors(board, i, j);
                if (board[i,j] == false)
                {
                    if (num < delete)
                    {
                        newBoard[i, j] = true;
                    }
                    else
                    {
                        newBoard[i, j] = false;
                    }
                } else
                {
                    if (num > create)
                    {
                        newBoard[i, j] = false;
                    }
                    else
                    {
                        newBoard[i, j] = true;
                    }
                }
            }
        }
        return newBoard;
    }
    /*
    // part of non functioning flood fill.
    public static List<helper> adjacencyList(helper[,] G, helper node)
    {
        List<helper> edges = new List<helper>();
        if ((node.y + 1) < BoardCreator2.height && !G[node.x, node.y + 1].val)
        {
            edges.Add(G[node.x, node.y + 1]);
        }
        if ((node.x + 1) < BoardCreator2.width && !G[node.x + 1, node.y].val)
        {
            edges.Add(G[node.x + 1, node.y]);
        }
        if ((node.y - 1) >= 0 && !G[node.x, node.y - 1].val)
        {
            edges.Add(G[node.x, node.y - 1]);
        }

        if ((node.x - 1) >= 0 && !G[node.x - 1, node.y].val)
        {
            edges.Add(G[node.x - 1, node.y]);
        }

        return edges;
    }

    //not quite functioning method to flood fill the map to check for isolated caves.
    int floodNum = 1;
    public void FloodFill(helper node)
    {
        if (board[node.x, node.y])
                {
            return;
                } else
        {
            flooded[node.x, node.y].flood = floodNum;
            FloodFill(flooded[node.x + 1, node.y]);
            FloodFill(flooded[node.x - 1, node.y]);
            FloodFill(flooded[node.x, node.y + 1]);
            FloodFill(flooded[node.x + 1, node.y - 1]);
            floodNum++;
        }

    }*/

    Vector3 RandomPosition()
    {
        
        int randomX = Random.Range(4, width);
        int randomY = Random.Range(4, height);
       
        Vector3 randomPosition = new Vector3(randomX, randomY);

        return randomPosition;
    }


    //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //Choose a random number of objects to instantiate within the minimum and maximum limits
        enemyCount = Random.Range(minimum, maximum + 1);

        //Instantiate objects until the randomly chosen limit objectCount is reached
        for (int i = 0; i < enemyCount; i++)
        {
           
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    //puts outer wall on level
    void InstantiateOuterWalls()
    {
        // The outer walls are one unit left, right, up and down from the board.
        float leftEdgeX = -1f;
        float rightEdgeX = width + 0f;
        float bottomEdgeY = -1f;
        float topEdgeY = height + 0f;

        // Instantiate both vertical walls (one on each side).
        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        // Instantiate both horizontal walls, these are one in left and right from the outer walls.
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeY);
    }


    void InstantiateVerticalOuterWall(float xCoord, float startingY, float endingY)
    {
        // Start the loop at the starting value for Y.
        float currentY = startingY;

        // While the value for Y is less than the end value...
        while (currentY <= endingY)
        {
            // ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
            InstantiateFromArray(outerWall, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(float startingX, float endingX, float yCoord)
    {
        // Start the loop at the starting value for X.
        float currentX = startingX;

        // While the value for X is less than the end value...
        while (currentX <= endingX)
        {
            // ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
            InstantiateFromArray(outerWall, currentX, yCoord);

            currentX++;
        }
    }

    //takes random tile from array and places it on the game board.
    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord)
    {
        // Create a random index for the array.
        int randomIndex = Random.Range(0, prefabs.Length);

        // The position to be instantiated at is based on the coordinates.
        Vector3 position = new Vector3(xCoord, yCoord, 0f);
        

        // Create an instance of the prefab from the random index of the array.
        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

        // Set the tile's parent to the board holder.
        tileInstance.transform.parent = boardHolder.transform;
    }
    void Win()
    {
        
        if (BoardCreator2.enemyCount <= 0 && !won)
        {
            Debug.Log(BoardCreator2.enemyCount);
            exit.SetActive(true);
            won = true;
        }
    }

}
