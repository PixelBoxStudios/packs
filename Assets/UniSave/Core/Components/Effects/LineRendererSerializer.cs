using UnityEngine;
using ProtoBuf;
using System.Linq;

[ProtoContract]
public sealed class LineRendererSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public bool CastShadows { get; set; }
    [ProtoMember(3)]  public bool ReceiveShadows { get; set; }
    [ProtoMember(4)]  public string[] MaterialNames { get; set; }
    [ProtoMember(5)]  public int LightmapIndex { get; set; }
    [ProtoMember(6)]  public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(7)]  public bool UseLightProbes { get; set; }
    [ProtoMember(8)]  public string LightProbeAnchor;
    [ProtoMember(9)]  public bool UseWorldSpace { get; set; }

    public LineRendererSerializer(GameObject gameObject, LineRendererSerializer component)
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();

        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.enabled = component.Enabled;
        lineRenderer.castShadows = component.CastShadows;
        lineRenderer.receiveShadows = component.ReceiveShadows;

        lineRenderer.materials = (from materialName in component.MaterialNames
                                  select (Material) UniSave.TryLoadResource(materialName)).ToArray();

        lineRenderer.lightmapIndex = component.LightmapIndex;
        lineRenderer.lightmapTilingOffset = (Vector4) component.LightmapTilingOffset;

        lineRenderer.useLightProbes = component.UseLightProbes;

        if (component.LightProbeAnchor != null)
            lineRenderer.lightProbeAnchor.name = component.LightProbeAnchor; 

        lineRenderer.useWorldSpace = component.UseWorldSpace;
    }

    public LineRendererSerializer(GameObject gameObject)
    {
        var lineRenderer = gameObject.GetComponent<LineRenderer>();

        Enabled = lineRenderer.enabled;
        CastShadows = lineRenderer.castShadows;
        ReceiveShadows = lineRenderer.receiveShadows;

        MaterialNames = (from material in lineRenderer.materials
                         select material.name).ToArray();

        LightmapIndex = lineRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer) lineRenderer.lightmapTilingOffset;

        UseLightProbes = lineRenderer.useLightProbes;
        
        if (lineRenderer.lightProbeAnchor != null)
            LightProbeAnchor = lineRenderer.lightProbeAnchor.name;

        UseWorldSpace = lineRenderer.useWorldSpace;
    }

    // Empty constructor required for ProtoBuf
    private LineRendererSerializer()
    {
    }
}

