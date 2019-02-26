using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    private GameObject boardHolder;
    public int width = 0;
    public int height = 0;
    public int maxArea = 0;
    public int[,] board;
    public GameObject[] innerWall;
    public GameObject[] outerWall;
    public GameObject[] floor;
    Node node;
    Node leftNode = null;
    Node rightNode = null;

    // Start is called before the first frame update
    void Start()
    {
        board = new int[width, height];
        boardHolder = new GameObject("BoardHolder");
        node = new Node(height, width, 0, 0);
       
        InitializeBoard(board);
        InstantiateOuterWalls();
        SpacePart(node);
        
        CreateRoom(node);
        InitializeNode(node);
        PlaceTile(board);
        Debug.Log(node.GetRight().xMin);
    }
    
    void SpacePart(Node node)
    {
        if ((node.width * node.height) < maxArea)
        {
            
            int HoW = Random.Range(0, 2); // split along height = 0 split along width = 1
            if (HoW == 0)
            {
                int splitter = Random.Range(node.xPos, width);
                
                leftNode = new Node(splitter - node.xPos, width, node.xPos, node.yPos);
                rightNode = new Node(height - splitter, width, node.xPos, splitter);
                node.SetLeft(leftNode);
                node.SetRight(rightNode);
            }
            if (HoW == 1)
            {
                int splitter = Random.Range(node.yPos, height);
                leftNode = new Node(height, splitter - node.yPos, node.xPos, node.yPos);
                rightNode = new Node(height, width - splitter, splitter, node.yPos);
                node.SetLeft(leftNode);
                node.SetRight(rightNode);
            }
            
            SpacePart(leftNode);
            SpacePart(rightNode);
        }
        else
        {
            return;
        }
    }

    void CreateRoom(Node node)
    {
        if(node.GetLeft()!= null || node.GetRight()!= null)
        {
            if(node.GetLeft()!= null)
            {
                CreateRoom(node.GetLeft());
            }
            if (node.GetRight() != null)
            {
                CreateRoom(node.GetRight());
            }
        } else
        {
            node.MakeRect();
        }
    }

    void InitializeBoard(int[,] board)
    {
        for(int i = 0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = 0;
            }
        }
    }
    void InitializeNode(Node node)
    {
        if(node.xMin == -1)
        {
            if(node.GetLeft().xMin== -1)
            {
                InitializeNode(node.GetLeft());
            }
            if (node.GetRight().xMin == -1)
            {
                InitializeNode(node.GetRight());
            }
        } else
        {
            for(int i = (int)node.xMin; i < node.xMax; i++)
            {
                for(int j = (int) node.yMin; j < node.yMax; j++)
                {
                    board[i, j] = 1;
                }
            }
        }
    }
    void PlaceTile(int[,] board)
    {
        for(int i = 0; i < board.GetLength(0); i++)
        {
            for(int j = 0; j < board.GetLength(1); j++)
            {
                if(board[i,j] == 0)
                {
                    InstantiateFromArray(floor, i, j);
                }
                InstantiateFromArray(innerWall, i, j);
            }
        }
        
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

