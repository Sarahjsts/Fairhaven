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
    public int[] gameID = new int[3];
    public int[] Playerstats = new int[6];
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
        BoardCreator2.ID = gameID[0];
        Player.playerHP = Playerstats[0];
        Player.playerMP = Playerstats[1];
        Player.Att = Playerstats[2];
        Player.Def = Playerstats[3];
        Player.Mag = Playerstats[4];
        Player.Mdef = Playerstats[5];

    }
    // opens sqlite db and pulls scene name from GameSave table
    public void LoadGame()
    {
        string connectionString = "URI=file:" + Application.persistentDataPath + "game.db";
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand();
        const string sql = "SELECT * FROM [GameSave]";
        dbcmd.CommandText = sql;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string saveName = reader.GetString(1);
            gameSave[0] = saveName;
            gameID[0] = reader.GetInt32(0);
        }
 
        string stats = "SELECT * FROM [Player]";
        dbcmd.CommandText = stats;
        reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            for(int i = 0; i < stats.Length; i++)
            {
                Playerstats[i] = reader.GetInt32(i+1);
            }
        }
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

 
