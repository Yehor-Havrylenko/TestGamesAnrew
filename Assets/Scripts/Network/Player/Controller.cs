using UnityEngine;

namespace Network.Player
{
    public class Controller : MonoBehaviour
    {
        public void SetPosition(Vector3 value) => transform.position = value;
        public void SetRotation(Quaternion value) => transform.rotation = value;

        public void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}