using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class SkinnedClothSerializer
{
    [ProtoMember(1)]  public bool Enabled { get; set; }
    [ProtoMember(2)]  public float BendingStiffness { get; set; }
    [ProtoMember(3)]  public float StretchingStiffness { get; set; }
    [ProtoMember(4)]  public float Damping { get; set; }
    [ProtoMember(5)]  public float Thickness { get; set; }
    [ProtoMember(6)]  public Vector3Serializer ExternalAcceleration { get; set; }
    [ProtoMember(7)]  public Vector3Serializer RandomAcceleration { get; set; }
    [ProtoMember(8)]  public bool UseGravity { get; set; }
    [ProtoMember(9)]  public bool SelfCollision { get; set; }
    [ProtoMember(10)] public ClothSkinningCoefficientSerializer[] Coefficients { get; set; }
    [ProtoMember(11)] public float WorldVelocityScale { get; set; }
    [ProtoMember(12)] public float WorldAccelerationScale { get; set; }

    public SkinnedClothSerializer(GameObject gameObject, SkinnedClothSerializer component)
    {
        var skinnedCloth = gameObject.GetComponent<SkinnedCloth>();

        if (skinnedCloth == null)
            skinnedCloth = gameObject.AddComponent<SkinnedCloth>();

        skinnedCloth.bendingStiffness = component.BendingStiffness;
        skinnedCloth.stretchingStiffness = component.StretchingStiffness;
        skinnedCloth.damping = component.Damping;
        skinnedCloth.thickness = component.Thickness;
        skinnedCloth.externalAcceleration = (Vector3) component.ExternalAcceleration;
        skinnedCloth.randomAcceleration = (Vector3) component.RandomAcceleration;
        skinnedCloth.useGravity = component.UseGravity;
        skinnedCloth.selfCollision = component.SelfCollision;
        skinnedCloth.enabled = component.Enabled;

        if (component.Coefficients.Length > 0)
            skinnedCloth.coefficients = Array.ConvertAll(component.Coefficients, element => (ClothSkinningCoefficient) element);

        skinnedCloth.worldVelocityScale = component.WorldVelocityScale;
        skinnedCloth.worldAccelerationScale = component.WorldAccelerationScale;
    }

    public SkinnedClothSerializer(GameObject gameObject)
    {
        var skinnedCloth = gameObject.GetComponent<SkinnedCloth>();

        BendingStiffness = skinnedCloth.bendingStiffness;
        StretchingStiffness = skinnedCloth.stretchingStiffness;
        Damping = skinnedCloth.damping;
        Thickness = skinnedCloth.thickness;
        ExternalAcceleration = (Vector3Serializer) skinnedCloth.externalAcceleration;
        RandomAcceleration = (Vector3Serializer) skinnedCloth.randomAcceleration;
        UseGravity = skinnedCloth.useGravity;
        SelfCollision = skinnedCloth.selfCollision;
        Enabled = skinnedCloth.enabled;
        Coefficients = Array.ConvertAll(skinnedCloth.coefficients, element => (ClothSkinningCoefficientSerializer) element);
        WorldVelocityScale = skinnedCloth.worldVelocityScale;
        WorldAccelerationScale = skinnedCloth.worldAccelerationScale;
    }

    // Empty constructor required for Protobuf
    private SkinnedClothSerializer()
    {
    }
}
