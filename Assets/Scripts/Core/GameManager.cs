using Assets.Scripts.Models.Enum;
using Assets.Scripts.Services;
using DefenseWar.Models;
using Spine;
using Spine.Unity;
using System.Collections;
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


        //Spine.AnimationState animationState;
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

            InitAnimationCharacter(characterGameObject, characterModel);

            gameDataService.totalStar += 1;
            eventManage.onUpdateTotalStar.Invoke(gameDataService.totalStar);
            #endregion
        }

        private void InitAnimationCharacter(GameObject characterGameObject, CharacterModel characterModel)
        {
            characterGameObject.name = characterModel.Id;
            var characterData = characterGameObject.GetComponentInParent<CharacterData>();
            characterData.SetData(characterModel, 1);

            var skeletonAniamtionComponent = characterGameObject.GetComponent<SkeletonAnimation>();
            skeletonAniamtionComponent.skeletonDataAsset = characterModel.skeletonDataAsset;
            characterGameObject.GetComponent<MeshRenderer>().sortingLayerName = "Character";
            skeletonAniamtionComponent.AnimationState.SetAnimation((int)AnimationStateEnum.Appear, "appear_demo", false);
            var appearAnimTrack = skeletonAniamtionComponent.state.AddAnimation((int)AnimationStateEnum.Appear, "idle", false, 0f);

            appearAnimTrack.Complete += delegate (TrackEntry trackEntry)
            {
                skeletonAniamtionComponent.AnimationState.SetAnimation((int)AnimationStateEnum.Idle, "idle", true);
            };
        }
    }
}