using UnityEngine;

[CreateAssetMenu(menuName = "Gameplay Config", fileName = "New Gameplay Config")]
public class GameplayConfig : ScriptableObject
{
    [Header("Motion")]
    [SerializeField, Range(0, 10)] private float _motionSpeed;

    [Header("Falling")]
    [SerializeField, Range(0, 10)] private float _fallingSpeed;

    [Header("Platform Generation")]
    [SerializeField, Range(0.5f, 100)] private float _minimalPlatformSize;
    [SerializeField, Range(0.1f, 100)] private float _maximalPlatformSize;
    [SerializeField, Range(0.5f, 100)] private float _minimalAbysWidth = 0.5f;
    [SerializeField, Range(0, 10)] private int _startablePlatformCount;

    [Header("Bridge Building")]
    [SerializeField, Range(0, 10)] private float _bridgeBuildingSpeed;
    [SerializeField, Range(0, 10)] private float _bridgeLayingSpeed;
    [SerializeField, Range(0, 5)] private float _buildingIndent;

    [Header("Score Counting")]
    [SerializeField, Range(0, 5)] private float _punch;

    public float MotionSpeed => _motionSpeed;
    public float FallingSpeed => _fallingSpeed;
    public float MinimalPlatformSize => _minimalPlatformSize;
    public float MaximalPlatformSize => _maximalPlatformSize;
    public float MinimalAbysWidth => _minimalAbysWidth;
    public int StartablePlatformCount => _startablePlatformCount;
    public float BridgeBuildingSpeed => _bridgeBuildingSpeed;
    public float BridgeLayingSpeed => _bridgeLayingSpeed;
    public float BuildingIndent => _buildingIndent;
    public float Punch => _punch;
}