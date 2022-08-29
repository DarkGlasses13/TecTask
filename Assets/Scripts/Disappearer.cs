using System;
using UnityEngine;

public class Disappearer : MonoBehaviour
{
    public event Action OnDisappear;

    public const float DisappearDistance = 25;

    private Character _character;

    private void Awake() => _character = FindObjectOfType<Character>();

    private void Update()
    {
        if (transform.position.x < _character.transform.position.x - DisappearDistance)
        {
            OnDisappear?.Invoke();
            gameObject.SetActive(false);
        }
    }
}