using DefenseWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefenseWar.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoints;

        [SerializeField] private GameObject charatecSpawn;

        private List<GameObject> childCharacters = new List<GameObject>();

        private void Start()
        {
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);
            childCharacters = Resources.LoadAll<GameObject>("Prefabs/Characters").ToList();
        }

        public void SpawnCharacter()
        {
            List<Transform> spawnEmpty = new List<Transform>();

            foreach (Transform spawnPoint in spawnPoints)
            {
                if (spawnPoint.childCount == 0)
                {
                    spawnEmpty.Add(spawnPoint);
                }
            }

            #region Init character
            int random = UnityEngine.Random.Range(0, spawnEmpty.Count);

            int randomChildCharacter = UnityEngine.Random.Range(0, childCharacters.Count);

            var childCharacter = childCharacters[randomChildCharacter];

            var characterGameObject = Instantiate(childCharacter, spawnEmpty[random]);

            characterGameObject.name = childCharacter.GetComponent<Character>().Name;
            #endregion
        }
    }
}