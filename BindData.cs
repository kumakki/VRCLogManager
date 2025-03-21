using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection.Metadata;
using VRCLogManager.Pages;

namespace VRCLogManager;

public class BD_MainWindow : INotifyPropertyChanged
{
  private string? _WindowVisible;
  public string? WindowVisible
  {
    get { return _WindowVisible; }
    set
    {
      if (_WindowVisible != value)
      {
        _WindowVisible = value;
        OnPropertyChanged(nameof(WindowVisible));
      }
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}

public class BD_MainPage : INotifyPropertyChanged
{
  private DateTime? _StartDate;
  public DateTime? StartDate
  {
    get { return _StartDate; }
    set
    {
      if (_StartDate != value)
      {
        _StartDate = value;
        OnPropertyChanged(nameof(StartDate));
      }
    }
  }
  
  private DateTime? _EndDate;
  public DateTime? EndDate
  {
    get { return _EndDate; }
    set
    {
      if (_EndDate != value)
      {
        _EndDate = value;
        OnPropertyChanged(nameof(EndDate));
      }
    }
  }
  
  private bool _PlayerExtraMatch;
  public bool PlayerExtraMatch
  {
    get { return _PlayerExtraMatch; }
    set
    {
      if (_PlayerExtraMatch != value)
      {
        _PlayerExtraMatch = value;
        OnPropertyChanged(nameof(PlayerExtraMatch));
      }
    }
  }
  
  private bool _WorldExtraMatch;
  public bool WorldExtraMatch
  {
    get { return _WorldExtraMatch; }
    set
    {
      if (_WorldExtraMatch != value)
      {
        _WorldExtraMatch = value;
        OnPropertyChanged(nameof(WorldExtraMatch));
      }
    }
  }
  
  private string? _PlayerName;
  public string? PlayerName
  {
    get { return _PlayerName; }
    set
    {
      if (_PlayerName != value)
      {
        _PlayerName = value;
        OnPropertyChanged(nameof(PlayerName));
      }
    }
  }
  
  private string? _WorldName;
  public string? WorldName
  {
    get { return _WorldName; }
    set
    {
      if (_WorldName != value)
      {
        _WorldName = value;
        OnPropertyChanged(nameof(WorldName));
      }
    }
  }
  
  private string? _UserName;
  public string? UserName
  {
    get { return _UserName; }
    set
    {
      if (_UserName != value)
      {
        _UserName = value;
        OnPropertyChanged(nameof(UserName));
      }
    }
  }
  
  private string? _PlayerID;
  public string? PlayerID
  {
    get { return _PlayerID; }
    set
    {
      if (_PlayerID != value)
      {
        _PlayerID = value;
        OnPropertyChanged(nameof(PlayerID));
      }
    }
  }
  
  private string? _WorldID;
  public string? WorldID
  {
    get { return _WorldID; }
    set
    {
      if (_WorldID != value)
      {
        _WorldID = value;
        OnPropertyChanged(nameof(WorldID));
      }
    }
  }
  
  private ObservableCollection<PlayerData>? _PlayerList;
  public ObservableCollection<PlayerData>? PlayerList
  {
    get { return _PlayerList; }
    set
    {
      if (_PlayerList != value)
      {
        _PlayerList = value;
        OnPropertyChanged(nameof(PlayerList));
      }
    }
  }
  
  private ObservableCollection<WorldData>? _WorldList;
  public ObservableCollection<WorldData>? WorldList
  {
    get { return _WorldList; }
    set
    {
      if (_WorldList != value)
      {
        _WorldList = value;
        OnPropertyChanged(nameof(WorldList));
      }
    }
  }
  
  private PlayerData? _SelectedItem_Player;
  public PlayerData? SelectedItem_Player
  {
    get { return _SelectedItem_Player; }
    set
    {
      if (_SelectedItem_Player != value)
      {
        _SelectedItem_Player = value;
        OnPropertyChanged(nameof(SelectedItem_Player));
      }
    }
  }
  
  private WorldData? _SelectedItem_World;
  public WorldData? SelectedItem_World
  {
    get { return _SelectedItem_World; }
    set
    {
      if (_SelectedItem_World != value)
      {
        _SelectedItem_World = value;
        OnPropertyChanged(nameof(SelectedItem_World));
      }
    }
  }
  
  private bool _ShowID;
  public bool ShowID
  {
    get { return _ShowID; }
    set
    {
      if (_ShowID != value)
      {
        _ShowID = value;
        OnPropertyChanged(nameof(ShowID));
      }
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}

public class BD_DBMaintenance : INotifyPropertyChanged
{
  private ObservableCollection<string>? _TableLists;
  public ObservableCollection<string>? TableLists
  {
    get { return _TableLists; }
    set
    {
      if (_TableLists != value)
      {
        _TableLists = value;
        OnPropertyChanged(nameof(TableLists));
      }
    }
  }

  private string? _SelectedItem;
  public string? SelectedItem
  {
    get { return _SelectedItem; }
    set
    {
      if (_SelectedItem != value)
      {
        _SelectedItem = value;
        OnPropertyChanged(nameof(SelectedItem));
      }
    }
  }

  private int? _SelectedIndex;
  public int? SelectedIndex
  {
    get { return _SelectedIndex; }
    set
    {
      if (_SelectedIndex != value)
      {
        _SelectedIndex = value;
        OnPropertyChanged(nameof(SelectedIndex));
      }
    }
  }

  private bool _LoadEnable;
  public bool LoadEnable
  {
    get { return _LoadEnable; }
    set
    {
      if (_LoadEnable != value)
      {
        _LoadEnable = value;
        OnPropertyChanged(nameof(LoadEnable));
      }
    }
  }
  private DBDataRecord? _DBDataColumn;
  public DBDataRecord? DBDataColumn
  {
    get { return _DBDataColumn; }
    set
    {
      if (_DBDataColumn != value)
      {
        _DBDataColumn = value;
        OnPropertyChanged(nameof(DBDataColumn));
      }
    }
  }

  private ObservableCollection<DBDataRecord>? _DBDataRecords;
  public ObservableCollection<DBDataRecord>? DBDataRecords
  {
    get { return _DBDataRecords; }
    set
    {
      if (_DBDataRecords != value)
      {
        _DBDataRecords = value;
        OnPropertyChanged(nameof(DBDataRecords));
      }
    }
  }


  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}

public class PlayerData : INotifyPropertyChanged
{
  private string? _PlayerID;
  public string? PlayerID
  {
    get { return _PlayerID; }
    set
    {
      if (_PlayerID!= value)
      {
        _PlayerID = value;
        OnPropertyChanged(nameof(PlayerID));
      }
    }
  }

  private string? _PlayerName;
  public string? PlayerName
  {
    get { return _PlayerName; }
    set
    {
      if (_PlayerName != value)
      {
        _PlayerName = value;
        OnPropertyChanged(nameof(PlayerName));
      }
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}

public class WorldData : INotifyPropertyChanged
{
  private string? _WorldID;
  public string? WorldID
  {
    get { return _WorldID; }
    set
    {
      if (_WorldID!= value)
      {
        _WorldID = value;
        OnPropertyChanged(nameof(WorldID));
      }
    }
  }

  private string? _WorldName;
  public string? WorldName
  {
    get { return _WorldName; }
    set
    {
      if (_WorldName != value)
      {
        _WorldName = value;
        OnPropertyChanged(nameof(WorldName));
      }
    }
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }
}