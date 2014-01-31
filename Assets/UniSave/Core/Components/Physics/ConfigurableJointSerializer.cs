using System.Linq;
using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class ConfigurableJointSerializer
{
    [ProtoMember(1)]  public string ConnectedBodyName { get; set; }
    [ProtoMember(2)]  public Vector3Serializer Axis { get; set; }
    [ProtoMember(3)]  public Vector3Serializer Anchor { get; set; }
    [ProtoMember(4)]  public float BreakForce { get; set; }
    [ProtoMember(5)]  public float BreakTorque { get; set; }
	[ProtoMember(6)]  public Vector3Serializer SecondaryAxis { get; set; }
	[ProtoMember(7)]  public ConfigurableJointMotionSerializer XMotion { get; set; }
	[ProtoMember(8)]  public ConfigurableJointMotionSerializer YMotion { get; set; }
	[ProtoMember(9)]  public ConfigurableJointMotionSerializer ZMotion { get; set; }
	[ProtoMember(10)] public ConfigurableJointMotionSerializer AngularXMotion { get; set; }
	[ProtoMember(11)] public ConfigurableJointMotionSerializer AngularYMotion { get; set; }
	[ProtoMember(12)] public ConfigurableJointMotionSerializer AngularZMotion { get; set; }
	[ProtoMember(13)] public SoftJointLimitSerializer LinearLimit { get; set; }
	[ProtoMember(14)] public SoftJointLimitSerializer LowAngularXLimit { get; set; }
	[ProtoMember(15)] public SoftJointLimitSerializer HighAngularXLimit { get; set; }
	[ProtoMember(16)] public SoftJointLimitSerializer AngularYLimit { get; set; }
	[ProtoMember(17)] public SoftJointLimitSerializer AngularZLimit { get; set; }
	[ProtoMember(18)] public Vector3Serializer TargetPosition { get; set; }
	[ProtoMember(19)] public Vector3Serializer TargetVelocity { get; set; }
	[ProtoMember(20)] public JointDriveSerializer XDrive { get; set; }
	[ProtoMember(21)] public JointDriveSerializer YDrive { get; set; }
	[ProtoMember(22)] public JointDriveSerializer ZDrive { get; set; }
	[ProtoMember(23)] public QuaternionSerializer TargetRotation { get; set; }
	[ProtoMember(24)] public Vector3Serializer TargetAngularVelocity { get; set; }
	[ProtoMember(25)] public RotationDriveModeSerializer RotationDriveMode { get; set; }
	[ProtoMember(26)] public JointDriveSerializer AngularXDrive { get; set; }
	[ProtoMember(27)] public JointDriveSerializer AngularYZDrive { get; set; }
	[ProtoMember(28)] public JointDriveSerializer SlerpDrive { get; set; }
	[ProtoMember(29)] public JointProjectionModeSerializer ProjectionMode { get; set; }
	[ProtoMember(30)] public float ProjectionDistance { get; set; }
	[ProtoMember(31)] public float ProjectionAngle { get; set; }
	[ProtoMember(32)] public bool ConfiguredInWorldSpace { get; set; }

    public ConfigurableJointSerializer(GameObject gameObject, ConfigurableJointSerializer component)
	{
        var configurableJoint = gameObject.GetComponent<ConfigurableJoint>();

        if (configurableJoint == null)
            configurableJoint = gameObject.AddComponent<ConfigurableJoint>();

        if (!String.IsNullOrEmpty(component.ConnectedBodyName))
        {
            var rigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            if (rigidBodies != null)
                configurableJoint.connectedBody = rigidBodies.FirstOrDefault(rigidBody => rigidBody.name == component.ConnectedBodyName);
        }

        configurableJoint.axis = (Vector3) component.Axis;
        configurableJoint.anchor = (Vector3) component.Anchor;
        configurableJoint.breakForce = component.BreakForce;
        configurableJoint.breakTorque = component.BreakTorque;
        configurableJoint.secondaryAxis = (Vector3) component.SecondaryAxis;
        configurableJoint.xMotion = (ConfigurableJointMotion) component.XMotion;
        configurableJoint.yMotion = (ConfigurableJointMotion) component.YMotion;
        configurableJoint.zMotion = (ConfigurableJointMotion) component.ZMotion;
        configurableJoint.angularXMotion = (ConfigurableJointMotion) component.AngularXMotion;
        configurableJoint.angularYMotion = (ConfigurableJointMotion) component.AngularYMotion;
        configurableJoint.angularZMotion = (ConfigurableJointMotion) component.AngularZMotion;
        configurableJoint.linearLimit = (SoftJointLimit) component.LinearLimit;
        configurableJoint.lowAngularXLimit = (SoftJointLimit) component.LowAngularXLimit;
        configurableJoint.highAngularXLimit = (SoftJointLimit) component.HighAngularXLimit;
        configurableJoint.angularYLimit = (SoftJointLimit) component.AngularYLimit;
        configurableJoint.angularZLimit = (SoftJointLimit) component.AngularZLimit;
        configurableJoint.targetPosition = (Vector3) component.TargetPosition;
        configurableJoint.targetVelocity = (Vector3) component.TargetVelocity;
        configurableJoint.xDrive = (JointDrive) component.XDrive;
        configurableJoint.yDrive = (JointDrive) component.YDrive;
        configurableJoint.zDrive = (JointDrive) component.ZDrive;
        configurableJoint.targetRotation = (Quaternion) component.TargetRotation;
        configurableJoint.targetAngularVelocity = (Vector3) component.TargetAngularVelocity;
        configurableJoint.rotationDriveMode = (RotationDriveMode) component.RotationDriveMode;
        configurableJoint.angularXDrive = (JointDrive) component.AngularXDrive;
        configurableJoint.angularYZDrive = (JointDrive) component.AngularYZDrive;
        configurableJoint.slerpDrive = (JointDrive) component.SlerpDrive;
        configurableJoint.projectionMode = (JointProjectionMode) component.ProjectionMode;
        configurableJoint.projectionDistance = component.ProjectionDistance;
        configurableJoint.projectionAngle = component.ProjectionAngle;
        configurableJoint.configuredInWorldSpace = component.ConfiguredInWorldSpace;
	}
	
	public ConfigurableJointSerializer(GameObject gameObject)
	{
        var configurableJoint = gameObject.GetComponent<ConfigurableJoint>();

        if (configurableJoint.connectedBody != null)
            ConnectedBodyName = configurableJoint.connectedBody.name;

        Axis = (Vector3Serializer) configurableJoint.axis;
        Anchor = (Vector3Serializer) configurableJoint.anchor;
        BreakForce = configurableJoint.breakForce;
        BreakTorque = configurableJoint.breakTorque;
	    SecondaryAxis = (Vector3Serializer) configurableJoint.secondaryAxis;
	    XMotion = (ConfigurableJointMotionSerializer) configurableJoint.xMotion;
	    YMotion = (ConfigurableJointMotionSerializer) configurableJoint.yMotion;
	    ZMotion = (ConfigurableJointMotionSerializer) configurableJoint.zMotion;
	    AngularXMotion = (ConfigurableJointMotionSerializer) configurableJoint.angularXMotion;
	    AngularYMotion = (ConfigurableJointMotionSerializer) configurableJoint.angularYMotion;
	    AngularZMotion = (ConfigurableJointMotionSerializer) configurableJoint.angularZMotion;
	    LinearLimit = (SoftJointLimitSerializer) configurableJoint.linearLimit;
	    LowAngularXLimit = (SoftJointLimitSerializer) configurableJoint.lowAngularXLimit;
	    HighAngularXLimit = (SoftJointLimitSerializer) configurableJoint.highAngularXLimit;
	    AngularYLimit = (SoftJointLimitSerializer) configurableJoint.angularYLimit;
	    AngularZLimit = (SoftJointLimitSerializer) configurableJoint.angularZLimit;
	    TargetPosition = (Vector3Serializer) configurableJoint.targetPosition;
	    TargetVelocity = (Vector3Serializer) configurableJoint.targetVelocity;
	    XDrive = (JointDriveSerializer) configurableJoint.xDrive;
	    YDrive = (JointDriveSerializer) configurableJoint.yDrive;
	    ZDrive = (JointDriveSerializer) configurableJoint.zDrive;
        TargetRotation = (QuaternionSerializer) configurableJoint.targetRotation;
	    TargetAngularVelocity = (Vector3Serializer) configurableJoint.targetAngularVelocity;
	    RotationDriveMode = (RotationDriveModeSerializer) configurableJoint.rotationDriveMode;
	    AngularXDrive = (JointDriveSerializer) configurableJoint.angularXDrive;
	    AngularYZDrive = (JointDriveSerializer) configurableJoint.angularYZDrive;
	    SlerpDrive = (JointDriveSerializer) configurableJoint.slerpDrive;
	    ProjectionMode = (JointProjectionModeSerializer) configurableJoint.projectionMode;
	    ProjectionDistance = configurableJoint.projectionDistance;
	    ProjectionAngle = configurableJoint.projectionAngle;
	    ConfiguredInWorldSpace = configurableJoint.configuredInWorldSpace;
	}

    // Empty constructor required for ProtoBuf
    private ConfigurableJointSerializer()
    {
    }
}
