using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class NavMeshAgentSerializer
{
    [ProtoMember(1)] public Vector3Serializer Destination { get; set; }
    [ProtoMember(2)] public float StoppingDistance { get; set; }
    [ProtoMember(3)] public Vector3Serializer Velocity { get; set; }
    [ProtoMember(4)] public Vector3Serializer NextPosition { get; set; }
    [ProtoMember(5)] public float BaseOffset { get; set; }
    [ProtoMember(6)] public bool AutoTraverseOffMeshLink { get; set; }
    [ProtoMember(7)] public bool AutoRepath { get; set; }
    [ProtoMember(8)] public int WalkableMask { get; set; }
    [ProtoMember(9)] public float Speed { get; set; }
    [ProtoMember(10)] public float AngularSpeed { get; set; }
    [ProtoMember(11)] public float Acceleration { get; set; }
    [ProtoMember(12)] public bool UpdatePosition { get; set; }
    [ProtoMember(13)] public bool UpdateRotation { get; set; }
    [ProtoMember(14)] public float Radius { get; set; }
    [ProtoMember(15)] public float Height { get; set; }
    [ProtoMember(16)] public ObstacleAvoidanceTypeSerializer ObstacleAvoidanceType { get; set; }
    [ProtoMember(17)] public bool Enabled { get; set; }

    public NavMeshAgentSerializer(GameObject gameObject, NavMeshAgentSerializer component)
    {
        var navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();

        navMeshAgent.destination = (Vector3) component.Destination;
        navMeshAgent.stoppingDistance = component.StoppingDistance;
        navMeshAgent.velocity = (Vector3) component.Velocity;
        navMeshAgent.nextPosition = (Vector3) component.NextPosition;
        navMeshAgent.baseOffset = component.BaseOffset;
        navMeshAgent.autoTraverseOffMeshLink = component.AutoTraverseOffMeshLink;
        navMeshAgent.autoRepath = component.AutoRepath;
        navMeshAgent.walkableMask = component.WalkableMask;
        navMeshAgent.speed = component.Speed;
        navMeshAgent.angularSpeed = component.AngularSpeed;
        navMeshAgent.acceleration = component.Acceleration;
        navMeshAgent.updatePosition = component.UpdatePosition;
        navMeshAgent.updateRotation = component.UpdateRotation;
        navMeshAgent.radius = component.Radius;
        navMeshAgent.height = component.Height;
        navMeshAgent.obstacleAvoidanceType = (ObstacleAvoidanceType) component.ObstacleAvoidanceType;
        navMeshAgent.enabled = component.Enabled;
    }

    public NavMeshAgentSerializer(GameObject gameObject)
    {
        var navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

        Destination = (Vector3Serializer) navMeshAgent.destination;
        StoppingDistance = navMeshAgent.stoppingDistance;
        Velocity = (Vector3Serializer) navMeshAgent.velocity;
        NextPosition = (Vector3Serializer) navMeshAgent.nextPosition;
        BaseOffset = navMeshAgent.baseOffset;
        AutoTraverseOffMeshLink = navMeshAgent.autoTraverseOffMeshLink;
        AutoRepath = navMeshAgent.autoRepath;
        WalkableMask = navMeshAgent.walkableMask;
        Speed = navMeshAgent.speed;
        AngularSpeed = navMeshAgent.angularSpeed;
        Acceleration = navMeshAgent.acceleration;
        UpdatePosition = navMeshAgent.updatePosition;
        UpdateRotation = navMeshAgent.updateRotation;
        Radius = navMeshAgent.radius;
        Height = navMeshAgent.height;
        ObstacleAvoidanceType = (ObstacleAvoidanceTypeSerializer) navMeshAgent.obstacleAvoidanceType;
        Enabled = navMeshAgent.enabled;
    }

    // Empty constructor required for ProtoBuf
    private NavMeshAgentSerializer()
    {
        
    }
}