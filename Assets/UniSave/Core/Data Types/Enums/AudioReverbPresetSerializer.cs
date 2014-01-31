﻿using ProtoBuf;

[ProtoContract]
public enum AudioReverbPresetSerializer
{
    Off,
    Generic,
    PaddedCell,
    Room,
    Bathroom,
    Livingroom,
    Stoneroom,
    Auditorium,
    Concerthall,
    Cave,
    Arena,
    Hangar,
    CarpetedHallway,
    Hallway,
    StoneCorridor,
    Alley,
    Forest,
    City,
    Mountains,
    Quarry,
    Plain,
    ParkingLot,
    SewerPipe,
    Underwater,
    Drugged,
    Dizzy,
    Psychotic,
    User
}