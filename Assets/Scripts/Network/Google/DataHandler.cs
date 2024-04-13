using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace Network.Google
{
    public class DataHandler : MonoBehaviour
    {
        private string scriptUrl =
            "https://script.google.com/macros/s/AKfycbwNC5SXtHua54cSWnVm5ls1qi7FveBOUd8cmxlBGnYd3L9uighd9I3OKSAIbjh89NWxlg/exec";

        [SerializeField] private Users _data = new();
        public UnityEvent<Users> UpdateEvent;
        private Coroutine _requestData;

        void Start()
        {
            _requestData = StartCoroutine(GetRequest());
        }

        IEnumerator GetRequest()
        {
            for (;;)
            {
                using (WWW www = new WWW(scriptUrl))
                {
                    yield return www;

                    if (!string.IsNullOrEmpty(www.error))
                    {
                        Debug.Log("Error: " + www.error);
                    }
                    else
                    {
                        string json = www.text;
                        Debug.Log(JsonConvert.SerializeObject(transform.position, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
                        Debug.Log(JsonConvert.SerializeObject(transform.rotation, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));
                        Debug.Log(json);
                        List<UserJSON> usersJson = JsonConvert.DeserializeObject<List<UserJSON>>(json);
                        List<User> users = new List<User>();
                        foreach (var userJson in usersJson)
                        {
                            var instance = new User();
                            instance.position = JsonConvert.DeserializeObject<Vector3>(userJson.position);
                            instance.quaternion = JsonConvert.DeserializeObject<Quaternion>(userJson.quaternion);
                            instance.name = userJson.name;
                            users.Add(instance);
                        }

                        _data.users = users;
                    }
                }

                UpdateEvent?.Invoke(_data);
                yield return new WaitForSeconds(0.0000001f);
            }
        }

        private void OnDestroy()
        {
            if (_requestData != null) StopCoroutine(_requestData);
        }
    }
}