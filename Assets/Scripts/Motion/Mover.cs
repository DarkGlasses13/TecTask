using UnityEngine;
using DG.Tweening;
using BridgeBuilding;
using PlatformGeneration;
using ScoreCounting;

namespace Motion
{
    [RequireComponent(typeof(Faller), typeof(BridgeBuilder))]
    public class Mover : ConfigReceiver
    {
        private PlatformGenerator _platformGenerator;
        private Faller _faller;
        private BridgeBuilder _bridgeBuilder;
        private ScoreCounter _scoreCounter;
        private Camera _camera;
        private Animator _animator;

        private void Awake()
        {
            _platformGenerator = FindObjectOfType<PlatformGenerator>();
            _faller = GetComponent<Faller>();
            _bridgeBuilder = GetComponent<BridgeBuilder>();
            _scoreCounter = GetComponent<ScoreCounter>();
            _camera = Camera.main;
            _animator = GetComponent<Animator>();
        }

        private void OnEnable() => _bridgeBuilder.OnBuildingComplete += Move;

        public void StartMoving() => Move(true, _platformGenerator.Get().Width * 0.5f - _config.BuildingIndent);

        public void Move(bool isSuccess, float distance)
        {
            Tweener motionTweener = transform.DOMove(transform.position + Vector3.right * distance, distance / _config.MotionSpeed);
            _animator.Play("Walking");

            if (isSuccess)
                motionTweener.OnComplete(() =>
                {
                    _bridgeBuilder.Build();
                    _animator.Play("Idle");
                    _scoreCounter.Add();

                }).SetEase(Ease.Linear);
            else
                motionTweener.OnComplete(() =>
                {
                    _camera.transform.parent = _camera.transform.parent.parent;
                    _faller.Fall();
                    _scoreCounter.Reset();

                }).SetEase(Ease.Linear);
        }

        private void OnDisable() => _bridgeBuilder.OnBuildingComplete -= Move;
    }
}