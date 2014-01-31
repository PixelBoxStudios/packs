using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public static partial class UniSave
{
	public static Dictionary<string, string> SupportedComponents = new Dictionary<string, string>
	{
		{ "Transform", "TransformSerializer" },
        { "Terrain", "TerrainSerializer" },
        { "SkinnedMeshRenderer", "SkinnedMeshRendererSerializer" },

                    // Mesh
		{ "MeshFilter" , "MeshFilterSerializer" },
        { "TextMesh", "TextMeshSerializer"},
        { "MeshRenderer", "MeshRendererSerializer" },

                    // Effects
        { "ParticleSystem", "ParticleSystemSerializer" },
        { "TrailRenderer", "TrailRendererSerializer" },
        { "LineRenderer", "LineRendererSerializer" },
        { "LensFlare", "LensFlareSerializer" },
        { "Projector", "ProjectorSerializer" },

                            //Legacy
                            { "ParticleAnimator", "ParticleAnimatorSerializer" },
                            { "ParticleRenderer", "ParticleRendererSerializer" },

                    // Physics
        { "Rigidbody", "RigidbodySerializer" },
        { "CharacterController", "CharacterControllerSerializer" },
        { "BoxCollider", "BoxColliderSerializer" },
        { "SphereCollider", "SphereColliderSerializer" },
        { "CapsuleCollider", "CapsuleColliderSerializer" },
        { "MeshCollider", "MeshColliderSerializer" },
        { "WheelCollider", "WheelColliderSerializer" },
        { "TerrainCollider", "TerrainColliderSerializer" },
        { "InteractiveCloth", "InteractiveClothSerializer" },
        { "SkinnedCloth", "SkinnedClothSerializer" },
        { "ClothRenderer", "ClothRendererSerializer" },
        { "HingeJoint", "HingeJointSerializer" },
        { "FixedJoint", "FixedJointSerializer" },
        { "SpringJoint", "SpringJointSerializer" },
        { "CharacterJoint", "CharacterJointSerializer" },
        { "ConfigurableJoint", "ConfigurableJointSerializer" },
        { "ConstantForce", "ConstantForceSerializer" },

                    // Navigation
        { "NavMeshAgent", "NavMeshAgentSerializer" },
        { "OffMeshLink", "OffMeshLinkSerializer" },

                    // Audio
        { "AudioListener", "AudioListenerSerializer" },
        { "AudioSource", "AudioSourceSerializer" },
        { "AudioReverbZone", "AudioReverbZoneSerializer" },
        { "AudioLowPassFilter", "AudioLowPassFilterSerializer" },
        { "AudioHighPassFilter", "AudioHighPassFilterSerializer" },
        { "AudioEchoFilter", "AudioEchoFilterSerializer" },
        { "AudioDistortionFilter", "AudioDistortionFilterSerializer" },
        { "AudioReverbFilter", "AudioReverbFilterSerializer" },
        { "AudioChorusFilter", "AudioChorusFilterSerializer" },

                    // Rendering
        { "Camera", "CameraSerializer" },
        { "Skybox", "SkyboxSerializer" },
        { "FlareLayer", "FlareLayerSerializer"},
        { "GUILayer", "GUILayerSerializer"},
        { "Light", "LightSerializer" },
        { "LightProbeGroup", "LightProbeGroupSerializer" },
        { "OcclusionArea", "OcclusionAreaSerializer" },
        { "OcclusionPortal", "OcclusionPortalSerializer" },
        { "LODGroup", "LODGroupSerializer" },
        { "GUITexture", "GUITextureSerializer" },
        { "GUIText", "GUITextSerializer" },

                    // Miscellaneous
        { "Animation", "AnimationSerializer" },

                    // Custom
        { "State", "StateSerializer" },
        { "GlobalSkybox", "GlobalSkyboxSerializer"}
	};

    private static bool _isSupported;
	
	public static bool IsComponentSupported(Component component)
	{
		foreach (var type in SupportedComponents)
		{
		    if (component.GetType().Name == type.Key)
			{
				_isSupported = true;
				break;
			}

		    /*else
		    {
                _isSupported = false;
		    }*/

		    _isSupported = false;
		}

	    return _isSupported;
	}
	
	public static string GetOriginalComponentName(string type)
	{
	    var key = from component in SupportedComponents
	              where component.Value.Equals(type)
	              select component.Key;
		
		return key.FirstOrDefault();
	}
}
