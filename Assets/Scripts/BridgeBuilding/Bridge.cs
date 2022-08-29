using UnityEngine;

namespace BridgeBuilding
{
    public class Bridge : MonoBehaviour
    {
        public float Length => transform.localScale.y;
    }
}