using ProtoBuf;

[ProtoContract]
public enum RenderingPathSerializer
{
    UsePlayerSettings = -1,
    VertexLit,
    Forward,
    DeferredLighting
}