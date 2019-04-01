using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;


    private void Update()
    {
        //opens pause menu when 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            PauseMenu();
            
        }
    }
    public void PauseMenu()
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            paused = true;
        } else
        {
            pauseMenu.SetActive(false);
            paused = false;
        }
        
    }


    // opens options menu
    public void Options()
    {

    }

}
