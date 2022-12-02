using DefenseWar.Models;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class CharacterData : MonoBehaviour
    {
        private string Id;
        private string Name;
        private EggTypeEnum EggType;
        private RankTypeEnum RankType;
        private int Star = 0;
        private int Chance = 20;

        [SerializeField] private GameObject StarPrefab;
        [SerializeField] private GameObject StarTransform;

        private void Start()
        {
        }

        public CharacterModel ReturnValue()
        {
            return new CharacterModel(Id, Name, EggType, 20);
        }

        public void SetData(CharacterModel character)
        {
            Id = character.Id;
            Name = character.Name;
            EggType = character.EggType;
            RankType = character.RankType;
            Star += 1;
            Chance = character.Chance;
            GetComponent<SpriteRenderer>().sprite = character.Avatar;
            foreach (Transform item in StarTransform.transform)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < Star; i++)
            {
                Instantiate(StarPrefab, StarTransform.transform);
            }
        }
    }
}
