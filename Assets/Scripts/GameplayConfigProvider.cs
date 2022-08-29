using System.Linq;
using UnityEngine;

public class GameplayConfigProvider : MonoBehaviour
{
    [SerializeField] private GameplayConfig _config;

    private void Awake() => FindObjectsOfType<ConfigReceiver>().ToList().ForEach(receiver => receiver.Receive(_config));
}