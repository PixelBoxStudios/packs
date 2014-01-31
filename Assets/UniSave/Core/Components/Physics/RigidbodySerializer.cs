using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class RigidbodySerializer
{
    [ProtoMember(1)] public Vector3Serializer Velocity { get; set; }
    [ProtoMember(2)] public Vector3Serializer AngularVelocity { get; set; }
    [ProtoMember(3)] public float Drag { get; set; }
    [ProtoMember(4)] public float AngularDrag { get; set; }
    [ProtoMember(5)] public float Mass { get; set; }
    [ProtoMember(6)] public bool UseGravity { get; set; }
    [ProtoMember(7)] public bool IsKinematic { get; set; }
    [ProtoMember(8)] public bool FreezeRotation { get; set; }
    [ProtoMember(9)] public RigidbodyConstraintsSerializer Constraints { get; set; }
    [ProtoMember(10)] public CollisionDetectionModeSerializer CollisionDetectionMode { get; set; }
    [ProtoMember(11)] public Vector3Serializer CenterOfMass { get; set; }
    [ProtoMember(12)] public QuaternionSerializer InertiaTensorRotation { get; set; }
    [ProtoMember(13)] public Vector3Serializer InertiaTensor { get; set; }
    [ProtoMember(14)] public bool DetectCollisions { get; set; }
    [ProtoMember(15)] public bool UseConeFriction { get; set; }
    [ProtoMember(16)] public Vector3Serializer Position { get; set; }
    [ProtoMember(17)] public QuaternionSerializer Rotation { get; set; }
    [ProtoMember(18)] public RigidbodyInterpolationSerializer Interpolation { get; set; }
    [ProtoMember(19)] public int SolverIterationCount { get; set; }
    [ProtoMember(20)] public float SleepVelocity { get; set; }
    [ProtoMember(21)] public float SleepAngularVelocity { get; set; }
    [ProtoMember(22)] public float MaxAngularVelocity { get; set; }

    public RigidbodySerializer(GameObject gameObject, RigidbodySerializer component)
    {
        var rigidbody = gameObject.GetComponent<Rigidbody>();

        if (rigidbody == null)
            rigidbody = gameObject.AddComponent<Rigidbody>();

        rigidbody.velocity = (Vector3) component.Velocity;
        rigidbody.angularVelocity = (Vector3) component.AngularVelocity;
        rigidbody.drag = component.Drag;
        rigidbody.angularDrag = component.AngularDrag;
        rigidbody.mass = component.Mass;
        rigidbody.useGravity = component.UseGravity;
        rigidbody.isKinematic = component.IsKinematic;
        rigidbody.freezeRotation = component.FreezeRotation;
        rigidbody.constraints = (RigidbodyConstraints) component.Constraints;
        rigidbody.collisionDetectionMode = (CollisionDetectionMode) component.CollisionDetectionMode;
        rigidbody.centerOfMass = (Vector3) component.CenterOfMass;
        rigidbody.inertiaTensorRotation = (Quaternion) component.InertiaTensorRotation;
        rigidbody.inertiaTensor = (Vector3) component.InertiaTensor;
        rigidbody.detectCollisions = component.DetectCollisions;
        rigidbody.useConeFriction = component.UseConeFriction;
        rigidbody.position = (Vector3) component.Position;
        rigidbody.rotation = (Quaternion) component.Rotation;
        rigidbody.interpolation = (RigidbodyInterpolation) component.Interpolation;
        rigidbody.solverIterationCount = component.SolverIterationCount;
        rigidbody.sleepVelocity = component.SleepVelocity;
        rigidbody.sleepAngularVelocity = component.SleepAngularVelocity;
        rigidbody.maxAngularVelocity = component.MaxAngularVelocity;
    }

    public RigidbodySerializer(GameObject gameObject)
    {
        var rigidbody = gameObject.GetComponent<Rigidbody>();

        Velocity = (Vector3Serializer) rigidbody.velocity;
        AngularVelocity = (Vector3Serializer) rigidbody.angularVelocity;
        Drag = rigidbody.drag;
        AngularDrag = rigidbody.angularDrag;
        Mass = rigidbody.mass;
        UseGravity = rigidbody.useGravity;
        IsKinematic = rigidbody.isKinematic;
        FreezeRotation = rigidbody.freezeRotation;
        Constraints = (RigidbodyConstraintsSerializer) rigidbody.constraints;
        CollisionDetectionMode = (CollisionDetectionModeSerializer) rigidbody.collisionDetectionMode;
        CenterOfMass = (Vector3Serializer) rigidbody.centerOfMass;
        InertiaTensorRotation = (QuaternionSerializer) rigidbody.inertiaTensorRotation;
        InertiaTensor = (Vector3Serializer) rigidbody.inertiaTensor;
        DetectCollisions = rigidbody.detectCollisions;
        UseConeFriction = rigidbody.useConeFriction;
        Position = (Vector3Serializer) rigidbody.position;
        Rotation = (QuaternionSerializer) rigidbody.rotation;
        Interpolation = (RigidbodyInterpolationSerializer) rigidbody.interpolation;
        SolverIterationCount = rigidbody.solverIterationCount;
        SleepVelocity = rigidbody.sleepVelocity;
        SleepAngularVelocity = rigidbody.sleepAngularVelocity;
        MaxAngularVelocity = rigidbody.maxAngularVelocity;
    }

    // Empty constructor required for ProtoBuf
    private RigidbodySerializer()
    {
    }
}
