using UnityEngine;

public class Controls : MonoBehaviour
{
    private Transform _player;
    private GameObject _spawnpoint;
    public GameObject Box;
    private RaycastHit _raycastHitInfo;

    private Light _directionalLight;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _spawnpoint = GameObject.Find("Box Spawnpoint");

        _directionalLight = GameObject.Find("Directional light").GetComponent<Light>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var pos = new Vector3(Random.Range(_spawnpoint.transform.position.x - 4.0f, _spawnpoint.transform.position.x + 4.0f),
                                      _spawnpoint.transform.position.y, Random.Range(_spawnpoint.transform.position.z - 2.0f, _spawnpoint.transform.position.z + 2.0f));
            Instantiate(Box, pos, Quaternion.identity);
        }

        Vector3 fwd = GameObject.FindGameObjectWithTag("MainCamera").transform.TransformDirection(Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(_player.position, fwd, out _raycastHitInfo, 2))
            {
                if (_raycastHitInfo.transform.name == Box.name || _raycastHitInfo.transform.name == Box.name + "(Clone)")
                    Destroy(_raycastHitInfo.transform.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            RenderSettings.skybox = (Material)Resources.Load("Eerie Skybox");

            RenderSettings.ambientLight = new Color(24.0f / 255.0f, 49.0f / 255.0f, 60.0f / 255.0f);

            _directionalLight.color = new Color(136.0f / 255.0f, 176.0f / 255.0f, 178.0f / 255.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            RenderSettings.skybox = (Material)Resources.Load("DawnDusk Skybox");

            RenderSettings.ambientLight = new Color(93.0f / 255.0f, 106.0f / 255.0f, 99.0f / 255.0f);

            _directionalLight.color = new Color(254.0f / 255.0f, 253.0f / 255.0f, 178.0f / 255.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RenderSettings.skybox = (Material)Resources.Load("Sunny2 Skybox");

            RenderSettings.ambientLight = new Color(197.0f / 255.0f, 232.0f / 255.0f, 250.0f / 255.0f);

            _directionalLight.color = new Color(242.0f / 255.0f, 247.0f / 255.0f, 251.0f / 255.0f);
        }
    }

    public void OnGUI()
    {
        if (TestGUI.Instance.CurrentState.Method.Name == "InGame")
        {
            GUI.Box(new Rect(20, Screen.height - (100 + 20), 520, 100), "Press E to spawn wooden boxes." +
                                                                        System.Environment.NewLine +
                                                                        "Press Left Mouse Button to remove a wooden box when standing close and looking at it." +
                                                                        System.Environment.NewLine +
                                                                        "Press 1, 2, or 3 to change the Skybox" +
                                                                        System.Environment.NewLine +
                                                                        System.Environment.NewLine +
                                                                        "To Save/Load, press Escape to bring up the menu.");
        }
    }
}
