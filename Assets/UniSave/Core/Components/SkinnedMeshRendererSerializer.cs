using System.Linq;
using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class SkinnedMeshRendererSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public bool CastShadows { get; set; }
    [ProtoMember(3)]  public bool ReceiveShadows { get; set; }
    [ProtoMember(4)]  public string[] MaterialNames { get; set; }
    [ProtoMember(5)]  public int LightmapIndex { get; set; }
    [ProtoMember(6)]  public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(7)]  public bool UseLightProbes { get; set; }
    [ProtoMember(8)]  public string LightProbeAnchorName { get; set; }
    [ProtoMember(9)]  public TransformSerializer[] Bones { get; set; }
    [ProtoMember(10)] public SkinQualitySerializer Quality { get; set; }
    [ProtoMember(11)] public MeshSerializer SharedMesh { get; set; }
    [ProtoMember(12)] public bool UpdateWhenOffscreen { get; set; }
    [ProtoMember(13)] public BoundsSerializer LocalBounds { get; set; }

    public SkinnedMeshRendererSerializer(GameObject gameObject, SkinnedMeshRendererSerializer component)
    {
        var skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();

        if (skinnedMeshRenderer == null)
            skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();

        skinnedMeshRenderer.enabled = component.Enabled;
        skinnedMeshRenderer.castShadows = component.CastShadows;
        skinnedMeshRenderer.receiveShadows = component.ReceiveShadows;

        var savedMaterials = new Material[component.MaterialNames.Length];
        var i = 0;

        foreach (string materialName in component.MaterialNames)
        {
            savedMaterials[i] = (Material) UniSave.TryLoadResource(materialName);
            i++;
        }

        skinnedMeshRenderer.materials = savedMaterials;

        skinnedMeshRenderer.lightmapIndex = component.LightmapIndex;
        skinnedMeshRenderer.lightmapTilingOffset = (Vector4) component.LightmapTilingOffset;

        skinnedMeshRenderer.useLightProbes = component.UseLightProbes;

        if (!String.IsNullOrEmpty(component.LightProbeAnchorName))
            skinnedMeshRenderer.lightProbeAnchor.name = component.LightProbeAnchorName;

        skinnedMeshRenderer.quality = (SkinQuality) component.Quality;

        if (component.SharedMesh != null)
        {
            skinnedMeshRenderer.sharedMesh = (Mesh) component.SharedMesh;
            skinnedMeshRenderer.sharedMesh.name = component.SharedMesh.MeshName;
        }

        skinnedMeshRenderer.updateWhenOffscreen = component.UpdateWhenOffscreen;
        skinnedMeshRenderer.localBounds = (Bounds) component.LocalBounds;
    }

    public SkinnedMeshRendererSerializer(GameObject gameObject)
    {
        var skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();

        Enabled = skinnedMeshRenderer.enabled;
        CastShadows = skinnedMeshRenderer.castShadows;
        ReceiveShadows = skinnedMeshRenderer.receiveShadows;

        MaterialNames = new string[skinnedMeshRenderer.materials.Length];
        var i = 0;

        foreach (Material material in skinnedMeshRenderer.materials)
        {
            MaterialNames[i] = material.name;
            i++;
        }

        LightmapIndex = skinnedMeshRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer) skinnedMeshRenderer.lightmapTilingOffset;

        UseLightProbes = skinnedMeshRenderer.useLightProbes;

        if (skinnedMeshRenderer.lightProbeAnchor != null)
            LightProbeAnchorName = skinnedMeshRenderer.lightProbeAnchor.name;

        Quality = (SkinQualitySerializer) skinnedMeshRenderer.quality;

        if (skinnedMeshRenderer.sharedMesh != null)
        {
            SharedMesh = (MeshSerializer) skinnedMeshRenderer.sharedMesh;
            SharedMesh.MeshName = skinnedMeshRenderer.sharedMesh.name;
        }

        UpdateWhenOffscreen = skinnedMeshRenderer.updateWhenOffscreen;
        LocalBounds = (BoundsSerializer) skinnedMeshRenderer.localBounds;
    }

    // Empty constructor required for Protobuf
    private SkinnedMeshRendererSerializer()
    {
    }
}