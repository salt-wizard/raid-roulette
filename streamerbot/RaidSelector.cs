namespace streamerbot;
using static CPHNameSpace.CPHArgs; // Mimic arguments for inline CPH
using Streamer.bot.Plugin.Interface.Model;

/************************************************************************
* COPY AND PASTE BELOW CLASS INTO STREAMER.BOT 
* DO NOT INCLUDE ABOVE CODE!
************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Numerics;
using System.Net;

public class CPHInline
{
    private string ACTION_GROUP = "RS1";
    private string RAID_TABLE = "raid_targets";
    private bool DEBUG = false;

    public void Init(){
    }

	public bool Execute()
	{
		// your main code goes here
		return true;
	}

    public void Dispose(){
    }

    /**
     * A user suggests a raid target
     */
    public bool HandleRaidSuggestion(){
        PrintArgsVerbose();

        // The target should be the first input message
        string command = (string)args["command"];
        string target = (string)args["input0"];
        string user = (string)args["user"];
        string userName = (string)args["userName"];
        bool isFollowing = (bool)args["isFollowing"];
        if(command != null && command.Equals("!raid") && target != null){
            LogVerbose($"Target {target} being suggested by {userName}.");
            
            LogVerbose($"Validating if {userName} has been following long enough...");
            if(DEBUG || isFollowing){
                string followAgeDays = "0";
                if(!DEBUG){
                    followAgeDays = (string)args["followAgeDays"];
                }
                if(DEBUG || Int32.Parse(followAgeDays) > 31){
                    LogVerbose($"User {userName} has been following long enough.");
                    
                    LogVerbose("Getting extended information about target...");
                    TwitchUserInfoEx? twitchTarget = CPH.TwitchGetExtendedUserInfoByLogin(target);

                    if(twitchTarget != null){
                        LogVerbose("Details returned:");
                        LogVerbose(ObjToString(twitchTarget));

                        LogVerbose("Entering user into the database...");
                        InsertNewSuggestion(twitchTarget.UserId,twitchTarget.UserLogin,twitchTarget.UserName,twitchTarget.ProfileImageUrl);

                        // The target must also not be in the local raid variable....this needs to be validated.
                        bool targetExists = false;
                        string? localRaidDBString = CPH.GetGlobalVar<String>("localRaidDB", true);
                        if(localRaidDBString != null && localRaidDBString.Length > 0){
                            JArray localRaidDB = JArray.Parse(localRaidDBString);

                            // Search localRaidDB object if target has already been suggested
                            foreach(var item in localRaidDB){
                                JObject json = (JObject)item;
                                LogVerbose($"Checking the following json :: {json.ToString()}");
                                string? currId = (string)json["userId"];
                                if(currId != null && currId == twitchTarget.UserId){
                                    LogVerbose("Streamer was found!!! Not putting streamer in JSON.");
                                    targetExists = true;
                                }
                            }

                            // If target is not there, it must be added to the list.
                            if(!targetExists){
                                LogVerbose("Adding target to local variable...");
                                JObject jobj = new JObject(
                                    new JProperty("userId", twitchTarget.UserId),
                                    new JProperty("userLogin", twitchTarget.UserLogin),
                                    new JProperty("userName", twitchTarget.UserName),
                                    new JProperty("userPfp", twitchTarget.ProfileImageUrl),
                                    new JProperty("isBlacklisted", false),
                                    new JProperty("isOnline", false),
                                    new JProperty("raidCount", 0),
                                    new JProperty("lastRaidDate", ""),
                                    new JProperty("raidedByCount", 0),
                                    new JProperty("lastRaidedDate", ""),
                                    new JProperty("isVeto", false)
                                );
                                localRaidDB.Add(jobj);

                                LogVerbose($"Updated localRaidDB :: {JsonConvert.SerializeObject(localRaidDB)}");
                                CPH.SetGlobalVar("localRaidDB", JsonConvert.SerializeObject(localRaidDB), true);
                            }
                        } else {
                            // If the variable is null then we don't have it and need to load it...
                            LogVerbose("This is the first entry being made...creating variable!!!");
                            LoadRaidDB();
                        }

                    } else {
                        LogError($"There was an error trying to pull details about {userName} from Twitch. Exiting!");
                    }
                    
                } else {
                    LogVerbose($"User {userName} has not been following long enough. Exiting!");
                }
            } else {
                LogVerbose($"User {userName} is not following. Exiting!");
            }
        } else {
           LogVerbose("Either no raid target was specified, or there was a massive breakdown in logic. Exiting!");
        }

        return true;
    }

    /** 
     * Sends the local DB JSON to the UI for rendering
     */
    public bool UpdateUI(){
        // Send list to UI via websocket
        string? sessionId = CPH.GetGlobalVar<string>("raidRoluetteSessionId", true);
        int wssIdx = CPH.GetGlobalVar<int>("raidRoluetteWssIdx", true);
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);

        // Broadcast latest user json
        JObject jobj = new JObject(
            new JProperty("action", "update"),
            new JProperty("users", localRaidDBString)
        );
        CPH.WebsocketCustomServerBroadcast(JsonConvert.SerializeObject(jobj), sessionId, wssIdx);

        // Provide updated authtoken
        jobj = new JObject(
            new JProperty("action", "token"),
            new JProperty("clientId", CPH.TwitchClientId),
            new JProperty("token", CPH.TwitchOAuthToken)
        );
        CPH.WebsocketCustomServerBroadcast(JsonConvert.SerializeObject(jobj), sessionId, wssIdx);

        return true;
    }

    /**
     * Start a raid via the UI.
     * The target being sent over should be either RANDOM (anyone in the list that is online and not blacklisted can be raided),
     * or a specific userId is sent over
     **/
    public bool StartRaid(){
        LogVerbose("=============================");
        LogVerbose($"ENTER StartRaid");
        LogVerbose("=============================");
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);
        
        PrintArgsVerbose();
        
        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);

        string action = (string)dataObj["action"];
        string userId = (string)dataObj["target"];
        if(action != null && action.Equals("raid")){
            if(userId.Equals("RANDOM")){
                // Get the list of all userIds that have been saved
                if(localRaidDBString != null && localRaidDBString.Length > 0){
                    JArray localRaidDB = JArray.Parse(localRaidDBString);
                    List<string> userIds = new();
                    foreach(var item in localRaidDB){
                        JObject json = (JObject)item;
                        if((bool)json["isOnline"] == true && (bool)json["isBlacklisted"] == false && (bool)json["isVeto"] == false){
                            LogVerbose($"Adding userId {(string)json["userId"]}");
                            userIds.Add((string)json["userId"]);
                        }
                    }

                    // Pick a random result
                    Random rand = new();
                    int randIndex = rand.Next(0,userIds.Count);
                    LogVerbose($"Random value selected :: {randIndex}");

                    userId = userIds[randIndex];
                    LogVerbose($"Random selection has chosen the following!!! :: {userId}");
                }
            }

            // Check if user being raided is already added
            if(localRaidDBString != null && localRaidDBString.Length > 0){
                JArray localRaidDB = JArray.Parse(localRaidDBString);
                JArray updatedDB = new JArray();
                foreach(var item in localRaidDB){
                    JObject json = (JObject)item;
                    LogVerbose($"Checking the following json :: {json.ToString()}");
                    string? currId = (string)json["userId"];
                    if(currId != null && currId == userId){
                        LogVerbose("Streamer was found.");

                        // Save a backup of the target being raided
                        CPH.SetGlobalVar("localRaidDBBackup", JsonConvert.SerializeObject(localRaidDB), true);

                        // If the user exists, update raidCount and update lastRaidDate
                        int raidCount = (int)json["raidCount"];
                        json["raidCount"] = raidCount + 1;
                        // https://stackoverflow.com/a/6121309
                        var dateAndTime = DateTime.Now;
                        var date = dateAndTime.Date;
                        json["lastRaidDate"] = date.ToString();

                        updatedDB.Add(json);

                        // Update database
                        LogVerbose($"Current raid count for user....{raidCount}");
                        UpdateRaidDetails(userId, raidCount + 1, date.ToString());
                        LogVerbose("Raid details have been updated...");
                    } else {
                        updatedDB.Add(json);
                    }
                }

                CPH.SetGlobalVar("localRaidDB", JsonConvert.SerializeObject(updatedDB), true);
            }

            LogVerbose($"Starting raiding into userId = {userId}");
            CPH.TwitchStartRaidById(userId);
        }
        LogVerbose("=============================");
        LogVerbose($"Exit StartRaid");
        LogVerbose("=============================");
        return true;
    }

    public bool CancelRaid(){
        LogVerbose("=============================");
        LogVerbose($"ENTER CancelRaid");
        LogVerbose("=============================");

        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];

        if(action.Equals("cancel")){
            // Immediately cancel the raid in progress
            CPH.TwitchCancelRaid();
            
            // Restore the backup
            string? localRaidDBBackup = CPH.GetGlobalVar<string>("localRaidDBBackup", true);
            CPH.SetGlobalVar("localRaidDB", localRaidDBBackup, true);

            // Update the database details
            if(localRaidDBBackup != null && localRaidDBBackup.Length > 0){
                JArray localRaidDB = JArray.Parse(localRaidDBBackup);
                foreach(var item in localRaidDB){
                    JObject json = (JObject)item;
                    LogVerbose($"Checking the following json :: {json.ToString()}");
                    UpdateRaidDetails((string)json["userId"], (int)json["raidCount"], (string)json["lastRaidDate"]);
                }
            }
        }

        LogVerbose("=============================");
        LogVerbose($"EXIT CancelRaid");
        LogVerbose("=============================");
        return true;
    }

    public bool BlacklistUser(){
        LogVerbose("=============================");
        LogVerbose($"ENTER BlacklistUser");
        LogVerbose("=============================");
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);

        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];
        string target = (string)dataObj["target"];

        if(action != null && action.Equals("blacklist")){
            // Update the database
            UpdateUserBlacklistStatus(target, true);
            LoadRaidDB();
        }
        
        LogVerbose("=============================");
        LogVerbose($"ENTER BlacklistUser");
        LogVerbose("=============================");
        return true;
    }

    public bool VetoUser(){
        LogVerbose("=============================");
        LogVerbose($"ENTER VetoUser");
        LogVerbose("=============================");
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);

        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];
        string target = (string)dataObj["target"];

        if(action != null && action.Equals("veto")){
            // Update the database
            UpdateUserVetoStatus(target, true);
            LoadRaidDB();
        }
        
        LogVerbose("=============================");
        LogVerbose($"ENTER VetoUser");
        LogVerbose("=============================");
        return true;
    }

    public bool UndoBlacklistUser(){
        LogVerbose("=============================");
        LogVerbose($"ENTER UndoBlacklistUser");
        LogVerbose("=============================");
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);

        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];
        string target = (string)dataObj["target"];

        if(action != null && action.Equals("unblacklist")){
            // Update the database
            UpdateUserBlacklistStatus(target, false);
            LoadRaidDB();
        }
        
        LogVerbose("=============================");
        LogVerbose($"ENTER UndoBlacklistUser");
        LogVerbose("=============================");
        return true;
    }

    public bool UndoVetoUser(){
        LogVerbose("=============================");
        LogVerbose($"ENTER UndoVetoUser");
        LogVerbose("=============================");
        string? localRaidDBString = CPH.GetGlobalVar<string>("localRaidDB", true);

        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];
        string target = (string)dataObj["target"];

        if(action != null && action.Equals("unveto")){
            // Update the database
            UpdateUserVetoStatus(target, false);
            LoadRaidDB();
        }
        
        LogVerbose("=============================");
        LogVerbose($"ENTER UndoVetoUser");
        LogVerbose("=============================");
        return true;
    }

    public bool RequestRender(){
        LogVerbose("=============================");
        LogVerbose($"ENTER RequestRender");
        LogVerbose("=============================");
        string data = (string)args["data"];
        JObject dataObj = JObject.Parse(data);
        string action = (string)dataObj["action"];

        if(action != null && action.Equals("render")){
            UpdateUI();
        }
        
        LogVerbose("=============================");
        LogVerbose($"ENTER RequestRender");
        LogVerbose("=============================");
        return true;
    }

    /**
    * Runs through the local DB list and validates if users are online. If the status is changed then the database table
    * needs to have its entry updated.
    */
    public bool UpdateOnlineStatus(){
        LogVerbose("=============================");
        LogVerbose($"ENTER UpdateOnlineStatus");
        LogVerbose("=============================");

        // Load the local RaidDB model
        string? localRaidDBString = CPH.GetGlobalVar<String>("localRaidDB", true);
        if(localRaidDBString != null && localRaidDBString.Length > 0){
            JArray localRaidDB = JArray.Parse(localRaidDBString);
            JArray newDB = new JArray();
            foreach(var item in localRaidDB){
                JObject json = (JObject)item;
                LogVerbose($"Checking the following json :: {json.ToString()}");
                string? currId = (string)json["userId"];
                string? currLogin = (string)json["userLogin"];
                bool? currIsOnline = (bool)json["isOnline"];
                if(currLogin != null && currId != null){
                    bool online = IsTargetOnline(currLogin);
                    if(online != currIsOnline){
                        // Update status for local and DB
                        json["isOnline"] = online;

                        // UPDATE TARGET IN DB
                        UpdateUserOnlineStatus(currId, online);
                    } else {
                        LogVerbose("Status did not change...");
                    }
                } else {
                    LogError("There was an error trying to get a user login!");
                }
                newDB.Add(json);
            }

            if(newDB.Count > 0){
                LogVerbose($"Updated local DB variable :: {JsonConvert.SerializeObject(newDB)}");
                CPH.SetGlobalVar("localRaidDB", JsonConvert.SerializeObject(newDB), true);
            }
        }

        LogVerbose("=============================");
        LogVerbose($"EXIT UpdateOnlineStatus");
        LogVerbose("=============================");
        return true;
    }

    /**
     * Validates if the raid target is online. 
     **/
    public bool IsTargetOnline(string target)
    {
        LogVerbose("=============================");
        LogVerbose($"ENTER IsTargetOnline");
        LogVerbose("=============================");
        LogVerbose($"Check if target {target} is online...");
        var response = new WebClient().DownloadString($"https://www.twitch.tv/{target}");
        //LogVerbose($"Response from {target} :: {response}");
        bool targetOnline = response.Contains("\"isLiveBroadcast\":true");
        if (targetOnline)
        {
            LogVerbose($"Target {target} is online!");
        } else
        {
            LogVerbose($"Target {target} is offline!");
        }

        LogVerbose("=============================");
        LogVerbose($"EXIT IsTargetOnline");
        LogVerbose("=============================");
        return targetOnline;
    }

    /************************************************************************
     * DATABASE FUNCTIONS
     ************************************************************************/
    public bool InitRaidDB(){
        LogVerbose("=============================");
        LogVerbose($"ENTER InitRaidDB");
        LogVerbose("=============================");
        
        // On start, check if debug mode is to be enabled
        bool? debug = CPH.GetGlobalVar<bool>("RR_DEBUG", true);
        if(debug.HasValue){
            DEBUG = debug.Value;
        }
        LogInfo($"INITIALIZING RAID ROULETTE....DEBUG MODE IS SET TO {DEBUG}!");
        
        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"CREATE TABLE IF NOT EXISTS {RAID_TABLE} (
                            userId VARCHAR(20) NOT NULL, 
                            userLogin VARCHAR(30) NOT NULL,
                            userName VARCHAR(30) NOT NULL,
                            userPfp VARCHAR(255) NOT NULL,
                            isBlacklisted BOOLEAN NOT NULL, 
                            isOnline BOOLEAN NOT NULL, 
                            raidCount INT NOT NULL, 
                            lastRaidDate DATE, 
                            raidedByCount INT NOT NULL, 
                            lastRaidedDate DATE
                        );";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.ExecuteNonQuery();

        sql = $@"CREATE UNIQUE INDEX IF NOT EXISTS userIdInd ON {RAID_TABLE} (userId);";
        command = new SQLiteCommand(sql, sql_db);
        command.ExecuteNonQuery();

        // 2025-02-08 - Adding new row for keeping track of veto.
        // This column should always be false on startup
        try{
            sql = $"ALTER TABLE {RAID_TABLE} ADD COLUMN isVeto BOOLEAN NOT NULL DEFAULT FALSE";
            command = new SQLiteCommand(sql, sql_db);
            command.ExecuteNonQuery();
        } catch {
            LogVerbose("Column already exists, continuing.");
        }

        sql = $"UPDATE {RAID_TABLE} SET isVeto = false";
        command = new SQLiteCommand(sql, sql_db);
        command.ExecuteNonQuery();

        sql_db.Close();

        LogVerbose("=============================");
        LogVerbose($"EXIT InitRaidDB");
        LogVerbose("=============================");
        return true;
    }

    public bool LoadRaidDB(){
        LogVerbose("=============================");
        LogVerbose($"ENTER LoadRaidDB");
        LogVerbose("=============================");

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"SELECT * FROM {RAID_TABLE};";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        SQLiteDataReader reader = command.ExecuteReader();

        JArray jarray = new JArray();
        if(reader.HasRows)
        {
            LogVerbose("Rows were returned.");
            while(reader.Read()){
                string userId = reader.GetString(0);
                string userLogin = reader.GetString(1);
                string userName = reader.GetString(2);
                string userPfp = reader.GetString(3);
                bool isBlacklisted = reader.GetBoolean(4);
                bool isOnline = reader.GetBoolean(5);
                int raidCount = reader.GetInt32(6);
                string lastRaidDate = SafeGetString(reader, 7);
                int raidedByCount = reader.GetInt32(8);
                string lastRaidedDate = SafeGetString(reader, 9);
                bool isVeto = reader.GetBoolean(10);
                
                JObject jobj = new JObject(
                    new JProperty("userId", userId),
                    new JProperty("userLogin", userLogin),
                    new JProperty("userName", userName),
                    new JProperty("userPfp", userPfp),
                    new JProperty("isBlacklisted", isBlacklisted),
                    new JProperty("isOnline", isOnline),
                    new JProperty("raidCount", raidCount),
                    new JProperty("lastRaidDate", lastRaidDate),
                    new JProperty("raidedByCount", raidedByCount),
                    new JProperty("lastRaidedDate", lastRaidedDate),
                    new JProperty("isVeto", isVeto)
                );
                jarray.Add(jobj);
            }
        } 
        
        LogVerbose($"Final result from DB :: {JsonConvert.SerializeObject(jarray)}");
        CPH.SetGlobalVar("localRaidDB", JsonConvert.SerializeObject(jarray), true);

        LogVerbose("=============================");
        LogVerbose($"EXIT LoadRaidDB");
        LogVerbose("=============================");
        return true;
    }

    private bool InsertNewSuggestion(string userId, string userLogin, string userName, string userPfp){
        LogVerbose("=============================");
        LogVerbose($"ENTER InsertSuggestion");
        LogVerbose("=============================");
        bool inserted = false;

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();

        string sql = $@"INSERT OR IGNORE INTO {RAID_TABLE} 
                            (userId, userLogin, userName, userPfp, isBlacklisted, isOnline, raidCount, raidedByCount) 
                            VALUES (@userId,@userLogin,@userName,@userPfp,FALSE,FALSE,0,0);";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.Parameters.AddWithValue(@"userId", userId);
        command.Parameters.AddWithValue(@"userLogin", userLogin);
        command.Parameters.AddWithValue(@"userName",userName);
        command.Parameters.AddWithValue(@"userPfp",userPfp);
        command.ExecuteNonQuery();

        LogVerbose("=============================");
        LogVerbose($"EXIT InsertSuggestion");
        LogVerbose("=============================");
        return inserted;
    }

    private void UpdateUserOnlineStatus(string userId, bool isOnline){
        LogVerbose("=============================");
        LogVerbose($"ENTER UpdateUserOnlineStatus");
        LogVerbose("=============================");

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"UPDATE {RAID_TABLE} SET isOnline = @isOnline WHERE userId = @userId";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.Parameters.AddWithValue(@"isOnline", isOnline);
        command.Parameters.AddWithValue(@"userId", userId);
        command.ExecuteNonQuery();

        LogVerbose("=============================");
        LogVerbose($"EXIT UpdateUserOnlineStatus");
        LogVerbose("=============================");
    }

    private void UpdateUserBlacklistStatus(string userId, bool isBlacklisted){
        LogVerbose("=============================");
        LogVerbose($"ENTER UpdateUserBlacklistStatus");
        LogVerbose("=============================");

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"UPDATE {RAID_TABLE} SET isBlacklisted = @isBlacklisted WHERE userId = @userId";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.Parameters.AddWithValue(@"isBlacklisted", isBlacklisted);
        command.Parameters.AddWithValue(@"userId", userId);
        command.ExecuteNonQuery();

        LogVerbose("=============================");
        LogVerbose($"EXIT UpdateUserBlacklistStatus");
        LogVerbose("=============================");
    }

    private void UpdateUserVetoStatus(string userId, bool isVeto){
        LogVerbose("=============================");
        LogVerbose($"ENTER UpdateUserVetoStatus");
        LogVerbose("=============================");

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"UPDATE {RAID_TABLE} SET isVeto = @isVeto WHERE userId = @userId";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.Parameters.AddWithValue(@"isVeto", isVeto);
        command.Parameters.AddWithValue(@"userId", userId);
        command.ExecuteNonQuery();

        LogVerbose("=============================");
        LogVerbose($"EXIT UpdateUserVetoStatus");
        LogVerbose("=============================");
    }

    private void UpdateRaidDetails(string userId, int raidCount, string lastRaidDate){
        LogVerbose("=============================");
        LogVerbose($"ENTER UpdateRaidDetails");
        LogVerbose("=============================");

        string? file = CPH.GetGlobalVar<string>("raidDBFile", true);
        string path = $@"Data Source={file}";
        SQLiteConnection sql_db = new SQLiteConnection(path);
        sql_db.Open();
        string sql = $@"UPDATE {RAID_TABLE} SET raidCount = @raidCount, lastRaidDate = @lastRaidDate WHERE userId = @userId";
        SQLiteCommand command = new SQLiteCommand(sql, sql_db);
        command.Parameters.AddWithValue(@"raidCount", raidCount);
        command.Parameters.AddWithValue(@"lastRaidDate", lastRaidDate);
        command.Parameters.AddWithValue(@"userId", userId);
        command.ExecuteNonQuery();

        LogVerbose("=============================");
        LogVerbose($"EXIT UpdateRaidDetails");
        LogVerbose("=============================");
    }

	/************************************************************************
	 * HELPER FUNCTIONS BELOW
	 ************************************************************************/
    private void LogVerbose(string msg){
        CPH.LogVerbose($"{ACTION_GROUP} :: {msg}");
    }

    private void LogDebug(string msg){
        CPH.LogDebug($"{ACTION_GROUP} :: {msg}");
    }

    private void LogInfo(string msg){
        CPH.LogInfo($"{ACTION_GROUP} :: {msg}");
    }

    private void LogWarn(string msg){
        CPH.LogWarn($"{ACTION_GROUP} :: {msg}");
    }

    private void LogError(string msg){
        CPH.LogError($"{ACTION_GROUP} :: {msg}");
    }

	public void PrintArgsVerbose()
    {
        LogVerbose($"Arguments being passed in...");
        foreach (var arg in args)
        {
            LogVerbose($"{arg.Key} :: {arg.Value}");
        }
    }

	public string ObjToString(object obj)
    {
        return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });
    }


    /**
     *
     */
    public static string SafeGetString(SQLiteDataReader reader, int colIndex)
    {
    if(!reader.IsDBNull(colIndex))
        return reader.GetString(colIndex);
        return string.Empty;
    }

}