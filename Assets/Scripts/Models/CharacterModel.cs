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
        public CharacterTypeEnum Type;
        public int Star;

        public CharacterModel(Guid id, string name, CharacterTypeEnum type, int star)
        {
            Id = id;
            Name = name;
            Type = type;
            Star = star;
        }
    }
}
