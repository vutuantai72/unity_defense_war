using Assets.Scripts.Services;
using DefenseWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TextCore.Text;

namespace DefenseWar.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnSlots;

        [SerializeField] private GameObject characterObject;

        [SerializeField] private CharacterModel[] characters;

        [SerializeField] private EnemyModel[] enemies;

        [SerializeField] private AssetLabelReference characterLabelReference;

        GameDataService gameDataService = GameDataService.Instance;
        EventManage eventManage = EventManage.Instance;

        private void Start()
        {
            Application.SetStackTraceLogType(LogType.Error, StackTraceLogType.Full);

            //Addressables.LoadAssetsAsync<GameObject>(characterLabelReference, (character) => 
            //{
            //    characterService.childCharacters.Add(character);
            //});

            gameDataService.CharactersUsed.AddRange(characters);
            gameDataService.Enemies.AddRange(enemies);
            //childCharacters = Resources.LoadAll<GameObject>("Prefabs/Characters").ToList();
        }

        public void SpawnCharacter()
        {
            List<Transform> slotEmpty = new List<Transform>();

            foreach (Transform spawnSlot in spawnSlots)
            {
                if (spawnSlot.GetComponentInChildren<CharacterData>().transform.childCount == 1)
                {
                    slotEmpty.Add(spawnSlot);
                }
            }

            if (slotEmpty.Count < 1)
                return;

            #region Init character
            int random = UnityEngine.Random.Range(0, slotEmpty.Count);

            int randomCharacter = UnityEngine.Random.Range(0, characters.Count());

            var characterModel = characters[randomCharacter];

            var characterGameObject = Instantiate(characterObject, slotEmpty[random].GetChild(0));

            characterGameObject.name = characterModel.Id;

            characterGameObject.GetComponent<SpriteRenderer>().sprite = characterModel.Avatar;

            var characterData = characterGameObject.GetComponentInParent<CharacterData>();

            characterData.SetData(characterModel, 1);


            gameDataService.totalStar += 1;
            eventManage.onUpdateTotalStar.Invoke(gameDataService.totalStar);
            #endregion
        }
    }
}