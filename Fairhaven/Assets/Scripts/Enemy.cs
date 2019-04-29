using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public int health;
    //public Slider HPbar;
    public GameObject enemy;
    public GameObject player;
    static public int HP = 100;
    static public int MP;
    static public int Att;
    static public int Def;
    static public int Mag;
    static public int Mdef;
    public bool dead = false;
    public float moveSpeed = 4f;
    private Animator anim;
    helper[,] board = new helper[BoardCreator2.width, BoardCreator2.height];

    public static helper[,] init(helper[,] G)
    {


        for (int i = 0; i < G.GetLength(0); i++)
        {
            for (int j = 0; j < G.GetLength(1); j++)
            {
                G[i, j] = new helper(i, j);
                if(BoardCreator2.board[i,j]== false)
                {
                    G[i, j].val = false;
                }
                
                else{
                    G[i, j].val = true;
                }
            }
        }
        return G;
    }
    helper[] array = null;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find(this.name);
        player = GameObject.Find("Player");
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        board = init(board);

        int startx = (int)enemy.transform.position.x;
        int starty = (int)enemy.transform.position.y;
        helper start = board[startx, starty];

        int goalx = (int)player.transform.position.x;
        int goaly = (int)player.transform.position.y;
        helper goal = new helper(goalx, goaly);

        array = Pathing(board, start, goal);
        i = array.Length - 1;
        Debug.Log("i is ? " + i);
    }



    
 
    int delay = 2;
    private int movInterval = 10;
    private int newPathInt = 25;
    private bool complete = false;


   
    void Update()
    {
        // sets enemy to move along the path every 10 frames rather than every frame
        if(Time.frameCount%movInterval == 0 && i >0)
        {

            Vector3 position = new Vector3(array[i].x, array[i].y, 0);

            float step = moveSpeed * Time.deltaTime;


            // transform.position = Vector3.MoveTowards(transform.position, position, step);
            transform.position = position;
            i--;

        }
        if (Time.frameCount % newPathInt == 0 && i <=0 && !dead)
        {
            Debug.Log("print");
            board = init(board);
            int newX = (int)enemy.transform.position.x;
            int newY = (int)enemy.transform.position.y;
            helper start = board[newX, newY];

            int newGoalX = (int)player.transform.position.x;
            int newGoalY = (int)player.transform.position.y;
            helper goal = new helper(newGoalX, newGoalY);
            Debug.Log("Player position is ? " + newX + ", " + newY);

            array = Pathing(board, start, goal);           
            i = array.Length - 1;
            Debug.Log(" array  length is " + i);
        }

        // non functioning code to make enemy make a new path




        if (dead)
        {
            anim.SetTrigger("dead");
        }



    }
    //obtains player's position so enemy can make a new path towards them.
    public void initPathing()
    {

        int startx = (int)enemy.transform.position.x;
        int starty = (int)enemy.transform.position.y;
        helper start = board[startx, starty];

        int goalx = (int)player.transform.position.x;
        int goaly = (int)player.transform.position.y;
        helper goal = new helper(goalx, goaly);
        Debug.Log(goal.x);

        array = Pathing(board, start, goal);
        i = array.Length - 1;
    }

    public static helper[] Pathing(helper[,] board, helper start, helper goal)
    {

        
        start.visited = true;
        

        Queue<helper> queue = new Queue<helper>();
        
        
        Queue<helper> prev = new Queue<helper>();
        queue.Enqueue(start);
        
        while (queue.Count != 0)
        {
            helper v = queue.Dequeue();
            
            v.visited = true;
            Debug.Log(v.x);
            if (v.Equals(goal))
            {
               
                prev = FindPath(v, prev);
                
                
                break;
            }

            
            List<helper> list = adjacencyList(board, v);

            for (int i = 0; i < list.Count; i++)
            {
                
                if (list[i].visited == false)
                {
                    Debug.Log(list[i].y);
                    list[i].visited = true;
                    list[i].setPrevious(v);
                    
                    queue.Enqueue(list[i]);

                }
            }

        }
        return prev.ToArray();

    }
    // creates list of edges for use in pathing function
    public static List<helper> adjacencyList(helper[,] G, helper node)
    {
        List<helper> edges = new List<helper>();
        if ((node.y + 1) < BoardCreator2.height&& !G[node.x, node.y + 1].val)
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
    public static Queue<helper> FindPath(helper node, Queue<helper> queue)
    {
        
        bool isTrue = node.previous == null;
        helper newNode = node.previous;

        if (isTrue)
        {
            return queue;
        }
        else
        {
            queue.Enqueue(newNode);
            return FindPath(newNode, queue);
        }
    }
    public void TakeDamage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            //HPbar.value = health;
        } else
        {
            dead = true;
            if (BoardCreator2.enemyCount > 0 && dead)
            {
                BoardCreator2.enemyCount--;
                Debug.Log("enemy count " + BoardCreator2.enemyCount);

            }
        }

    }

}
