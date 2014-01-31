using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class GlobalSkyboxSerializer
{
    [ProtoMember(1)] public string MaterialName { get; set; }
    [ProtoMember(2)] public ColorSerializer AmbientLight { get; set; }

     public GlobalSkyboxSerializer(GameObject gameObject, GlobalSkyboxSerializer component)
    {
         var globalSkybox = gameObject.GetComponent<GlobalSkybox>();
         
         if (globalSkybox == null)
             globalSkybox = gameObject.AddComponent<GlobalSkybox>();
         
         RenderSettings.skybox = (Material) UniSave.TryLoadResource(component.MaterialName);
         RenderSettings.ambientLight = (Color) component.AmbientLight;
    }

    public GlobalSkyboxSerializer(GameObject gameObject)
    {
        var globalSkybox = gameObject.GetComponent<GlobalSkybox>();

        if (globalSkybox.Skybox != null)
            MaterialName = globalSkybox.Skybox.name;

        AmbientLight = (ColorSerializer) globalSkybox.AmbientLight;
    }

    // Empty constructor required for ProtoBuf
    private GlobalSkyboxSerializer()
    {
    }
}
