using ProtoBuf;

[ProtoContract]
public enum ObstacleAvoidanceTypeSerializer
{
    NoObstacleAvoidance,
    LowQualityObstacleAvoidance,
    MedQualityObstacleAvoidance,
    GoodQualityObstacleAvoidance,
    HighQualityObstacleAvoidance
}
