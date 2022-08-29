using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace PlatformGeneration
{
    public class PlatformGenerator : ConfigReceiver
    {
        private Platform _prefab;
        private ObjectPool<Platform> _platforms;
        private Platform _lastPlatform;
        private Queue<Platform> _spawnedPlatforms = new();

        private Camera _camera;

        public static float Apex { get; private set; }
        public static float MaximalDistance { get; private set; }

        private void Awake()
        {
            float aspect = (Screen.width > Screen.height) ? (Screen.width / Screen.height) : (Screen.height / Screen.width);
            _camera = Camera.main;
            _prefab = Resources.Load<Platform>("Actors/Platform");
            _lastPlatform = FindObjectOfType<Platform>();
            Apex = (_lastPlatform.transform.position + Vector3.up * (Platform.Height * 0.5f)).y;
            MaximalDistance = _camera.orthographicSize * 2 * aspect - Mathf.Abs(Apex);

            _spawnedPlatforms.Enqueue(_prefab);
        }

        private void Start()
        {
            _platforms = new(Create, Get, Return, null, false);

            for (int i = 0; i < _config.StartablePlatformCount; i++)
                _platforms.Get();
        }

        public Platform Get()
        {
            Platform platform = _spawnedPlatforms.Dequeue();
            _platforms.Get();
            return platform;
        }

        private void Return(Platform platform) => platform.gameObject.SetActive(false);

        private void Get(Platform platform)
        {
            float abysWidth = Random.Range(_config.MinimalAbysWidth, MaximalDistance);
            platform.SetSize(Random.Range(_config.MinimalPlatformSize, _config.MaximalPlatformSize));
            platform.transform.position = _lastPlatform.transform.position + Vector3.right * ((platform.Width + _lastPlatform.Width) * 0.5f + abysWidth);
            _lastPlatform = platform;
            _lastPlatform.SetAbysWidth(abysWidth);
            _spawnedPlatforms.Enqueue(platform);
            platform.gameObject.SetActive(true);
        }

        private Platform Create()
        {
            Platform platform = Instantiate(_prefab, _lastPlatform.transform.parent);
            platform.name = "Platform";
            platform.GetComponent<Disappearer>().OnDisappear += () => _platforms.Release(platform);
            return platform;
        }
    }
}