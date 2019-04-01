using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public int health;
    //public Slider HPbar;
    public GameObject enemy;
    public bool dead = false;
    public float moveSpeed = 2f;
    private Animator anim;


    public static helper[,] init()
    {
        int width = 20;
        int height = 20;
        helper[,] board = new helper[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = new helper(i, j);
            }
        }
        return board;
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Pathing();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            anim.SetTrigger("dead");
        }
    }

    //not quite functioning rudimentary AI 
    public void Pathing()
    {
        helper[,] board = null;
        board = init();

        int startx = 0;
        int starty = 0;
        int goalx = 2;
        int goaly = 4;

        Queue<helper> queue = new Queue<helper>();
        board[startx, starty].visited = true;
        helper start = board[startx, starty];
        helper goal = new helper(goalx, goaly);
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            helper v = queue.Dequeue();
            Debug.Log(v.x + "," + v.y);
            if (transform.position.x == goal.x && transform.position.y == goal.y)
            {
                body.velocity = new Vector2(0,0);
                break;
            }
            Vector3 position = new Vector3(v.x, v.y, 0f);
            transform.Translate(position);
            v.visited = true;

            helper[] array = new helper[4];
            if (v.y + 1 < board.GetLength(1))
            {
                
                array[0] = board[v.x, v.y + 1];
                if (!array[0].visited)
                {
                    queue.Enqueue(array[0]);
                    array[0].visited = true;
                    Debug.Log("go");

                }

            } 
            else
            {
                array[0] = null;
            }
            if (v.x - 1 >= 0)
            {
                array[1] = board[v.x - 1, v.y];
                if (!array[1].visited)
                {
                    queue.Enqueue(array[1]);
                    array[1].visited = true;
                    Debug.Log("go");
                }
            }
            else
            {
                array[1] = null;
            }
            
            if (v.y - 1 >= 0)
            {
                array[2] = board[v.x, v.y - 1];
                if (!array[2].visited)
                {
                    queue.Enqueue(array[2]);
                    array[2].visited = true;
                    Debug.Log("go");
                }

            }
            else
            {
                array[2] = null;
            }
            if (v.x + 1 < board.GetLength(0))
            {
                array[3] = board[v.x + 1, v.y];
                if (!array[3].visited)
                {
                    queue.Enqueue(array[3]);
                    array[3].visited = true;
                    Debug.Log("go");
                }
            }
            else
            {
                array[3] = null;
            }
            
            // I have no idea why this doesn't work in Unity
           /* for (int i = 0; i < array.Length; i++)
            {
                
                if (array[i] != null)
                {
                    if (!array[i].visited)
                    {
                        queue.Enqueue(array[i]);
                        array[i].visited = true;
                        Debug.Log("go");

                    }

                }
            }*/
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
