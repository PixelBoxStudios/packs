using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class LensFlareSerializer
{
    [ProtoMember(1)] public string FlareName { get; set; }
    [ProtoMember(2)] public float Brightness { get; set; }
    [ProtoMember(3)] public ColorSerializer Color { get; set; }
    [ProtoMember(4)] public bool Enabled { get; set; }

    public LensFlareSerializer(GameObject gameObject, LensFlareSerializer component)
    {
        var lensFlare = gameObject.GetComponent<LensFlare>();

        if (lensFlare == null)
            lensFlare = gameObject.AddComponent<LensFlare>();

        if (!String.IsNullOrEmpty(component.FlareName))
            lensFlare.flare = (Flare) UniSave.TryLoadResource(component.FlareName);

        lensFlare.brightness = component.Brightness;
        lensFlare.color = (Color) component.Color;
        lensFlare.enabled = component.Enabled;
    }

    public LensFlareSerializer(GameObject gameObject)
    {
        var lensFlare = gameObject.GetComponent<LensFlare>();

        if (lensFlare.flare != null)
            FlareName = lensFlare.flare.name;

        Brightness = lensFlare.brightness;
        Color = (ColorSerializer) lensFlare.color;
        Enabled = lensFlare.enabled;
    }

    // Empty constructor required for ProtoBuf
    private LensFlareSerializer()
    {
    }
}

