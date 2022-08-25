using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BridgeBuilder : MonoBehaviour
{
    public event Action OnBuildingComplete;

    [SerializeField, Range(0, 10)] private float _buildingSpeed;
    [SerializeField, Range(0, 10)] private float _layingSpeed;

    private Bridge _prefab;
    private ObjectPool<Bridge> _bridges;
    private Transform _levelParent;
    private Transform _container;
    private bool _isBuilding;

    private void Awake()
    {
        _prefab = Resources.Load<Bridge>("Bridge");
        _bridges = new(Create, Get, Return);
        _levelParent = FindObjectOfType<Level>().transform;
        _container = new GameObject("Bridges").transform;
        _container.transform.parent = _levelParent;
    }

    private IEnumerator Building()
    {
        _isBuilding = true;
        float limit = PlatformGenerator.MaximalDistance;
        Bridge bridge = _bridges.Get();
        bridge.transform.position = Vector3.up * PlatformGenerator.Apex;

        while (_isBuilding)
        {
            if (bridge.transform.localScale.y == limit)
                _isBuilding = false;

            bridge.transform.localScale = Vector3
                .MoveTowards(bridge.transform.localScale, bridge.transform.localScale + Vector3.up * limit, _buildingSpeed * Time.deltaTime);

            yield return null;
        }

        yield return Laying(bridge);
        OnBuildingComplete?.Invoke();
        yield break;
    }

    private IEnumerator Laying(Bridge bridge)
    {
        Quaternion targetRotation = Quaternion.Euler(Vector3.forward * -90);
        float speedFactor = 10;

        while (bridge.transform.rotation != targetRotation)
        {
            bridge.transform.rotation = Quaternion
                .RotateTowards(bridge.transform.rotation, targetRotation, _layingSpeed * speedFactor * Time.deltaTime);

            yield return null;
        }

        yield break;
    }

    private void Return(Bridge bridge) => bridge.gameObject.SetActive(false);

    private void Get(Bridge bridge) => bridge.gameObject.SetActive(true);

    private Bridge Create()
    {
        Bridge bridge = Instantiate(_prefab, _container);
        Return(bridge);
        return bridge;
    }
}