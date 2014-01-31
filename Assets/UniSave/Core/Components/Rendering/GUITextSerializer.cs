using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class GUITextSerializer
{
    [ProtoMember(1)] public string Text { get; set; }
    [ProtoMember(2)] public string MaterialName { get; set; }
    [ProtoMember(3)] public Vector2Serializer PixelOffset { get; set; }
    [ProtoMember(4)] public string FontName { get; set; }
    [ProtoMember(5)] public TextAlignmentSerializer Alignment { get; set; }
    [ProtoMember(6)] public TextAnchorSerializer Anchor { get; set; }
    [ProtoMember(7)] public float LineSpacing { get; set; }
    [ProtoMember(8)] public float TabSize { get; set; }
    [ProtoMember(9)] public int FontSize { get; set; }
    [ProtoMember(10)] public FontStyleSerializer FontStyle { get; set; }

    public GUITextSerializer(GameObject gameObject, GUITextSerializer component)
    {
        var guiText = gameObject.GetComponent<GUIText>();

        if (guiText == null)
            guiText = gameObject.AddComponent<GUIText>();

        guiText.text = component.Text;

        if (!String.IsNullOrEmpty(component.MaterialName))
            guiText.material = (Material) UniSave.TryLoadResource(component.MaterialName);

        guiText.pixelOffset = (Vector2)component.PixelOffset;

        if (!String.IsNullOrEmpty(component.FontName))
            guiText.font = (Font) UniSave.TryLoadResource(component.FontName);
         
        guiText.alignment = (TextAlignment) component.Alignment;
        guiText.anchor = (TextAnchor) component.Anchor;
        guiText.lineSpacing = component.LineSpacing;
        guiText.tabSize = component.TabSize;
        guiText.fontSize = component.FontSize;
        guiText.fontStyle = (FontStyle) component.FontStyle;
    }

    public GUITextSerializer(GameObject gameObject)
    {
        var guiText = gameObject.GetComponent<GUIText>();

        Text = guiText.text;

        if (guiText.material != null)
            MaterialName = guiText.material.name;

        PixelOffset = (Vector2Serializer) guiText.pixelOffset;

        if (guiText.font != null)
            FontName = guiText.font.name;

        Alignment = (TextAlignmentSerializer) guiText.alignment;
        Anchor = (TextAnchorSerializer) guiText.anchor;
        LineSpacing = guiText.lineSpacing;
        TabSize = guiText.tabSize;
        FontSize = guiText.fontSize;
        FontStyle = (FontStyleSerializer) guiText.fontStyle;
    }

    // Empty constructor required for ProtoBuf
    private GUITextSerializer()
    {
    }
}
