using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using Microsoft.Data.Sqlite;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;


namespace VRCLogManager;

public static class DB
{
    static string connectionString = MST.connectionString;

    public static void DBCheck()
    {
        //DBの存在を確認、無ければテーブルとともに作成

        //フォルダー存在チェック
        if (Directory.Exists(MST.vlmLocal) == false)
        {
            //無かったら作成
            Directory.CreateDirectory(MST.vlmLocal);
        }

        //DB存在チェック、無かったら作る
        using (var connection = new SqliteConnection(MST.connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = MST.createTable_JoinLeave;
            command.ExecuteNonQuery();
            command.CommandText = MST.createTable_World;
            command.ExecuteNonQuery();
            command.CommandText = MST.createTable_Player;
            command.ExecuteNonQuery();
            command.CommandText = MST.createTable_MyAccount;
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    public static ObservableCollection<string> GetTables()
    {
        ObservableCollection<string> buf = new ObservableCollection<string>();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table'";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(reader.GetString(0));
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static List<string> GetTableColumns(string table)
    {
        List<string> buf = new List<string>();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "PRAGMA TABLE_INFO('" + table + "')";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(reader.GetString(1));
                }
                while (buf.Count < 10)
                {
                    buf.Add("");
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static ObservableCollection<DBDataRecord> GetAllData(string table, int columnCount)
    {
        ObservableCollection<DBDataRecord> buf = new ObservableCollection<DBDataRecord>();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM " + table;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(new DBDataRecord());
                    if (columnCount > 0)
                    {
                        buf[buf.Count - 1].Column1 = reader.GetString(0);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column1 = "";
                    }
                    if (columnCount > 1)
                    {
                        buf[buf.Count - 1].Column2 = reader.GetString(1);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column2 = "";
                    }
                    if (columnCount > 2)
                    {
                        buf[buf.Count - 1].Column3 = reader.GetString(2);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column3 = "";
                    }
                    if (columnCount > 3)
                    {
                        buf[buf.Count - 1].Column4 = reader.GetString(3);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column4 = "";
                    }
                    if (columnCount > 4)
                    {
                        buf[buf.Count - 1].Column5 = reader.GetString(4);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column5 = "";
                    }
                    if (columnCount > 5)
                    {
                        buf[buf.Count - 1].Column6 = reader.GetString(5);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column6 = "";
                    }
                    if (columnCount > 6)
                    {
                        buf[buf.Count - 1].Column7 = reader.GetString(6);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column7 = "";
                    }
                    if (columnCount > 7)
                    {
                        buf[buf.Count - 1].Column8 = reader.GetString(7);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column8 = "";
                    }
                    if (columnCount > 8)
                    {
                        buf[buf.Count - 1].Column9 = reader.GetString(8);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column9 = "";
                    }
                    if (columnCount > 9)
                    {
                        buf[buf.Count - 1].Column10 = reader.GetString(9);
                    }
                    else
                    {
                        buf[buf.Count - 1].Column10 = "";
                    }
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static int GetColumnWidth(string columnName)
    {
        int buf = 0;
        switch (columnName)
        {
            case "ID":
                buf = 40;
                break;
            case "PlayerID":
                buf = 50;
                break;
            case "PlayerName":
                buf = 65;
                break;
            case "WorldID":
                buf = 50;
                break;
            case "WorldName":
                buf = 150;
                break;
            case "JoinLeave":
                buf = 60;
                break;
            case "LogDate":
                buf = 65;
                break;
            case "LogTime":
                buf = 65;
                break;
            case "MyAccountID":
                buf = 80;
                break;
            case "MyAccountName":
                buf = 80;
                break;
            case "Name":
                buf = 200;
                break;
            case "Number":
                buf = 55;
                break;
            case "":
                buf = 0;
                break;
            default:
                buf = 20;
                break;
        }


        return buf;
    }

    public static ObservableCollection<PlayerData> GetPlayerDatas()
    {
        ObservableCollection<PlayerData> buf = new ObservableCollection<PlayerData>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID, Name, MAX(Number) FROM Player GROUP BY ID ORDER BY Name";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(new PlayerData() { PlayerID = reader.GetString(0), PlayerName = reader.GetString(1) });
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static ObservableCollection<WorldData> GetWorldDatas()
    {
        ObservableCollection<WorldData> buf = new ObservableCollection<WorldData>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID, Name, MAX(Number) FROM World GROUP BY ID ORDER BY Name";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(new WorldData() { WorldID = reader.GetString(0), WorldName = reader.GetString(1) });
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static ObservableCollection<AccountData> GetAccountDatas()
    {
        ObservableCollection<AccountData> buf = new ObservableCollection<AccountData>();

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID, Name, MAX(Number) FROM MyAccount GROUP BY ID ORDER BY Name";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    buf.Add(new AccountData() { AccountID = reader.GetString(0), AccountName = reader.GetString(1) });
                }
            }
            connection.Close();
        }
        return buf;
    }

    public static void SetData(string id, string name, int type)
    {
        string dataType = "";
        switch (type)
        {
            case 0:
                dataType = "MyAccount";
                break;
            case 1:
                dataType = "Player";
                break;
            case 2:
                dataType = "World";
                break;
            default:
                return;
        }
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT ID, Name, Number FROM " + dataType + " WHERE ID = @id ORDER BY Number";
            command.Parameters.AddWithValue("@id", id);
            bool hasRecord = false;
            string bufID = "";
            string bufName = "";
            int bufNumber = 0;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    bufID = reader.GetString(0);
                    bufName = reader.GetString(1);
                    bufNumber = reader.GetInt32(2);
                    hasRecord = true;
                }
            }

            if (hasRecord == false)
            {
                //データが存在しなければ新規挿入する
                command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO " + dataType + " (ID, Name, Number) VALUES (@id, @name, 0)";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
            else if (bufName != name)
            {
                //データが存在し、最終名と違う場合は新規採番し挿入する
                command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO " + dataType + " (ID, Name, Number) VALUES (@id, @name, @number)";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@number", bufID + 1);
                command.ExecuteNonQuery();
            }
            //データは存在するが最終名と同じ場合は何もしない

            connection.Close();
        }
    }

    public static void SetJoinLeave(string playerID, int joinleave, string worldID, int logDate, int logTime, string accountID)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM JoinLeave WHERE PlayerID = @playerid AND JoinLeave = @joinleave AND WorldID = @worldid AND "
                                + "LogDate = @logdate AND LogTime = @logtime AND AccountID = @accountid";
            command.Parameters.AddWithValue("@playerid", playerID);
            command.Parameters.AddWithValue("@joinleave", joinleave);
            command.Parameters.AddWithValue("@worldid", worldID);
            command.Parameters.AddWithValue("@logdate", logDate);
            command.Parameters.AddWithValue("@logtime", logTime);
            command.Parameters.AddWithValue("@accountid", accountID);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    //全く同じデータがすでにあったら追加しない
                    return;
                }
            }

