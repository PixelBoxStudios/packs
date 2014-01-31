using System;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class CharacterControllerSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
    [ProtoMember(4)] public float Radius { get; set; }
    [ProtoMember(5)] public float Height { get; set; }
    [ProtoMember(6)] public Vector3Serializer Center { get; set; }
    [ProtoMember(7)] public float SlopeLimit { get; set; }
    [ProtoMember(8)] public float StepOffset { get; set; }
    [ProtoMember(9)] public bool DetectCollisions { get; set; }

    public CharacterControllerSerializer(GameObject gameObject, CharacterControllerSerializer component)
    {
        var characterController = gameObject.GetComponent<CharacterController>();

        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();

        characterController.enabled = component.Enabled;
        characterController.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            characterController.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        characterController.radius = component.Radius;
        characterController.height = component.Height;
        characterController.center = (Vector3) component.Center;
        characterController.slopeLimit = component.SlopeLimit;
        characterController.stepOffset = component.StepOffset;
        characterController.detectCollisions = component.DetectCollisions;
    }

    public CharacterControllerSerializer(GameObject gameObject)
    {
        var characterController = gameObject.GetComponent<CharacterController>();

        Enabled = characterController.enabled;
        IsTrigger = characterController.isTrigger;

        if (characterController.material != null)
            MaterialName = characterController.material.name;

        Radius = characterController.radius;
        Height = characterController.height;
        Center = (Vector3Serializer) characterController.center;
        SlopeLimit = characterController.slopeLimit;
        StepOffset = characterController.stepOffset;
        DetectCollisions = characterController.detectCollisions;
    }

    // Empty constructor required for ProtoBuf
    private CharacterControllerSerializer()
    {
    }
}
