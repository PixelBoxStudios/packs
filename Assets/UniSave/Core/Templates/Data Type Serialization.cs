/* UniSave - Data Type Serialization File Template
 * 
 * Change DataTypeName and Properties
 * 
 * Parts are commented out to prevent errors.
 */

using UnityEngine;
using ProtoBuf;

[ProtoContract]
public class DataTypeNameSerializer
{
    // [ProtoMember(1)] Property1
    // [ProtoMember(2)] Property2
    // [ProtoMember(3)] Property3
    // [ProtoMember(4)] Property4

    /*
    public DataTypeNameSerializer(DataTypeName data)
    {
        Property1 = data.Property1;
        Property2 = data.Property2;
        etc.
    }
    */

    /*
    public static explicit operator DataTypeName(DataTypeNameSerializer data)
    {
        return new DataTypeName(data.Property1, data.Property2, etc.);
    }
    */

    /*
    public static explicit operator DataTypeNameSerializer(DataTypeName data)
    {
        return new DataTypeNameSerializer(data);
    }
    */

    // Empty constructor required for ProtoBuf.
    public DataTypeNameSerializer()
    {
    }
}