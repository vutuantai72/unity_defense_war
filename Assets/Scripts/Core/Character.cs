using DefenseWar.Models;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class Character : MonoBehaviour
    {
        [SerializeField] public string Id = Guid.NewGuid().ToString();
        [SerializeField] public string Name;
        [SerializeField] public CharacterTypeEnum Type;
        [SerializeField] public int Star;

        public CharacterModel ReturnValue()
        {
            return new CharacterModel(Guid.Parse(Id), Name, Type, Star);
        }

        public void SetData(CharacterModel character)
        {
            Id = character.Id.ToString();
            Name = character.Name;
            Type = character.Type; 
            Star = character.Star;
        }
    }
}
