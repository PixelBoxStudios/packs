using UnityEngine;

[ExecuteInEditMode]
public class GlobalSkybox : MonoBehaviour
{
    [HideInInspector]
    public Material Skybox;
    [HideInInspector]
    public Color AmbientLight;

    private void Update()
    {
        Skybox = RenderSettings.skybox;
        AmbientLight = RenderSettings.ambientLight;
    }
}