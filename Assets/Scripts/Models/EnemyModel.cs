using Assets.Scripts.Services;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Models
{
    [Serializable]
    public class EnemyModel
    {
        public string Id;
        public string Name;
        public float Speed = 20;
        public EnemyTypeEnum EnemyType;
        public Sprite Avatar;

        public EnemyModel(string id, string name, float speed, EnemyTypeEnum enemyType, Sprite avatar = null)
        {
            Id = id;
            Name = name;
            Speed = speed;
            EnemyType = enemyType;
            Avatar = avatar;
        }
    }
}
