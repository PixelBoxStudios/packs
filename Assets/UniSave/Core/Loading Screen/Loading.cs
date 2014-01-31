using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    public static Loading Instance;

    void Awake()
	{
        Instance = this;
        DontDestroyOnLoad(this);
	}

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(UniSave.LoadAfterLoadingScreen());
    }
}
