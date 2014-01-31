using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class LODGroupSerializer
{
    [ProtoMember(1)] public Vector3Serializer LocalReferencePoint { get; set; }
    [ProtoMember(2)] public float Size { get; set; }

    public LODGroupSerializer(GameObject gameObject, LODGroupSerializer component)
    {
        var lodGroup = gameObject.GetComponent<LODGroup>();

        if (lodGroup == null)
            lodGroup = gameObject.AddComponent<LODGroup>();

        lodGroup.localReferencePoint = (Vector3) component.LocalReferencePoint;
        lodGroup.size = component.Size;
    }

    public LODGroupSerializer(GameObject gameObject)
    {
        var lodGroup = gameObject.GetComponent<LODGroup>();

        LocalReferencePoint = (Vector3Serializer) lodGroup.localReferencePoint;
        Size = lodGroup.size;
    }

    // Empty constructor required for ProtoBuf
    private LODGroupSerializer()
    {
    }
}
