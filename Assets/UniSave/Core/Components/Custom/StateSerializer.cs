using UnityEngine;
using System.Collections.Generic;
using ProtoBuf;

[ProtoContract]
public sealed class StateSerializer
{
    [ProtoMember(1)] public bool IsSpawnedAtRuntime;
    [ProtoMember(2)] public List<int> List = new List<int>();
    [ProtoMember(3)] public string UniqueName;
	
	public StateSerializer(GameObject gameObject, StateSerializer component)
	{
		var state = gameObject.GetComponent<State>();
		
		if (state != null)
		{
			state.IsSpawnedAtRuntime = component.IsSpawnedAtRuntime;
			state.List = component.List;
			state.UniqueName = component.UniqueName;
		}
		else
		{
			var spawnState = gameObject.AddComponent<State>();
			spawnState.IsSpawnedAtRuntime = component.IsSpawnedAtRuntime;
			spawnState.List = component.List;
			spawnState.UniqueName = component.UniqueName;
		}
	}
	
	public StateSerializer(GameObject gameObject)
	{
		var state = gameObject.GetComponent<State>();
		IsSpawnedAtRuntime = state.IsSpawnedAtRuntime;
		List = state.List;
		UniqueName = state.UniqueName;
	}

    // Required for Protobuf
    private StateSerializer()
    {
    }
}