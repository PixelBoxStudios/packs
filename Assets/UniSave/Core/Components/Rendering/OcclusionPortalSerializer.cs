using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class OcclusionPortalSerializer
{
    [ProtoMember(1)] public bool Open { get; set; }

    public OcclusionPortalSerializer(GameObject gameObject, OcclusionPortalSerializer component)
    {
        var occlusionPortal = gameObject.GetComponent<OcclusionPortal>();

        if (occlusionPortal== null)
            occlusionPortal = gameObject.AddComponent<OcclusionPortal>();

        occlusionPortal.open = component.Open;
    }

    public OcclusionPortalSerializer(GameObject gameObject)
    {
        var occlusionPortal = gameObject.GetComponent<OcclusionPortal>();

        Open = occlusionPortal.open;
    }

    // Empty constructor required for ProtoBuf
    private OcclusionPortalSerializer()
    {
    }
}
