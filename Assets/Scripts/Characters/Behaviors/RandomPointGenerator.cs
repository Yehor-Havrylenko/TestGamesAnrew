using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Behaviors
{
    public class RandomPointGenerator : MonoBehaviour
    {
        [SerializeField] private Transform maxPoint;
        [SerializeField] private Transform minPoint;
        [SerializeField] private int pointCount;

        public List<Vector3> Init()
        {
            var result = new List<Vector3>();
            for (int i = 0; i < pointCount; i++)
            {
                var xPosition = Random.Range(maxPoint.position.x, minPoint.position.x);
                var zPosition = Random.Range(maxPoint.position.z, minPoint.position.z);
                result.Add(new Vector3(xPosition, 1, zPosition));
            }

            return result;
        }
    }
}