using Assets.Scripts.Services;
using DefenseWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DefenseWar.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoints;

        [SerializeField] private AssetLabelReference characterLabelReference;

        CharacterService characterService = CharacterService.Instance;

        private void Start()
        {
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);

            Addressables.LoadAssetsAsync<GameObject>(characterLabelReference, (charater) => 
            {
                characterService.childCharacters.Add(charater);
            });


            //childCharacters = Resources.LoadAll<GameObject>("Prefabs/Characters").ToList();
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

            if (spawnEmpty.Count < 1)
                return;

            #region Init character
            int random = UnityEngine.Random.Range(0, spawnEmpty.Count);

            int randomChildCharacter = UnityEngine.Random.Range(0, characterService.childCharacters.Count);

            var childCharacter = characterService.childCharacters[randomChildCharacter];

            var characterGameObject = Instantiate(childCharacter, spawnEmpty[random]);

            characterGameObject.name = childCharacter.GetComponent<Character>().Name;
            #endregion
        }
    }
}