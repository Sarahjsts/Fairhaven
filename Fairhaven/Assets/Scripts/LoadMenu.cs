using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class LoadMenu : MonoBehaviour
{


    public Button back;
    public GameObject loadGameMenu;
    public Button Save1;
    public Text textSave1;
    private string[] gameSave = new string[1]; 
    // Start is called before the first frame update
    public void Awake()
    {
        back.onClick.AddListener(GoBack);
        LoadGame();
        textSave1.text = gameSave[0];
        Save1.onClick.AddListener(Load);
    }

    //loads scene from database
    public void Load()
    {

        SceneManager.LoadScene(textSave1.text);
    }
    // opens sqlite db and pulls scene name from GameSave table
    public void LoadGame()
    {
        const string connectionString = "URI=file:game.db";
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand();
        // requires a table to be created named employee
        // with columns firstname and lastname
        // such as,
        //        CREATE TABLE employee (
        //           firstname nvarchar(32),
        //           lastname nvarchar(32));
        const string sql = "SELECT * FROM [GameSave]";
        dbcmd.CommandText = sql;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string lastName = reader.GetString(1);
            gameSave[0] = lastName;
        }
        // clean up
        reader.Dispose();
        dbcmd.Dispose();
        dbcon.Close();
    }
    // closes load game menu
    public void GoBack()
    {
        loadGameMenu.SetActive(false);
    }


}

 
