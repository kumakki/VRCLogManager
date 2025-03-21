namespace VRCLogManager;

public static class MST
{
    public static string locallow = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low";
    public static string vlmLocal = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\VRCLogManager";
    public static string dbFile = MST.vlmLocal + "\\VRCLogManager.db";
    public static string connectionString = "Data Source=" + dbFile;
    public static string createTable_JoinLeave = "CREATE TABLE IF NOT EXISTS JoinLeave (PlayerID text, JoinLeave int, WorldID text, LogDate int, LogTime int, AccountID text, PRIMARY KEY (PlayerID, JoinLeave, WorldID, LogDate, LogTime, AccountID))";
    public static string createTable_World = "CREATE TABLE IF NOT EXISTS World (ID text, Name text, Number int)";
    public static string createTable_Player = "CREATE TABLE IF NOT EXISTS Player (ID text, Name text, Number int)";
    public static string createTable_MyAccount = "CREATE TABLE IF NOT EXISTS MyAccount (ID text, Name text, Number int)";
}