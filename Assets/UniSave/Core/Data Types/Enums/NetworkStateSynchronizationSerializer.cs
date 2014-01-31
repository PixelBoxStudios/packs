using ProtoBuf;

[ProtoContract]
public enum NetworkStateSynchronizationSerializer
{
    Off,
    ReliableDeltaCompressed,
    Unreliable
}
