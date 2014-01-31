using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class WheelColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
	[ProtoMember(4)] public Vector3Serializer Center { get; set; }
	[ProtoMember(5)] public float Radius { get; set; }
	[ProtoMember(6)] public float SuspensionDistance { get; set; }
	[ProtoMember(7)] public JointSpringSerializer SuspensionSpring { get; set; }
	[ProtoMember(8)] public float Mass { get; set; }
	[ProtoMember(9)] public WheelFrictionCurveSerializer ForwardFriction { get; set; }
	[ProtoMember(10)] public WheelFrictionCurveSerializer SidewaysFriction { get; set; }
	[ProtoMember(11)] public float MotorTorque { get; set; }
	[ProtoMember(12)] public float BrakeTorque { get; set; }
	[ProtoMember(13)] public float SteerAngle { get; set; }

    public WheelColliderSerializer(GameObject gameObject, WheelColliderSerializer component)
    {
        var wheelCollider = gameObject.GetComponent<WheelCollider>();

        if (wheelCollider == null)
            wheelCollider = gameObject.AddComponent<WheelCollider>();

        wheelCollider.enabled = component.Enabled;
        wheelCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            wheelCollider.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        wheelCollider.center = (Vector3) component.Center;
        wheelCollider.radius = component.Radius;
        wheelCollider.suspensionDistance = component.SuspensionDistance;
        wheelCollider.suspensionSpring = (JointSpring) component.SuspensionSpring;
        wheelCollider.mass = component.Mass;
        wheelCollider.forwardFriction = (WheelFrictionCurve) component.ForwardFriction;
        wheelCollider.sidewaysFriction = (WheelFrictionCurve) component.SidewaysFriction;
        wheelCollider.motorTorque = component.MotorTorque;
        wheelCollider.brakeTorque = component.BrakeTorque;
        wheelCollider.steerAngle = component.SteerAngle;
    }

    public WheelColliderSerializer(GameObject gameObject)
    {
        var wheelCollider = gameObject.GetComponent<WheelCollider>();

        Enabled = wheelCollider.enabled;
        IsTrigger = wheelCollider.isTrigger;

        if (wheelCollider.material != null)
            MaterialName = wheelCollider.material.name;

        Center = (Vector3Serializer) wheelCollider.center;
        Radius = wheelCollider.radius;
        SuspensionDistance = wheelCollider.suspensionDistance;
        SuspensionSpring = (JointSpringSerializer) wheelCollider.suspensionSpring;
        Mass = wheelCollider.mass;
        ForwardFriction = (WheelFrictionCurveSerializer) wheelCollider.forwardFriction;
        SidewaysFriction = (WheelFrictionCurveSerializer) wheelCollider.sidewaysFriction;
        MotorTorque = wheelCollider.motorTorque;
        BrakeTorque = wheelCollider.brakeTorque;
        SteerAngle = wheelCollider.steerAngle;
    }

    // Empty constructor required for ProtoBuf
    private WheelColliderSerializer()
    {
    }
}
