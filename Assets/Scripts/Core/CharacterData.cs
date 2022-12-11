using Assets.Scripts.Models.Enum;
using DefenseWar.Models;
using DefenseWar.Models.Enum;
using DG.Tweening;
using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace DefenseWar.Core
{
    public class CharacterData : MonoBehaviour
    {
        public string Id;
        private string Name;
        private EggTypeEnum EggType;
        private RankTypeEnum RankType;
        private int Star = 0;
        private int Chance = 20;
        private float Damage;
        private float DamagePerSec;

        [SerializeField] private GameObject starPrefab;
        [SerializeField] private Transform starTransform;
        [SerializeField] private GameObject characterObject;

        [SerializeField] private Transform enemyTransform;
        [SerializeField] private GameObject arrow;

        public float attackSpeed;
        private float timeBtwAttack;

        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
            timeBtwAttack = attackSpeed;
        }


        // Update is called once per frame
        void Update()
        {
            var skeletonAnimation = transform.GetComponentInChildren<SkeletonAnimation>();

            if (skeletonAnimation == null || enemyTransform.childCount == 0) return;

            if (timeBtwAttack <= 0)
            {
                Spine.TrackEntry trackEntry = skeletonAnimation.AnimationState.SetAnimation((int)AnimationStateEnum.Attack, "attack_char", false);
                var animTrack = skeletonAnimation.state.AddAnimation((int)AnimationStateEnum.Attack, "idle", false, 0f);

                animTrack.Complete += delegate (TrackEntry trackEntry)
                {
                    skeletonAnimation.AnimationState.SetAnimation((int)AnimationStateEnum.Idle, "idle", true);
                };
                //trackEntry.AnimationTime = 1.5f;
                //skeletonAnimation.AnimationState.AddAnimation(0, "idle", true, 0.8f);

                StartCoroutine(OnAttackAnimComplete(trackEntry));
                timeBtwAttack = attackSpeed + trackEntry.AnimationEnd;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }

        }

        private IEnumerator OnAttackAnimComplete(TrackEntry trackEntry)
        {
            yield return new WaitForSeconds(trackEntry.AnimationEnd/2.5f);
            var arrowObject = Instantiate(arrow, transform);
            var dir = enemyTransform.GetChild(0).position - arrowObject.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180 + 37;
            arrowObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            var target = enemyTransform.GetChild(0).position;
            arrowObject.transform.DOMove(target, 1f).OnComplete(() =>
            {
                Destroy(arrowObject);
            });
        }

        public (CharacterModel, int) ReturnValue()
        {
            return (new CharacterModel(Id, Name, EggType, RankType, 20, Damage, DamagePerSec), Star);
        }

        public void SetData(CharacterModel character, int star)
        {
            Id = character.Id;
            Name = character.Name;
            EggType = character.EggType;
            RankType = character.RankType;
            Star = star;
            Chance = character.Chance;
            Damage = character.Damage;
            DamagePerSec = character.DamagePerSec;
            foreach (Transform item in starTransform)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < star; i++)
            {
                Instantiate(starPrefab, starTransform);
            }
        }

        public void ResetData()
        {
            Id = string.Empty;
            Name = string.Empty;
            EggType = EggTypeEnum.None;
            RankType = RankTypeEnum.None;
            Star = 0;
            Chance = 0;
            Damage = 0;
            DamagePerSec = 0;
            foreach (Transform item in starTransform)
            {
                Destroy(item.gameObject);
            }
        }
    }
}
