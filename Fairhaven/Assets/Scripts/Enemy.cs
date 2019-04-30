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
    public int HP = 100;
    static public int MP;
    static public int Att;
    static public int Def;
    static public int Mag;
    static public int Mdef;
    public bool dead = false;
    public float moveSpeed = 4f;
    Enemy test;
    private Animator anim;
    helper[,] board = new helper[BoardCreator2.width, BoardCreator2.height];



    public static helper[,] init(helper[,] G)
    {


        for (int i = 0; i < G.GetLength(0); i++)
        {
            for (int j = 0; j < G.GetLength(1); j++)
            {
                G[i, j] = new helper(i, j);
                if (BoardCreator2.board[i, j] == false)
                {
                    G[i, j].val = false;
                }

                else
                {
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
        test = new Enemy();
        enemy = GameObject.Find(this.name);
        Debug.Log(this.name);
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

        array = BFS.Pathing(board, start, goal);
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
        if (Time.frameCount % movInterval == 0 && i > 0)
        {

            Vector3 position = new Vector3(array[i].x, array[i].y, 0);

            float step = moveSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, position, step);
            //transform.position = position;
            i--;

        }
        if (Time.frameCount % newPathInt == 0 && i <= 0 && !dead)
        {
            initPathing();
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
        board = init(board);
        helper start = board[startx, starty];

        int goalx = (int)player.transform.position.x;
        int goaly = (int)player.transform.position.y;
        helper goal = new helper(goalx, goaly);

        array = BFS.Pathing(board, start, goal);
        i = array.Length - 1;
    }
    
    public void TakeDamage(int damage)
    {


        if (health > 0)
        {
            health -= damage;
            Debug.Log(" hp is" + health);
            //HPbar.value = health;
        }
        else
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

