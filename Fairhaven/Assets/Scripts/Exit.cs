﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject exit;
    private void Awake()
    {
        Vector3 defaultPos = new Vector3(BoardCreator2.width - 1, BoardCreator2.height - 1);
        exit.transform.position = defaultPos;
 
        Debug.Log("exit is false");
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // loads level 2 when player gets to exit tile
        if(collision.gameObject.name == "Player" && BoardCreator2.enemyCount == 0)
        {
            string name = SceneManager.GetActiveScene().name;
            string justNumbers = new string( name.Where(char.IsDigit).ToArray());
            int SceneNum = Int32.Parse(justNumbers) + 1;
           
            SceneManager.LoadScene("Level" + SceneNum);
        }
        
    }


}
