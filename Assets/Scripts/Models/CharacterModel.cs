using Assets.Scripts.Services;
using DefenseWar.Models.Enum;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace DefenseWar.Models
{
    [Serializable]
    public class CharacterModel
    {
        public string Id;
        public string Name;
        public EggTypeEnum EggType;
        public RankTypeEnum RankType;
        public int Chance = 20;
        public float Damage;
        public float DamagePerSec;
        public SkeletonDataAsset skeletonDataAsset;
        public AnimatorController animator;
        public Transform targetEnemy;
        public GameObject Bullet;

        public CharacterModel(
            string id, 
            string name, 
            EggTypeEnum eggType, 
            RankTypeEnum rankType, 
            int chance,
            float damage,
            float damagePerSec,
            GameObject bullet = null)
        {
            Id = id;
            Name = name;
            EggType = eggType;
            RankType = rankType;
            Chance = chance;
            Damage = damage;
            DamagePerSec = damagePerSec;
            Bullet = bullet;
        }
    }
}
