using Assets.Scripts.Services;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public Sprite Avatar;

        public CharacterModel(string id, string name, EggTypeEnum eggType, int chance)
        {
            Id = id;
            Name = name;
            EggType = eggType;
            Chance = chance;
        }
    }
}
