using UnityEngine;
using DG.Tweening;
using System;
using PlatformGeneration;

namespace Motion
{
    public class Faller : ConfigReceiver
    {
        public event Action OnFalled;

        public void Fall() => transform
            .DOMove(transform.position + Vector3.down * Platform.Height, Platform.Height / _config.FallingSpeed)
            .OnComplete(() => OnFalled?.Invoke());
    }
}