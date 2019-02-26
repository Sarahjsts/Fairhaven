using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator2 : MonoBehaviour
{
    private GameObject boardHolder;
    // Start is called before the first frame update


    public int width = 40;
    public int height = 40;
    public int delete = 3;
    public int create = 3;
    public int numSteps = 0;
    public GameObject[] innerWall;
    public GameObject[] outerWall;
    public GameObject[] floor;
    public int[,] board;

    void Start()
    {
        boardHolder = new GameObject("BoardHolder");
        CreateBoard();
        for(int i = 0; i < numSteps; i++)
        {
            board = SimStep(board);
        }
     
        BoardTiles();
    }
    public void CreateBoard() {
        board = new int[width, height];
        
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                int filled = Random.Range(0, 2);
                    board[i, j] = filled;
            }
        }
    }
    public void BoardTiles()
    {
        InstantiateOuterWalls();

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        InstantiateFromArray(floor, i, j);
                        if (board[i, j] == 0)
                        {
                            InstantiateFromArray(innerWall, i, j);
                        }
                    }
                }          


    }
    int CountNeighbors(int[,] board, int x,int y)
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
                //Otherwise, a normal check of the neighbour
                else if (board[xNeighbor,yNeighbor] == 1)
                {
                    count = count + 1;
                }
            }
        }
        return count;
    }
    int[,] SimStep(int [,] board)
    {
        int[,] newBoard = new int[width, height];
        for(int i =0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j< board.GetLength(1); j++)
            {
                int num = CountNeighbors(board, i, j);
                if (board[i,j] == 0)
                {
                    if (num < delete)
                    {
                        newBoard[i, j] = 1;
                    }
                    else
                    {
                        newBoard[i, j] = 0;
                    }
                } else
                {
                    if (num > create)
                    {
                        newBoard[i, j] = 0;
                    }
                    else
                    {
                        newBoard[i, j] = 1;
                    }
                }
            }
        }
        return newBoard;
    }
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
}
