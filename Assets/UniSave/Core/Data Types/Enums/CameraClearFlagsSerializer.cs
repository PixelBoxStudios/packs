using ProtoBuf;

[ProtoContract]
public enum CameraClearFlagsSerializer
{
    Skybox,
    Color,
    SolidColor,
    Depth,
    Nothing
}