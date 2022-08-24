using UnityEngine;
using UnityEngine.Pool;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100)] private float _minimalSize;
    [SerializeField, Range(0.1f, 100)] private float _maximalSize;
    [SerializeField, Range(0.1f, 100)] private float _minimalDistance;

    private float _maximalDistance;

    private Platform _prefab;
    private ObjectPool<Platform> _platforms;
    private Platform _lastPlatform;

    private Camera _camera;

    private void Awake()
    {
        float aspect = (Screen.width > Screen.height) ? (Screen.width / Screen.height) : (Screen.height / Screen.width);
        _camera = Camera.main;
        _maximalDistance = _camera.orthographicSize * 2 * aspect;
        _prefab = Resources.Load<Platform>("Platform");
        _platforms = new(Create, Get, Return, null);
        _lastPlatform = FindObjectOfType<Platform>();
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
            Spawn();
    }

    private void Spawn()
    {
        float distance = Random.Range(_minimalDistance, _maximalDistance);
        Platform platform = _platforms.Get();
        platform.transform.position = _lastPlatform.transform.position + Vector3.right * ((platform.Size + _lastPlatform.Size) * 0.5f + distance);
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