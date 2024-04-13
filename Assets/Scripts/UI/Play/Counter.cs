using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Play
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> texts;
        [SerializeField] private float countEnemyDie;

        public void AddDiedEnemy()
        {
            countEnemyDie++;
            foreach (var text in texts)
            {
                text.text = $"Killed Enemies Count {countEnemyDie.ToString()}";
            }
        }

        public void RemoveDiedEnemy()
        {
            countEnemyDie--;
            foreach (var text in texts)
            {
                text.text = $"Killed Enemies Count {countEnemyDie.ToString()}";
            }
        }

        public void Restore()
        {
            countEnemyDie = 0;
            foreach (var text in texts)
            {
                text.text = $"Killed Enemies Count {countEnemyDie.ToString()}";
            }
        }
    }
}