using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class LightProbeGroupSerializer
{
    [ProtoMember(1)] public Vector3Serializer[] ProbePositions { get; set; }

    public LightProbeGroupSerializer(GameObject gameObject, LightProbeGroupSerializer component)
    {
        var lightProbeGroup = gameObject.GetComponent<LightProbeGroup>();

        if (lightProbeGroup == null)
            lightProbeGroup = gameObject.AddComponent<LightProbeGroup>();

        if (component.ProbePositions != null)
            lightProbeGroup.probePositions = Array.ConvertAll(component.ProbePositions, element => (Vector3) element);
    }

    public LightProbeGroupSerializer(GameObject gameObject)
    {
        var lightProbeGroup = gameObject.GetComponent<LightProbeGroup>();

        if (lightProbeGroup.probePositions != null)
            ProbePositions = Array.ConvertAll(lightProbeGroup.probePositions, element => (Vector3Serializer) element);
    }

    // Empty constructor required for ProtoBuf
    private LightProbeGroupSerializer()
    {
    }
}
