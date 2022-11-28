using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Models
{
    public class CharacterModel
    {
        public Guid Id;
        public string Name;
        public EggTypeEnum EggType;
        public RankTypeEnum RankType;
        public int Star;
        public int Chance = 20;

        public CharacterModel(Guid id, string name, EggTypeEnum eggType, int star, int chance)
        {
            Id = id;
            Name = name;
            EggType = eggType;
            Star = star;
            Chance = chance;
        }
    }
}
