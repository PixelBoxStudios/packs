using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class LightSerializer
{
	[ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public LightTypeSerializer Type { get; set; }
	[ProtoMember(3)]  public ColorSerializer Color { get; set; }
	[ProtoMember(4)]  public float Intensity { get; set; }
	[ProtoMember(5)]  public LightShadowsSerializer Shadows { get; set; }
	[ProtoMember(6)]  public float ShadowStrength { get; set; }
	[ProtoMember(7)]  public float ShadowBias { get; set; }
	[ProtoMember(8)]  public float ShadowSoftness { get; set; }
	[ProtoMember(9)]  public float ShadowSoftnessFade { get; set; }
	[ProtoMember(10)] public float Range { get; set; }
	[ProtoMember(11)] public float SpotAngle { get; set; }
    [ProtoMember(12)] public string CookieName { get; set; }
    [ProtoMember(13)] public string FlareName { get; set; }
	[ProtoMember(14)] public LightRenderModeSerializer RenderMode { get; set; }
	[ProtoMember(15)] public int CullingMask { get; set; }
    [ProtoMember(16)] public Vector2Serializer AreaSize { get; set; }
	
	public LightSerializer(GameObject gameObject, LightSerializer component)
	{
	    var light = gameObject.GetComponent<Light>();

        if (gameObject.light == null)
            light = gameObject.AddComponent<Light>();

        light.enabled = component.Enabled;
		light.type = (LightType)component.Type;
		light.color = (Color)component.Color;
		light.intensity = component.Intensity;
		light.shadows = (LightShadows)component.Shadows;
		light.shadowStrength = component.ShadowStrength;
		light.shadowBias = component.ShadowBias;
		light.shadowSoftness = component.ShadowSoftness;
		light.shadowSoftnessFade = component.ShadowSoftnessFade;
		light.range = component.Range;
		light.spotAngle = component.SpotAngle;

        if (!String.IsNullOrEmpty(component.CookieName))
		{
            gameObject.light.cookie = (Texture) Resources.Load(component.CookieName);
		}

        if (!String.IsNullOrEmpty(component.FlareName))
            light.flare = (Flare) UniSave.TryLoadResource(component.FlareName);

	    light.renderMode = (LightRenderMode) component.RenderMode;
		light.cullingMask = component.CullingMask;
	    //light.areaSize = (Vector2) component.AreaSize;
	}
	
	public LightSerializer(GameObject gameObject)
	{
		Enabled = gameObject.light.enabled;
		Type = (LightTypeSerializer)gameObject.light.type;
		Color = (ColorSerializer)gameObject.light.color;
		Intensity = gameObject.light.intensity;
		Shadows = (LightShadowsSerializer)gameObject.light.shadows;
		ShadowStrength = gameObject.light.shadowStrength;
		ShadowBias = gameObject.light.shadowBias;
		ShadowSoftness = gameObject.light.shadowSoftness;
		ShadowSoftnessFade = gameObject.light.shadowSoftnessFade;
		Range = gameObject.light.range;
		SpotAngle = gameObject.light.spotAngle;
		
		if (gameObject.light.cookie != null)
		    CookieName = gameObject.light.cookie.name;

        if (gameObject.light.flare != null)
            FlareName = gameObject.light.flare.name;

	    CullingMask = gameObject.light.cullingMask;
	    //AreaSize = (Vector2Serializer) gameObject.light.areaSize;
	}

    // Empty constructor required for Protobuf
    private LightSerializer()
    {
    }
}