using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct ClothSkinningCoefficientSerializer 
{
    [ProtoMember(1)] public float MaxDistance;
    [ProtoMember(2)] public float MaxDistanceBias;
    [ProtoMember(3)] public float CollisionSphereRadius;
    [ProtoMember(4)] public float CollisionSphereDistance;

    public ClothSkinningCoefficientSerializer(ClothSkinningCoefficient data)
    {
        MaxDistance = data.maxDistance;
        MaxDistanceBias = data.maxDistanceBias;
        CollisionSphereRadius = data.collisionSphereRadius;
        CollisionSphereDistance = data.collisionSphereDistance;
    }

    public static explicit operator ClothSkinningCoefficient(ClothSkinningCoefficientSerializer data)
    {
        var clothSkinningCoefficient = new ClothSkinningCoefficient
        {
            maxDistanceBias = data.MaxDistanceBias,
            collisionSphereRadius = data.CollisionSphereRadius,
            collisionSphereDistance = data.CollisionSphereDistance
        };

        return clothSkinningCoefficient;
    }

    public static explicit operator ClothSkinningCoefficientSerializer(ClothSkinningCoefficient data)
    {
        return new ClothSkinningCoefficientSerializer(data);
    }
}
