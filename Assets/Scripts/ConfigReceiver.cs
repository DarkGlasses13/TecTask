using UnityEngine;

public class ConfigReceiver : MonoBehaviour
{
    protected GameplayConfig _config;

    public void Receive(GameplayConfig config) => _config = config;
}