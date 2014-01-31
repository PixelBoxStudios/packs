using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public class ParticleAnimatorSerializer
{
	[ProtoMember(1)] public bool DoesAnimateColor { get; set; }
	[ProtoMember(2)] public Vector3Serializer WorldRotationAxis { get; set; }
	[ProtoMember(3)] public Vector3Serializer LocalRotationAxis { get; set; }
	[ProtoMember(4)] public float SizeGrow { get; set; }
	[ProtoMember(5)] public Vector3Serializer RndForce { get; set; }
	[ProtoMember(6)] public Vector3Serializer Force { get; set; }
	[ProtoMember(7)] public float Damping { get; set; }
	[ProtoMember(8)] public bool Autodestruct { get; set; }
	[ProtoMember(9)] public ColorSerializer[] ColorAnimation { get; set; }

    public ParticleAnimatorSerializer(GameObject gameObject, ParticleAnimatorSerializer component)
    {
        var particleAnimator = gameObject.GetComponent<ParticleAnimator>();

        if (particleAnimator == null)
            particleAnimator = gameObject.AddComponent<ParticleAnimator>();

        particleAnimator.doesAnimateColor = component.DoesAnimateColor;
        particleAnimator.worldRotationAxis = (Vector3) component.WorldRotationAxis;
        particleAnimator.localRotationAxis = (Vector3) component.LocalRotationAxis;
        particleAnimator.sizeGrow = component.SizeGrow;
        particleAnimator.rndForce = (Vector3) component.RndForce;
        particleAnimator.force = (Vector3) component.Force;
        particleAnimator.damping = component.Damping;
        particleAnimator.autodestruct = component.Autodestruct;
        particleAnimator.colorAnimation = Array.ConvertAll(component.ColorAnimation, element => (Color)element);
    }

    public ParticleAnimatorSerializer(GameObject gameObject)
    {
        var particleAnimator = gameObject.GetComponent<ParticleAnimator>();

        DoesAnimateColor = particleAnimator.doesAnimateColor;
        WorldRotationAxis = (Vector3Serializer) particleAnimator.worldRotationAxis;
        LocalRotationAxis = (Vector3Serializer) particleAnimator.localRotationAxis;
        SizeGrow = particleAnimator.sizeGrow;
        RndForce = (Vector3Serializer) particleAnimator.rndForce;
        Force = (Vector3Serializer) particleAnimator.force;
        Damping = particleAnimator.damping;
        Autodestruct = particleAnimator.autodestruct;
        ColorAnimation = Array.ConvertAll(particleAnimator.colorAnimation, element => (ColorSerializer) element);
    }

    // Empty constructor required for Protobuf
    private ParticleAnimatorSerializer()
    {
    }
}
