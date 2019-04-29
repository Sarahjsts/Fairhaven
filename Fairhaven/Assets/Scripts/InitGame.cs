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
        SaveID INTEGER PRIMARY KEY AUTOINCREMENT,
        SceneName VARCHAR(50));

DROP TABLE IF EXISTS [Player];
CREATE TABLE [Player] (
        PlayerID INTEGER PRIMARY KEY AUTOINCREMENT,
        PlayerHP INT,
        PlayerMP INT,
        PlayerAtt INT,
        PlayerDef INT,
        PlayerMag INT,
        PlayerMdef INT,
        
        FOREIGN KEY(PlayerID) REFERENCES GameSave(SaveID)
);

DROP TABLE IF EXISTS [Npc];
CREATE TABLE [Npc] (
        NPCID INTEGER PRIMARY KEY AUTOINCREMENT,
        NPCHP INT,
        NPCMP INT,
        NPCAtt INT,
        NPCDef INT,
        NPCMag INT,
        NPCMdef INT
);
DROP TABLE IF EXISTS [Items];
CREATE TABLE [Items] (
        ItemID INT,
        ItemEffect VARCHAR(50)
);

INSERT INTO [Npc] (NPCHP, NPCMP, NPCAtt, NPCDef, NPCMag, NPCMdef) VALUES
(10, 5, 2, 2, 2, 2);

INSERT INTO [Npc] (NPCHP, NPCMP, NPCAtt, NPCDef, NPCMag, NPCMdef)VALUES
(20, 3, 7, 1, 1, 1);

INSERT INTO [Npc] (NPCHP, NPCMP, NPCAtt, NPCDef, NPCMag, NPCMdef)VALUES
(30, 4, 2, 2, 8, 3);";
        dbcmd.CommandText = databaseCreation;
        dbcmd.ExecuteNonQuery();
        init = true;
        dbcmd.Dispose();
        dbcon.Close();
    }


}
