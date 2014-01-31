using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(State))]
public class StateEditor : Editor
{
	public override void OnInspectorGUI()
	{
		var t = (State)target;
		
		t.IsSpawnedAtRuntime = EditorGUILayout.Toggle("Spawned", t.IsSpawnedAtRuntime);
		
		if (!t.IsSpawnedAtRuntime)
		{
			for (var i = 0; i < t.List.Count; i++)
			{
				EditorGUILayout.PrefixLabel("Component");
				t.List[i] = EditorGUILayout.Popup(t.List[i], t.PopupList);
				EditorGUILayout.Space();
			}
			
			EditorGUILayout.Space();
			
			if (GUILayout.Button("Add"))
			{
                if (t.ComponentList.Count - 1 > t.SelectionIndex)
                {
                    t.SelectionIndex++;
                    t.List.Add(t.SelectionIndex);
                }
			}

		    if (GUILayout.Button("Remove"))
		    {

				if (t.List.Count > 1)
				{
					t.List.RemoveAt(t.List.Count - 1);
                    t.SelectionIndex--;
				}
			}
			
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			
			if (GUILayout.Button("Refresh"))
			{
				t.CheckComponents();
			}
		}
		
		if (GUI.changed)
			EditorUtility.SetDirty(target);
	}
}
