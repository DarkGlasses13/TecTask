using System.Linq;
using UnityEngine;

public class ConfigProvider : MonoBehaviour
{
    [SerializeField] private GameplayConfig _config;

    private void Awake() => FindObjectsOfType<ConfigReceiver>().ToList().ForEach(receiver => receiver.Receive(_config));
}