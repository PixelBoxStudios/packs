using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class SaveData
{
    [ProtoMember(1)] public List<string> UniqueGameObjectNames = new List<string>();
    [ProtoMember(2)] public List<GameObjectSerializer> GameObjects = new List<GameObjectSerializer>();
    [ProtoMember(3)] public List<string> DestroyedObjectNames = new List<string>();
    [ProtoMember(4)] public string LevelName { get; set; }

    /*
    public SaveData()
    {
        UniqueGameObjectNames = new List<string>();
        GameObjects = new List<GameObjectSerializer>();
        DestroyedObjectNames = new List<string>();
    }
     */
}
