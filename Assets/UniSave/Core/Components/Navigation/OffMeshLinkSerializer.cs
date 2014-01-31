using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class OffMeshLinkSerializer
{
    [ProtoMember(1)] public bool Activated { get; set; }
    [ProtoMember(2)] public float CostOverride { get; set; }

    public OffMeshLinkSerializer(GameObject gameObject, OffMeshLinkSerializer component)
    {
        var offMeshLink = gameObject.GetComponent<OffMeshLink>();

        if (offMeshLink == null)
            offMeshLink = gameObject.AddComponent<OffMeshLink>();

        offMeshLink.activated = component.Activated;
        offMeshLink.costOverride = component.CostOverride;
    }

    public OffMeshLinkSerializer(GameObject gameObject)
    {
        var offMeshLink = gameObject.GetComponent<OffMeshLink>();

        Activated = offMeshLink.activated;
        CostOverride = offMeshLink.costOverride;
    }

    // Empty constructor required for ProtoBuf;
    private OffMeshLinkSerializer()
    {
    }
}
