using UnityEngine;
using UnityEngine.Pool;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100)] private float _minimalSize;
    [SerializeField, Range(0.1f, 100)] private float _maximalSize;
    [SerializeField, Range(0.1f, 100)] private float _minimalDistance;

    private Platform _prefab;
    private ObjectPool<Platform> _platforms;
    private Platform _lastPlatform;

    private Camera _camera;

    public static float Apex { get; private set; }
    public static float MaximalDistance { get; private set; }

    private void Awake()
    {
        float aspect = (Screen.width > Screen.height) ? (Screen.width / Screen.height) : (Screen.height / Screen.width);
        Platform startablePlatform = FindObjectOfType<Platform>();
        _camera = Camera.main;
        _prefab = Resources.Load<Platform>("Platform");
        _platforms = new(Create, Get, Return);
        _lastPlatform = startablePlatform;
        Apex = (startablePlatform.transform.position + Vector3.up * (startablePlatform.Size.y * 0.5f)).y;
        MaximalDistance = _camera.orthographicSize * 2 * aspect - Mathf.Abs(Apex);
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
            Spawn();
    }

    private void Spawn()
    {
        float distance = Random.Range(_minimalDistance, MaximalDistance);
        Platform platform = _platforms.Get();
        platform.transform.position = _lastPlatform.transform.position + Vector3.right * ((platform.Size.x + _lastPlatform.Size.x) * 0.5f + distance);
        _lastPlatform = platform;
    }

    private void Return(Platform platform) => platform.gameObject.SetActive(false);

    private void Get(Platform platform)
    {
        platform.SetSize(Random.Range(_minimalSize, _maximalSize));
        platform.gameObject.SetActive(true);
    }

    private Platform Create()
    {
        Platform platform = Instantiate(_prefab, _lastPlatform.transform.parent);
        platform.name = "Platform";
        Return(platform);
        return platform;
    }
}