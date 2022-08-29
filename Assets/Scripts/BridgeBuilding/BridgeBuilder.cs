using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using PlatformGeneration;

namespace BridgeBuilding
{
    public class BridgeBuilder : ConfigReceiver
    {
        public event Action<bool, float> OnBuildingComplete;

        private Bridge _prefab;
        private ObjectPool<Bridge> _bridges;
        private Transform _levelParent;
        private Transform _container;
        private bool _isBuilding;

        private PlatformGenerator _platformGenerator;
        private Animator _animator;

        public float Indent => _config.BuildingIndent;

        private void Awake()
        {
            _prefab = Resources.Load<Bridge>("Actors/Bridge");
            _bridges = new(Create, Get, Return, null, false);
            _levelParent = FindObjectOfType<Level>().transform;
            _container = new GameObject("Bridges").transform;
            _container.transform.parent = _levelParent;
            _platformGenerator = FindObjectOfType<PlatformGenerator>();
            _animator = FindObjectOfType<Character>().GetComponent<Animator>();
        }

        public void Build()
        {
            _isBuilding = true;
            StartCoroutine(Building());
        }

        private IEnumerator Building()
        {
            Platform platform = _platformGenerator.Get();
            float limit = PlatformGenerator.MaximalDistance;
            Vector3 spawnPosition = transform.position + Vector3.right * _config.BuildingIndent;
            Bridge bridge = _bridges.Get();

            spawnPosition.y = PlatformGenerator.Apex;
            bridge.transform.position = spawnPosition;

            while (_isBuilding)
            {
                if (Input.GetMouseButtonDown(0))
                    _animator.Play("Building");

                if (Input.GetMouseButton(0))
                {
                    if (bridge.transform.localScale.y >= limit)
                        _isBuilding = false;

                    bridge.transform.localScale = Vector3
                        .MoveTowards(bridge.transform.localScale, bridge.transform.localScale + Vector3.up * limit, _config.BridgeBuildingSpeed * Time.deltaTime);
                }

                if (Input.GetMouseButtonUp(0))
                    _isBuilding = false;

                yield return null;
            }

            _animator.Play("Idle");
            yield return Laying(bridge);

            OnBuildingComplete?.Invoke
            (
                bridge.Length >= platform.AbysWidth && bridge.Length <= platform.AbysWidth + platform.Width,
                bridge.Length >= platform.AbysWidth && bridge.Length <= platform.AbysWidth + platform.Width
                    ? platform.AbysWidth + platform.Width
                    : bridge.Length
            );

            yield break;
        }

        private IEnumerator Laying(Bridge bridge)
        {
            Quaternion targetRotation = Quaternion.Euler(Vector3.forward * -90);
            float speedFactor = 10;

            while (bridge.transform.rotation != targetRotation)
            {
                bridge.transform.rotation = Quaternion
                    .RotateTowards(bridge.transform.rotation, targetRotation, _config.BridgeLayingSpeed * speedFactor * Time.deltaTime);

                yield return null;
            }

            yield break;
        }

        private void Return(Bridge bridge) => bridge.gameObject.SetActive(false);

        private void Get(Bridge bridge)
        {
            bridge.transform.rotation = Quaternion.Euler(Vector3.zero);
            bridge.transform.localScale = new(0.25f, 0, 1);
            bridge.gameObject.SetActive(true);
        }

        private Bridge Create()
        {
            Bridge bridge = Instantiate(_prefab, _container);
            bridge.GetComponent<Disappearer>().OnDisappear += () => _bridges.Release(bridge);
            return bridge;
        }
    }
}