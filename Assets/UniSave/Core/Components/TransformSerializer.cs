using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class TransformSerializer
{
    [ProtoMember(1)]  public Vector3Serializer Position { get; set; }
    [ProtoMember(2)]  public Vector3Serializer LocalPosition { get; set; }
    [ProtoMember(3)]  public Vector3Serializer EulerAngles { get; set; }
    [ProtoMember(4)]  public Vector3Serializer LocalEulerAngles { get; set; }
    [ProtoMember(5)]  public Vector3Serializer Right { get; set; }
    [ProtoMember(6)]  public Vector3Serializer Up { get; set; }
    [ProtoMember(7)]  public Vector3Serializer Forward { get; set; }
    [ProtoMember(8)]  public QuaternionSerializer Rotation { get; set; }
    [ProtoMember(9)]  public QuaternionSerializer LocalRotation { get; set; }
    [ProtoMember(10)] public Vector3Serializer LocalScale { get; set; }
    [ProtoMember(11)] public TransformSerializer Parent { get; set; }
	
	public TransformSerializer(GameObject gameObject, TransformSerializer component)
	{
        gameObject.transform.position = (Vector3)component.Position;
		gameObject.transform.localPosition = (Vector3)component.LocalPosition;
		gameObject.transform.eulerAngles = (Vector3)component.EulerAngles;
		gameObject.transform.localEulerAngles = (Vector3)component.LocalEulerAngles;
		gameObject.transform.right = (Vector3)component.Right;
		gameObject.transform.up = (Vector3)component.Up;
		gameObject.transform.forward = (Vector3)component.Forward;
		//gameObject.transform.rotation = (Quaternion)component.Rotation;
		//gameObject.transform.localRotation = (Quaternion)component.LocalRotation;
		gameObject.transform.localScale = (Vector3)component.LocalScale;
	}
	
	public TransformSerializer(GameObject gameObject)
	{
		Position = (Vector3Serializer)gameObject.transform.position;
		LocalPosition = (Vector3Serializer)gameObject.transform.localPosition;
		EulerAngles = (Vector3Serializer)gameObject.transform.eulerAngles;
		LocalEulerAngles = (Vector3Serializer)gameObject.transform.localEulerAngles;
		Right = (Vector3Serializer)gameObject.transform.right;
		Up = (Vector3Serializer)gameObject.transform.up;
		Forward = (Vector3Serializer)gameObject.transform.forward;
		//Rotation = (QuaternionSerializer)gameObject.transform.rotation;
		//LocalRotation = (QuaternionSerializer)gameObject.transform.localRotation;
		LocalScale = (Vector3Serializer)gameObject.transform.localScale;
	}

    // Required for Protobuf
    public TransformSerializer()
    {
    }
}
