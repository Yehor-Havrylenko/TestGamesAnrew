using System;
using UnityEngine;

namespace Network.Google
{
    [Serializable]
    public class User
    {
        public string name;
        public Vector3 position;

        public Quaternion quaternion;
        //TODO: сделать сравнение по имени
    }
}