using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class SkyboxSerializer 
{
    [ProtoMember(1)] public string MaterialName { get; set; }

    public SkyboxSerializer(GameObject gameObject, SkyboxSerializer component)
    {
        var skybox = gameObject.GetComponent<Skybox>();

        if (skybox == null)
            skybox = gameObject.AddComponent<Skybox>();

        if (!String.IsNullOrEmpty(component.MaterialName))
        {
            skybox.material = (Material) UniSave.TryLoadResource(component.MaterialName);
        }
    }

    public SkyboxSerializer(GameObject gameObject)
    {
        var skybox = gameObject.GetComponent<Skybox>();

        if (skybox.material != null)
            MaterialName = skybox.material.name;
    }

    // Empty constructor required for ProtoBuf
    private SkyboxSerializer()
    {
    }
}
