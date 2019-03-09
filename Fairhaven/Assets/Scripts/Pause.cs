using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // writes game save to database
    public void Save()
    {

    }

    // opens options menu
    public void Options()
    {

    }

}
