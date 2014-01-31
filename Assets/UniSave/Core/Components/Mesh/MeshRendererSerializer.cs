using UnityEngine;
using System;
using ProtoBuf;
using System.Linq;

[ProtoContract]
public sealed class MeshRendererSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public bool CastShadows { get; set; }
    [ProtoMember(3)]  public bool ReceiveShadows { get; set; }
    [ProtoMember(7)]  public string[] MaterialNames { get; set; }
    [ProtoMember(8)]  public int LightmapIndex { get; set; }
    [ProtoMember(9)]  public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(10)] public bool UseLightProbes { get; set; }
    [ProtoMember(11)] public string LightProbeAnchorName;

    public MeshRendererSerializer(GameObject gameObject, MeshRendererSerializer component)
    {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();

        if (meshRenderer == null)
            meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.enabled = component.Enabled;
        meshRenderer.castShadows = component.CastShadows;
        meshRenderer.receiveShadows = component.ReceiveShadows;

        meshRenderer.materials = (from materialName in component.MaterialNames
                                  select (Material) UniSave.TryLoadResource(materialName)).ToArray();

        meshRenderer.lightmapIndex = component.LightmapIndex;
        meshRenderer.lightmapTilingOffset = (Vector4) component.LightmapTilingOffset;

        meshRenderer.useLightProbes = component.UseLightProbes;

        if (!String.IsNullOrEmpty(component.LightProbeAnchorName))
            meshRenderer.lightProbeAnchor.name = component.LightProbeAnchorName;
    }

    public MeshRendererSerializer(GameObject gameObject)
    {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();

        Enabled = meshRenderer.enabled;
        CastShadows = meshRenderer.castShadows;
        ReceiveShadows = meshRenderer.receiveShadows;

        MaterialNames = (from material in meshRenderer.materials
                         select material.name).ToArray();

        LightmapIndex = meshRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer) meshRenderer.lightmapTilingOffset;

        UseLightProbes = meshRenderer.useLightProbes;
        
        if (meshRenderer.lightProbeAnchor != null)
            LightProbeAnchorName = meshRenderer.lightProbeAnchor.name;

    }

    // Empty constructor required for ProtoBuf
    private MeshRendererSerializer()
    {
    }
}
