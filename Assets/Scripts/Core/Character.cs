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
        [SerializeField] public EggTypeEnum EggType;
        [SerializeField] public RankTypeEnum RankType;
        [SerializeField] public int Star = 1;
        [SerializeField] public GameObject StarPrefab;
        [SerializeField] public GameObject StarTransform;
        [SerializeField] public int Chance = 20;

        private void Start()
        {
            foreach (Transform item in StarTransform.transform)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < Star; i++)
            {
                Instantiate(StarPrefab, StarTransform.transform);
            }
        }

        public CharacterModel ReturnValue()
        {
            return new CharacterModel(Guid.Parse(Id), Name, EggType, Star);
        }

        public void SetData(CharacterModel character)
        {
            Id = character.Id.ToString();
            Name = character.Name;
            EggType = character.EggType; 
            Star = character.Star;
        }
    }
}
