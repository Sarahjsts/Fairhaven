using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class InitGame : MonoBehaviour
{
   public static bool init = false;
    // Start is called before the first frame update
   public Button save;

    void Start()
    {
        if (!init)
        {
            createDB();
            Debug.Log("DB created");
            Debug.Log(Application.persistentDataPath);

        }
    }

    void createDB()
    {
        Debug.Log(Application.persistentDataPath);
        string connectionString = "URI=file:" + Application.persistentDataPath + "/game.db";
        Debug.Log(connectionString);
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand();
        string databaseCreation = @"DROP TABLE IF EXISTS [GameSave];
                CREATE TABLE [GameSave] (
                        [SaveID] INTEGER NOT NULL PRIMARY KEY,
                        [SceneName] VARCHAR(50) 
                );

                DROP TABLE IF EXISTS [Player];
                CREATE TABLE [Player] (
                        [PlayerID] INTEGER NOT NULL PRIMARY KEY,
                        [PlayerHp] INTEGER,
                        [PlayerMp] INTEGER,
                        [PlayerAtt] INTEGER,
                        [PlayerDef] INTEGER,
                        [PlayerMag] INTEGER,
                        [PlayerMdef] INTEGER,
                        FOREIGN KEY(PlayerID) REFERENCES GameSave(SaveID)
                );

                DROP TABLE IF EXISTS [Npc];
                CREATE TABLE [Npc] (
                        [NPCID] INTEGER NOT NULL PRIMARY KEY,
                        [NPCHp] INTEGER NOT NULL,
                        [NPCMp] INTEGER NOT NULL,
                        [NPCAtt] INTEGER NOT NULL,
                        [NPCDef] INTEGER NOT NULL,
                        [NPCMag] INTEGER NOT NULL,
                        [NPCMdef] INTEGER NOT NULL
                );

                DROP TABLE IF EXISTS [Items];
                CREATE TABLE [Items](
                        [ItemID] INTEGER NOT NULL PRIMARY KEY,
                        [ItemEffect] VARCHAR(50)

                );";
        dbcmd.CommandText = databaseCreation;
        dbcmd.ExecuteNonQuery();
        init = true;
        dbcmd.Dispose();
        dbcon.Close();
    }

    void SaveGame()
    {
        string connectionString = "URI=file:" + Application.persistentDataPath + "game.db";
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand();
        //string overWriteSave = "UPDATE [GameSave] " +
        //"SET [SceneName] = " + EditorSceneManager.GetActiveScene().name;
        //"WHERE [SaveID] = " + BoardCreator2.ID + ";";
        string newSave = "INSERT INTO GameSave VALUES (1, 'Level1')";
        //SqliteParameter str = new SqliteParameter("@Level", EditorSceneManager.GetActiveScene().name);
        //dbcmd.Parameters.Add(str);
       
            dbcmd.CommandText = newSave;
            dbcmd.ExecuteNonQuery();
            Debug.Log("counter 1");
            

        dbcmd.Dispose();
        dbcon.Close();
        Debug.Log("save complete");
    }
}
