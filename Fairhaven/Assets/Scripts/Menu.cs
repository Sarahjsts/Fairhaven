using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button LoadGame;
    public Button NewGame;
    public Button Options;
    public string newGameScene;

    public GameObject loadGameMenu;
    public GameObject optionsMenu;

    //adds functionality to button
    public void Awake()
    {
        NewGame.onClick.AddListener(GameNew);
        LoadGame.onClick.AddListener(GameLoad);
        Options.onClick.AddListener(OpenOptions);
    }
    // Start is called before the first frame update
    public void GameNew()
    {
        //loads level 1 and makes a new game save in the database
        SceneManager.LoadScene(newGameScene);
    }

    //opens game load menu
    public void GameLoad()
    {
        loadGameMenu.SetActive(true);
    }

    //opens options menu
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
    }

}
