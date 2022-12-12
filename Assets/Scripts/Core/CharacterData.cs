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

        [SerializeField] public Transform enemyTransform;

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
            var animator = transform.GetComponentInChildren<Animator>();

            if (animator == null || enemyTransform.childCount == 0) return;

            if (timeBtwAttack <= 0)
            {
                animator.SetBool("IsAttack", true);
                timeBtwAttack = attackSpeed;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }

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
