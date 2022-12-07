using DefenseWar.Models;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class EnemyData : MonoBehaviour
    {
        private string Id;
        private string Name;
        private float Speed;
        public EnemyTypeEnum EnemyType;

        private void Start()
        {
        }

        public EnemyModel ReturnValue()
        {
            return new EnemyModel(Id, Name, Speed, EnemyType);
        }

        public void SetData(EnemyModel enemy)
        {
            Id = enemy.Id;
            Name = enemy.Name;
            EnemyType = enemy.EnemyType;
            Speed = enemy.Speed;
            GetComponent<SpriteRenderer>().sprite = enemy.Avatar;
        }
    }
}
