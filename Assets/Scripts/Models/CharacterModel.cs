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
        public int Star;

        public CharacterModel(Guid id, string name, EggTypeEnum eggType, int star)
        {
            Id = id;
            Name = name;
            EggType = eggType;
            Star = star;
        }
    }
}
