using System;

public class Class1
{
    private GameObject boardHolder;
    public int width = 20;
    public int height = 20;
    public int maxArea = 4;
    public int[,] board;
    //public GameObject[] innerWall;
    //public GameObject[] outerWall;
    //public GameObject[] floor;
    Node node;

    // Start is called before the first frame update
   static void Main()
    {
        board = new int[width, height];
        //boardHolder = new GameObject("BoardHolder");
        node = new Node(height, width, 0, 0);
        InitializeBoard(board);
        InstantiateOuterWalls();
        SpacePart(node);
        CreateRoom(node);
        InitializeNode(node);
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write(Board[i, j] + " ");
            }
            Console.WriteLine(" ");
            Console.ReadKey;
        }
        //PlaceTile(board);
    }
    
    void SpacePart(Node node)
    {
        if ((node.width * node.height) > maxArea)
        {
            Node leftNode = null;
            Node rightNode = null;
            int HoW = Random.Range(0, 2); // split along height = 0 split along width = 1
            if (HoW == 0)
            {
                int splitter = Random.Range(0, width);
                leftNode = new Node(splitter - node.xPos, width, node.xPos, node.yPos);
                rightNode = new Node(height - splitter, width, node.xPos, splitter);
                node.SetLeft(leftNode);
                node.SetRight(rightNode);
            }
            if (HoW == 1)
            {
                int splitter = Random.Range(0, height);
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
        if(node.rect == null)
        {
            if(node.GetLeft().rect == null)
            {
                InitializeNode(node.GetLeft());
            }
            if (node.GetRight().rect == null)
            {
                InitializeNode(node.GetRight());
            }
        } else
        {
            for(int i = (int)node.rect.xMin; i < node.rect.xMax; i++)
            {
                for(int j = (int) node.rect.yMin; j < node.rect.yMax; j++)
                {
                    board[i, j] = 1;
                }
            }
        }
    }
}
