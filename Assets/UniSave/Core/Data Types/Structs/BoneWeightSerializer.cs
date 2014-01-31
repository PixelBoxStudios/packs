using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct BoneWeightSerializer
{
    [ProtoMember(1)] public float Weight0 { get; set; }
    [ProtoMember(2)] public float Weight1 { get; set; }
    [ProtoMember(3)] public float Weight2 { get; set; }
    [ProtoMember(4)] public float Weight3 { get; set; }
    [ProtoMember(5)] public int BoneIndex0 { get; set; }
    [ProtoMember(6)] public int BoneIndex1 { get; set; }
    [ProtoMember(7)] public int BoneIndex2 { get; set; }
    [ProtoMember(8)] public int BoneIndex3 { get; set; }

    public BoneWeightSerializer(BoneWeight data) : this()
    {
        Weight0 = data.weight0;
        Weight1 = data.weight1;
        Weight2 = data.weight2;
        Weight3 = data.weight3;
        BoneIndex0 = data.boneIndex0;
        BoneIndex1 = data.boneIndex1;
        BoneIndex2 = data.boneIndex2;
        BoneIndex3 = data.boneIndex3;
    }

    public static explicit operator BoneWeight(BoneWeightSerializer data)
    {
        var boneWeight = new BoneWeight
        {
            weight0 = data.Weight0,
            weight1 = data.Weight1,
            weight2 = data.Weight2,
            weight3 = data.Weight3,
            boneIndex0 = data.BoneIndex0,
            boneIndex1 = data.BoneIndex1,
            boneIndex2 = data.BoneIndex2,
            boneIndex3 = data.BoneIndex3
        };

        return boneWeight;
    }

    public static explicit operator BoneWeightSerializer(BoneWeight data)
    {
        return new BoneWeightSerializer(data);
    }
}