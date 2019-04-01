using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public bool loss;
    GameObject gameOver;
    public GameObject exit;
    public bool won = false;
    public int num = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BoardCreator2.enemyCount <= 0 && !won)
        {
            Vector3 defaultPos = new Vector3(BoardCreator2.width - 1, BoardCreator2.height - 1);
            exit.transform.position = defaultPos;
            GameObject tileInstance = Instantiate(exit, defaultPos, Quaternion.identity) as GameObject;
            won = true;
        }

    }




        public void Win()
        {

        }
        //win condition. all enemies are dead

        // loss condition. player is dead

    }

