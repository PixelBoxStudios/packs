using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class GUITextureSerializer
{
    [ProtoMember(1)] public ColorSerializer Color { get; set; }
    [ProtoMember(2)] public string TextureName { get; set; }
    [ProtoMember(3)] public RectSerializer PixelInset { get; set; }

    public GUITextureSerializer(GameObject gameObject, GUITextureSerializer component)
    {
        var guiTexture = gameObject.GetComponent<GUITexture>();

        if (guiTexture == null)
            guiTexture = gameObject.AddComponent<GUITexture>();

        guiTexture.color = (Color) component.Color;

        if (!String.IsNullOrEmpty(component.TextureName))
            guiTexture.texture = (Texture) UniSave.TryLoadResource(component.TextureName);

        guiTexture.pixelInset = (Rect) component.PixelInset;
    }

    public GUITextureSerializer(GameObject gameObject)
    {
        var guiTexture = gameObject.GetComponent<GUITexture>();

        Color = (ColorSerializer) guiTexture.color;

        if (guiTexture.texture != null)
            TextureName = guiTexture.texture.name;

        PixelInset = (RectSerializer) guiTexture.pixelInset;
    }

    // Empty constructor required for ProtoBuf
    private GUITextureSerializer()
    {
    }
}

