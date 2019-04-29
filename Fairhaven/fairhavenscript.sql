DROP TABLE IF EXISTS [GameSave];
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
(30, 4, 2, 2, 8, 3);