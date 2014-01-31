using System;
using System.Linq;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class CharacterJointSerializer
{
    [ProtoMember(1)]  public string ConnectedBodyName { get; set; }
    [ProtoMember(2)]  public Vector3Serializer Axis { get; set; }
    [ProtoMember(3)]  public Vector3Serializer Anchor { get; set; }
    [ProtoMember(4)]  public float BreakForce { get; set; }
    [ProtoMember(5)]  public float BreakTorque { get; set; }
	[ProtoMember(6)]  public Vector3Serializer SwingAxis { get; set; }
	[ProtoMember(7)]  public SoftJointLimitSerializer LowTwistLimit { get; set; }
	[ProtoMember(8)]  public SoftJointLimitSerializer HighTwistLimit { get; set; }
	[ProtoMember(9)]  public SoftJointLimitSerializer Swing1Limit { get; set; }
	[ProtoMember(10)] public SoftJointLimitSerializer Swing2Limit { get; set; }
	[ProtoMember(11)] public QuaternionSerializer TargetRotation { get; set; }
	[ProtoMember(12)] public Vector3Serializer TargetAngularVelocity { get; set; }
	[ProtoMember(13)] public JointDriveSerializer RotationDrive { get; set; }

    public CharacterJointSerializer(GameObject gameObject, CharacterJointSerializer component)
	{
        var characterJoint = gameObject.GetComponent<CharacterJoint>();

        if (characterJoint == null)
            characterJoint = gameObject.AddComponent<CharacterJoint>();

        if (!String.IsNullOrEmpty(component.ConnectedBodyName))
        {
            var rigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            if (rigidBodies != null)
                characterJoint.connectedBody = rigidBodies.FirstOrDefault(rigidBody => rigidBody.name == component.ConnectedBodyName);
        }

        characterJoint.axis = (Vector3) component.Axis;
        characterJoint.anchor = (Vector3) component.Anchor;
        characterJoint.breakForce = component.BreakForce;
        characterJoint.breakTorque = component.BreakTorque;
        characterJoint.swingAxis = (Vector3) component.SwingAxis;
        characterJoint.lowTwistLimit = (SoftJointLimit) component.LowTwistLimit;
        characterJoint.highTwistLimit = (SoftJointLimit) component.HighTwistLimit;
        characterJoint.swing1Limit = (SoftJointLimit) component.Swing1Limit;
        characterJoint.swing2Limit = (SoftJointLimit) component.Swing2Limit;
        characterJoint.targetRotation = (Quaternion) component.TargetRotation;
        characterJoint.targetAngularVelocity = (Vector3) component.TargetAngularVelocity;
        characterJoint.rotationDrive = (JointDrive) component.RotationDrive;
	}
	
	public CharacterJointSerializer(GameObject gameObject)
	{
        var characterJoint = gameObject.GetComponent<CharacterJoint>();

        if (characterJoint.connectedBody != null)
            ConnectedBodyName = characterJoint.connectedBody.name;

        Axis = (Vector3Serializer) characterJoint.axis;
        Anchor = (Vector3Serializer) characterJoint.anchor;
        BreakForce = characterJoint.breakForce;
        BreakTorque = characterJoint.breakTorque;
	    SwingAxis = (Vector3Serializer) characterJoint.swingAxis;
	    LowTwistLimit = (SoftJointLimitSerializer) characterJoint.lowTwistLimit;
	    HighTwistLimit = (SoftJointLimitSerializer) characterJoint.highTwistLimit;
	    Swing1Limit = (SoftJointLimitSerializer) characterJoint.swing1Limit;
	    Swing2Limit = (SoftJointLimitSerializer) characterJoint.swing2Limit;
	    TargetRotation = (QuaternionSerializer) characterJoint.targetRotation;
	    TargetAngularVelocity = (Vector3Serializer) characterJoint.targetAngularVelocity;
	    RotationDrive = (JointDriveSerializer) characterJoint.rotationDrive;
	}

    // Empty constructor required for ProtoBuf
    private CharacterJointSerializer()
    {
    }
}
