using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class OcclusionAreaSerializer
{
    [ProtoMember(1)] public Vector3Serializer Center { get; set; }
    [ProtoMember(2)] public Vector3Serializer Size { get; set; }

    public OcclusionAreaSerializer(GameObject gameObject, OcclusionAreaSerializer component)
    {
        var occlusionArea = gameObject.GetComponent<OcclusionArea>();

        if (occlusionArea == null)
            occlusionArea = gameObject.AddComponent<OcclusionArea>();

        occlusionArea.center = (Vector3) component.Center;
        occlusionArea.size = (Vector3) component.Size;
    }

    public OcclusionAreaSerializer(GameObject gameObject)
    {
        var occlusionArea = gameObject.GetComponent<OcclusionArea>();

        Center = (Vector3Serializer) occlusionArea.center;
        Size = (Vector3Serializer) occlusionArea.size;
    }

    // Empty constructor required for ProtoBuf
    private OcclusionAreaSerializer()
    {
    }
}
