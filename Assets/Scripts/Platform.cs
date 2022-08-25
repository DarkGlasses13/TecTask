using UnityEngine;

public class Platform : MonoBehaviour 
{
    public const float Height = 10;

    public Vector3 Size => transform.localScale;

    public void SetSize(float size) => transform.localScale = new(size, Height, 1);
}