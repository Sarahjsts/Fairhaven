using Mono.Data.SqliteClient;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int[] enemyStats = new int[6];
    // Start is called before the first frame update
    void Start()
    {
        InitStats();
        Enemy.HP = enemyStats[0];
        Enemy.MP = enemyStats[1];
        Enemy.Att = enemyStats[2];
        Enemy.Def = enemyStats[3];
        Enemy.Mag = enemyStats[4];
        Enemy.Mdef = enemyStats[5];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void InitStats()
    {

        string connectionString = "URI=file:" + Application.persistentDataPath + "game.db";
        IDbConnection dbcon = new SqliteConnection(connectionString);
        dbcon.Open();
        IDbCommand dbcmd = dbcon.CreateCommand(); ;
        IDataReader reader = dbcmd.ExecuteReader();
        string enemyName = name;
        string stats = "";
        switch (enemyName)
        {
            case "Skeleton":
            stats = "SELECT * FROM [NPC] WHERE NPCID = 1";
            break;

            case "Bandit":
            stats = "SELECT * FROM [NPC] WHERE NPCID = 2";
            break;

            case "Magician":
            stats = "SELECT * FROM [NPC] WHERE NPCID = 3";
            break;

        }
            
    
    
            dbcmd.CommandText = stats;
            reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < stats.Length; i++)
                {
                    enemyStats[i] = reader.GetInt32(i + 1);
                }
            }
            reader.Dispose();
            dbcmd.Dispose();
            dbcon.Close();
        }
    }

