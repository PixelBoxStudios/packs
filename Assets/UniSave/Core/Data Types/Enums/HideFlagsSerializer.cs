using ProtoBuf;

[ProtoContract]
public enum HideFlagsSerializer
{
    HideInHierarchy,
    HideInInspector,
    DontSave,
    NotEditable,
    HideAndDontSave
}