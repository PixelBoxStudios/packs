using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class InteractiveClothSerializer
{
    [ProtoMember(1)]  public float BendingStiffness { get; set; }
    [ProtoMember(2)]  public float StretchingStiffness { get; set; }
    [ProtoMember(3)]  public float Damping { get; set; }
    [ProtoMember(4)]  public float Thickness { get; set; }
    [ProtoMember(5)]  public Vector3Serializer ExternalAcceleration { get; set; }
    [ProtoMember(6)]  public Vector3Serializer RandomAcceleration { get; set; }
    [ProtoMember(7)]  public bool UseGravity { get; set; }
    [ProtoMember(8)]  public bool SelfCollision { get; set; }
    [ProtoMember(9)]  public bool Enabled { get; set; }
    [ProtoMember(11)] public MeshSerializer Mesh { get; set; }
    [ProtoMember(12)] public float Friction { get; set; }
    [ProtoMember(13)] public float Density { get; set; }
    [ProtoMember(14)] public float Pressure { get; set; }
    [ProtoMember(15)] public float CollisionResponse { get; set; }
    [ProtoMember(16)] public float TearFactor { get; set; }
    [ProtoMember(17)] public float AttachmentTearFactor { get; set; }
    [ProtoMember(18)] public float AttachmentResponse { get; set; }

    public InteractiveClothSerializer(GameObject gameObject, InteractiveClothSerializer component)
    {
        var interactiveClothObject = gameObject.GetComponent<InteractiveCloth>();

        if (interactiveClothObject == null)
            interactiveClothObject = gameObject.AddComponent<InteractiveCloth>();

        interactiveClothObject.bendingStiffness = component.BendingStiffness;
        interactiveClothObject.stretchingStiffness = component.StretchingStiffness;
        interactiveClothObject.damping = component.Damping;
        interactiveClothObject.thickness = component.Thickness;
        interactiveClothObject.externalAcceleration = (Vector3)component.ExternalAcceleration;
        interactiveClothObject.randomAcceleration = (Vector3)component.RandomAcceleration;
        interactiveClothObject.useGravity = component.UseGravity;
        interactiveClothObject.selfCollision = component.SelfCollision;
        interactiveClothObject.enabled = component.Enabled;

        if (component.Mesh != null)
        {
            interactiveClothObject.mesh = (Mesh) component.Mesh;
            interactiveClothObject.mesh.name = component.Mesh.MeshName;
        }

        interactiveClothObject.friction = component.Friction;
        interactiveClothObject.density = component.Density;
        interactiveClothObject.pressure = component.Pressure;
        interactiveClothObject.collisionResponse = component.CollisionResponse;
        interactiveClothObject.tearFactor = component.TearFactor;
        interactiveClothObject.attachmentTearFactor = component.AttachmentTearFactor;
        interactiveClothObject.attachmentResponse = component.AttachmentResponse;
    }

    public InteractiveClothSerializer(GameObject gameObject)
    {
        var interactiveClothObject = gameObject.GetComponent<InteractiveCloth>();

        BendingStiffness = interactiveClothObject.bendingStiffness;
        StretchingStiffness = interactiveClothObject.stretchingStiffness;
        Damping = interactiveClothObject.damping;
        Thickness = interactiveClothObject.thickness;
        ExternalAcceleration = (Vector3Serializer)interactiveClothObject.externalAcceleration;
        RandomAcceleration = (Vector3Serializer)interactiveClothObject.randomAcceleration;
        UseGravity = interactiveClothObject.useGravity;
        SelfCollision = interactiveClothObject.selfCollision;
        Enabled = interactiveClothObject.enabled;

        if (interactiveClothObject.mesh != null)
        {
            Mesh = (MeshSerializer) interactiveClothObject.mesh;
            Mesh.MeshName = interactiveClothObject.mesh.name;
        }

        Friction = interactiveClothObject.friction;
        Density = interactiveClothObject.density;
        Pressure = interactiveClothObject.pressure;
        CollisionResponse = interactiveClothObject.collisionResponse;
        TearFactor = interactiveClothObject.tearFactor;
        AttachmentTearFactor = interactiveClothObject.attachmentTearFactor;
        AttachmentResponse = interactiveClothObject.attachmentResponse;
    }

    // Empty constructor required for Protobuf
    private InteractiveClothSerializer()
    {
    }
}
