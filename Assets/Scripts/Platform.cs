using UnityEngine;

public class Platform : MonoBehaviour 
{
    public const float Height = 10;

    public float Size => transform.localScale.x;

    public void SetSize(float size) => transform.localScale = new(size, Height, 1);
}