            command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO JoinLeave (PlayerID, JoinLeave, WorldID, LogDate, LogTime, AccountID) VALUES (@playerid, @joinleave, @worldid, @logdate, @logtime, @accountid)";
            command.Parameters.AddWithValue("@playerid", playerID);
            command.Parameters.AddWithValue("@joinleave", joinleave);
            command.Parameters.AddWithValue("@worldid", worldID);
            command.Parameters.AddWithValue("@logdate", logDate);
            command.Parameters.AddWithValue("@logtime", logTime);
            command.Parameters.AddWithValue("@accountid", accountID);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public static void AllTruncateTables()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM JoinLeave";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM MyAccount";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM Player";
            command.ExecuteNonQuery();
            command.CommandText = "DELETE FROM World";
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

public class DBDataRecord : INotifyPropertyChanged
{
    private string? _Column1;
    public string? Column1
    {
        get { return _Column1; }
        set
        {
            if (_Column1 != value)
            {
                _Column1 = value;
                OnPropertyChanged(nameof(Column1));
            }
        }
    }

    private string? _Column2;
    public string? Column2
    {
        get { return _Column2; }
        set
        {
            if (_Column2 != value)
            {
                _Column2 = value;
                OnPropertyChanged(nameof(Column2));
            }
        }
    }

    private string? _Column3;
    public string? Column3
    {
        get { return _Column3; }
        set
        {
            if (_Column3 != value)
            {
                _Column3 = value;
                OnPropertyChanged(nameof(Column3));
            }
        }
    }

    private string? _Column4;
    public string? Column4
    {
        get { return _Column4; }
        set
        {
            if (_Column4 != value)
            {
                _Column4 = value;
                OnPropertyChanged(nameof(Column4));
            }
        }
    }

    private string? _Column5;
    public string? Column5
    {
        get { return _Column5; }
        set
        {
            if (_Column5 != value)
            {
                _Column5 = value;
                OnPropertyChanged(nameof(Column5));
            }
        }
    }

    private string? _Column6;
    public string? Column6
    {
        get { return _Column6; }
        set
        {
            if (_Column6 != value)
            {
                _Column6 = value;
                OnPropertyChanged(nameof(Column6));
            }
        }
    }

    private string? _Column7;
    public string? Column7
    {
        get { return _Column7; }
        set
        {
            if (_Column7 != value)
            {
                _Column7 = value;
                OnPropertyChanged(nameof(Column7));
            }
        }
    }

    private string? _Column8;
    public string? Column8
    {
        get { return _Column8; }
        set
        {
            if (_Column8 != value)
            {
                _Column8 = value;
                OnPropertyChanged(nameof(Column8));
            }
        }
    }

    private string? _Column9;
    public string? Column9
    {
        get { return _Column9; }
        set
        {
            if (_Column9 != value)
            {
                _Column9 = value;
                OnPropertyChanged(nameof(Column9));
            }
        }
    }

    private string? _Column10;
    public string? Column10
    {
        get { return _Column10; }
        set
        {
            if (_Column10 != value)
            {
                _Column10 = value;
                OnPropertyChanged(nameof(Column10));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}