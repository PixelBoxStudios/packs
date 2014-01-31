using ProtoBuf;

[ProtoContract]
public sealed class SaveFileInfo
{
    [ProtoMember(1)] public string Name { get; private set; }
    [ProtoMember(2)] public int LevelNumber { get; private set; } 
    [ProtoMember(3)] public string LevelName { get; private set; }
    [ProtoMember(4)] public string Date { get; private set; }

    public SaveFileInfo(string name, int levelNumber, string levelName, string date)
    {
        Name = name;
        LevelNumber = levelNumber;
        LevelName = levelName;
        Date = date;
    }

    public SaveFileInfo(SaveFileInfo saveFileInfo)
    {
        Name = saveFileInfo.Name;
        LevelNumber = saveFileInfo.LevelNumber;
        LevelName = saveFileInfo.LevelName;
        Date = saveFileInfo.Date;
    }

    // Empty constructor required for ProtoBuf.
    public SaveFileInfo()
    {
    }
}
