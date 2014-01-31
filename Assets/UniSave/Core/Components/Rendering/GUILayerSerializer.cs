using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class GUILayerSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }

    public GUILayerSerializer(GameObject gameObject, GUILayerSerializer component)
    {
        var guiLayer = gameObject.GetComponent<GUILayer>();

        if (guiLayer == null)
            guiLayer = gameObject.AddComponent<GUILayer>();

        guiLayer.enabled = component.Enabled;
    }

    public GUILayerSerializer(GameObject gameObject)
    {
        var guiLayer = gameObject.GetComponent<GUILayer>();

        Enabled = guiLayer.enabled;
    }

    // Empty constructor required for ProtoBuf
    private GUILayerSerializer()
    {
    }
}
