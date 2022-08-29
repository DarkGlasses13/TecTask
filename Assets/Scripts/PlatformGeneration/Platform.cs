using UnityEngine;

namespace PlatformGeneration
{
    public class Platform : MonoBehaviour
    {
        public const float Height = 10;

        public float Width => transform.localScale.x;
        public float AbysWidth { get; private set; }

        public void SetSize(float size) => transform.localScale = new(size, Height, 1);

        public void SetAbysWidth(float width) => AbysWidth = width;
    }
}