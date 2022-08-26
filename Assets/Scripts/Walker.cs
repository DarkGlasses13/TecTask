using Assets.Scripts;
using UnityEngine;

public class Walker : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _speed; 

    private GroundDetector _detector;

    private void Awake() => _detector = GetComponent<GroundDetector>();

    private void Update()
    {
        if (_detector.IsGround())
            Walk();
    }

    private void Walk() => transform.Translate(_speed * Time.deltaTime * Vector3.right);
}