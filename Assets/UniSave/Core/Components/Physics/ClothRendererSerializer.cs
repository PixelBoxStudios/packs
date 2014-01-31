using UnityEngine;
using ProtoBuf;
using System.Linq;

[ProtoContract]
public sealed class ClothRendererSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public bool CastShadows { get; set; }
    [ProtoMember(3)]  public bool ReceiveShadows { get; set; }
    [ProtoMember(4)]  public string[] MaterialNames { get; set; }
    [ProtoMember(5)]  public int LightmapIndex { get; set; }
    [ProtoMember(6)]  public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(7)] public bool UseLightProbes { get; set; }
    [ProtoMember(8)] public string LightProbeAnchor;
    [ProtoMember(9)] public bool PauseWhenNotVisible { get; set; }

    public ClothRendererSerializer(GameObject gameObject, ClothRendererSerializer component)
    {
        var clothRenderer = gameObject.GetComponent<ClothRenderer>();

        if (clothRenderer == null)
            clothRenderer = gameObject.AddComponent<ClothRenderer>();

        clothRenderer.enabled = component.Enabled;
        clothRenderer.castShadows = component.CastShadows;
        clothRenderer.receiveShadows = component.ReceiveShadows;

        clothRenderer.materials = (from materialName in component.MaterialNames 
                                   select (Material) UniSave.TryLoadResource(materialName)).ToArray();

        clothRenderer.lightmapIndex = component.LightmapIndex;
        clothRenderer.lightmapTilingOffset = (Vector4)component.LightmapTilingOffset;

        clothRenderer.useLightProbes = component.UseLightProbes;

        if (component.LightProbeAnchor != null)
            clothRenderer.lightProbeAnchor.name = component.LightProbeAnchor;

        clothRenderer.pauseWhenNotVisible = component.PauseWhenNotVisible;
    }

    public ClothRendererSerializer(GameObject gameObject)
    {
        var clothRenderer = gameObject.GetComponent<ClothRenderer>();

        Enabled = clothRenderer.enabled;
        CastShadows = clothRenderer.castShadows;
        ReceiveShadows = clothRenderer.receiveShadows;

        MaterialNames = (from material in clothRenderer.materials 
                         select material.name).ToArray();

        LightmapIndex = clothRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer) clothRenderer.lightmapTilingOffset;

        UseLightProbes = clothRenderer.useLightProbes;
        
        if (clothRenderer.lightProbeAnchor != null)
            LightProbeAnchor = clothRenderer.lightProbeAnchor.name;

        PauseWhenNotVisible = clothRenderer.pauseWhenNotVisible;
    }

    // Empty constructor required for Protobuf
    private ClothRendererSerializer()
    {
    }
}
