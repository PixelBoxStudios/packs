using ProtoBuf;

[ProtoContract]
public enum AnimationCullingTypeSerializer
{
    AlwaysAnimate,
    BasedOnRenderers,
    BasedOnClipBounds,
    BasedOnUserBounds
}
