using System;
using System.Collections.Generic;
using Network.Google;
using Network.Player;
using UnityEditor;
using UnityEngine;

namespace Network.Pools
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private Controller prefab;
        private Dictionary<User, Controller> _instances;
        private Dictionary<User, Controller> _oldInstances;
        private Dictionary<User, Controller> _actualInstances;
        private Users _actualData;
        private Users _oldData;

        private void Awake()
        {
            _instances = new Dictionary<User, Controller>();
            _oldInstances = new Dictionary<User, Controller>();
            _actualInstances = new Dictionary<User, Controller>();
        }

        public void UpdateUsers(Users value)
        {
            if (_actualData != null) _oldData = new(_actualData);
            _actualData = value;
            UpdateInstances();
        }

        private void UpdateInstances()
        {
            _oldInstances = new(_actualInstances);
            _actualInstances.Clear();
            var notFind = new List<User>();
            foreach (var user in _actualData.users)
            {
                if (_oldInstances.TryGetValue(user, out Controller value))
                {
                    value.SetPosition(user.position);
                    value.SetRotation(user.quaternion);
                    _actualInstances.Add(user, value);
                }
                else
                {
                    notFind.Add(user);
                }
            }

            foreach (var user in _oldInstances.Keys)
            {
                if (_instances.TryGetValue(user, out Controller value))
                {
                    value.OnDestroy();
                    _instances.Remove(user);
                }
            }

            foreach (var instance in CreateInstances(notFind))
            {
                _actualInstances.Add(instance.Key, instance.Value);
            }
            _oldInstances.Clear();
        }

        private Dictionary<User, Controller> CreateInstances(List<User> value)
        {
            var result = new Dictionary<User, Controller>();
            foreach (var user in value)
            {
                var instance = Instantiate(prefab);
                instance.SetPosition(user.position);
                instance.SetRotation(user.quaternion);
                result.Add(user, instance);
            }

            return result;
        }
    }
}