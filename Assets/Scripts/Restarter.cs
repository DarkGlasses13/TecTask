using UnityEngine;
using UnityEngine.SceneManagement;
using Motion;

public class Restarter : MonoBehaviour
{
    private Faller _faller;

    private void Awake()
    {
        _faller = FindObjectOfType<Faller>();
        _faller.OnFalled += Activate;
        gameObject.SetActive(false);
    }

    public void Restart() => SceneManager.LoadScene(0);

    private void Activate() => gameObject.SetActive(true);
}