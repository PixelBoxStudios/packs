using System;
using System.Reflection;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class FlareLayerSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }

    public FlareLayerSerializer(GameObject gameObject, FlareLayerSerializer component)
    {
        var flareLayer = gameObject.GetComponent("FlareLayer");

        if (flareLayer == null)
            flareLayer = gameObject.AddComponent("FlareLayer");

        PropertyInfo enabledProperty = flareLayer.GetType().GetProperty("Enabled");

        enabledProperty.SetValue(flareLayer, component.Enabled, null);
    }

    public FlareLayerSerializer(GameObject gameObject)
    {
        var flareLayer = gameObject.GetComponent("FlareLayer");

        PropertyInfo enabledProperty = flareLayer.GetType().GetProperty("Enabled");

        Enabled = (bool) enabledProperty.GetValue(flareLayer, null);
    }

    // Empty constructor required for ProtoBuf
    private FlareLayerSerializer()
    {
    }
}