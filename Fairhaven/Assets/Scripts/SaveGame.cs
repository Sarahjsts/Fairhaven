using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    public Button save;

    // Start is called before the first frame update
    void Awake()
    {
        save.onClick.AddListener(Save);

    }


    // writes game save to database
    public void Save()
    {
        bool saved = false;
        Debug.Log(Application.persistentDataPath);
        if (InitGame.init)
        {
            string connectionString = "URI=file:" + Application.persistentDataPath + "/game.db";
            Debug.Log(connectionString);
            SqliteConnection dbcon = new SqliteConnection(connectionString);
            dbcon.Open();
            SqliteCommand dbcmd = (SqliteCommand)dbcon.CreateCommand();
            //string overWriteSave = "UPDATE [GameSave] " +
            //"SET [SceneName] = " + EditorSceneManager.GetActiveScene().name;
            //"WHERE [SaveID] = " + BoardCreator2.ID + ";";

            string newSave = "INSERT INTO GameSave VALUES (1, 'Level3');";
            //SqliteParameter str = new SqliteParameter("@Level", EditorSceneManager.GetActiveScene().name);
            //dbcmd.Parameters.Add(str);
            if (File.Exists(Application.persistentDataPath + "game.db"))
            {
                dbcmd.CommandText = newSave;
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                dbcon.Close();
                Debug.Log("counter 1");
                Debug.Log("save complete");
                saved = true;
            }
            else
            {
                Debug.Log("db not real");
            }

        }

    }
}
