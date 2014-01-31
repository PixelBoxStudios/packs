using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class GameObjectSerializer
{
    [ProtoMember(1)]  public string Name { get; set; }
    [ProtoMember(2)]  public HideFlagsSerializer HideFlags { get; set; }
    [ProtoMember(3)]  public bool IsStatic { get; set; }
    [ProtoMember(4)]  public int Layer { get; set; }
    [ProtoMember(5)]  public bool Active { get; set; }
    [ProtoMember(6)]  public string Tag { get; set; }
    [ProtoMember(7)]  public string UniqueName;
    [ProtoMember(8)]  public List<object> Components = new List<object>();
    [ProtoMember(9)]  public List<GameObjectSerializer> Children = new List<GameObjectSerializer>();
    [ProtoMember(10)] public Vector3Serializer LocalPosition { get; set; }
    [ProtoMember(11)] public Vector3Serializer LocalScale { get; set; }
    [ProtoMember(12)] public QuaternionSerializer LocalRotation { get; set; }
}