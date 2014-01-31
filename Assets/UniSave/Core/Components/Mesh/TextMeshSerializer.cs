using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class TextMeshSerializer
{
	[ProtoMember(1)]  public string Text { get; set; }
	[ProtoMember(2)]  public int FontSize { get; set; }
	[ProtoMember(3)]  public FontStyleSerializer FontStyle { get; set; }
	[ProtoMember(4)]  public float OffsetZ { get; set; }
	[ProtoMember(5)]  public TextAlignmentSerializer Alignment { get; set; }
	[ProtoMember(6)]  public TextAnchorSerializer Anchor { get; set; }
	[ProtoMember(7)]  public float CharacterSize { get; set; }
	[ProtoMember(8)]  public float LineSpacing { get; set; }
	[ProtoMember(9)]  public float TabSize { get; set; }
    [ProtoMember(10)] public string FontName { get; set; }

    public TextMeshSerializer(GameObject gameObject, TextMeshSerializer component)
    {
        var textMesh = gameObject.GetComponent<TextMesh>();

        if (textMesh == null)
            textMesh = gameObject.AddComponent<TextMesh>();

        textMesh.text = component.Text;

        if (!String.IsNullOrEmpty(component.FontName))
            textMesh.font = (Font) UniSave.TryLoadResource(component.FontName);

        textMesh.fontSize = component.FontSize;
        textMesh.fontStyle = (FontStyle) component.FontStyle;
        textMesh.offsetZ = component.OffsetZ;
        textMesh.alignment = (TextAlignment) component.Alignment;
        textMesh.anchor = (TextAnchor) component.Anchor;
        textMesh.characterSize = component.CharacterSize;
        textMesh.lineSpacing = component.LineSpacing;
        textMesh.tabSize = component.TabSize;
    }

    public TextMeshSerializer(GameObject gameObject)
    {
        var textMesh = gameObject.GetComponent<TextMesh>();

        Text = textMesh.text;

        if (textMesh.font != null)
            FontName = textMesh.font.name;

        FontSize = textMesh.fontSize;
        FontStyle = (FontStyleSerializer) textMesh.fontStyle;
        OffsetZ = textMesh.offsetZ;
        Alignment = (TextAlignmentSerializer) textMesh.alignment;
        Anchor = (TextAnchorSerializer) textMesh.anchor;
        CharacterSize = textMesh.characterSize;
        LineSpacing = textMesh.lineSpacing;
        TabSize = textMesh.tabSize;
    }

    // Empty constructor required for Protobuf
    private TextMeshSerializer()
    {
    }
}
