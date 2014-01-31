using UnityEngine;
using ProtoBuf;
using System.Linq;

[ProtoContract]
public sealed class TrailRendererSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public bool CastShadows { get; set; }
    [ProtoMember(3)]  public bool ReceiveShadows { get; set; }
    [ProtoMember(4)]  public string[] MaterialNames { get; set; }
    [ProtoMember(5)]  public int LightmapIndex { get; set; }
    [ProtoMember(6)]  public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(7)]  public bool UseLightProbes { get; set; }
    [ProtoMember(8)]  public string LightProbeAnchor;
    [ProtoMember(9)]  public float Time { get; set; }
	[ProtoMember(10)] public float StartWidth { get; set; }
	[ProtoMember(11)] public float EndWidth { get; set; }
	[ProtoMember(12)] public bool Autodestruct { get; set; }

    public TrailRendererSerializer(GameObject gameObject, TrailRendererSerializer component)
    {
        var trailRenderer = gameObject.GetComponent<TrailRenderer>();

        if (trailRenderer == null)
            trailRenderer = gameObject.AddComponent<TrailRenderer>();

        trailRenderer.enabled = component.Enabled;
        trailRenderer.castShadows = component.CastShadows;
        trailRenderer.receiveShadows = component.ReceiveShadows;

        trailRenderer.materials = (from materialName in component.MaterialNames
                                   select (Material) UniSave.TryLoadResource(materialName)).ToArray();

        trailRenderer.lightmapIndex = component.LightmapIndex;
        trailRenderer.lightmapTilingOffset = (Vector4) component.LightmapTilingOffset;

        trailRenderer.useLightProbes = component.UseLightProbes;

        if (component.LightProbeAnchor != null)
            trailRenderer.lightProbeAnchor.name = component.LightProbeAnchor;

        trailRenderer.time = component.Time;
        trailRenderer.startWidth = component.StartWidth;
        trailRenderer.endWidth = component.EndWidth;
        trailRenderer.autodestruct = component.Autodestruct;
    }

    public TrailRendererSerializer(GameObject gameObject)
    {
        var trailRenderer = gameObject.GetComponent<TrailRenderer>();

        Enabled = trailRenderer.enabled;
        CastShadows = trailRenderer.castShadows;
        ReceiveShadows = trailRenderer.receiveShadows;

        MaterialNames = (from material in trailRenderer.materials
                         select material.name).ToArray();

        LightmapIndex = trailRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer)trailRenderer.lightmapTilingOffset;

        UseLightProbes = trailRenderer.useLightProbes;
        
        if (trailRenderer.lightProbeAnchor != null)
            LightProbeAnchor = trailRenderer.lightProbeAnchor.name;

        Time = trailRenderer.time;
        StartWidth = trailRenderer.startWidth;
        EndWidth = trailRenderer.endWidth;
        Autodestruct = trailRenderer.autodestruct;
    }

    // Empty constructor required for ProtoBuf
    private TrailRendererSerializer()
    {
    }
}
