using Assets.Scripts.Services;
using DefenseWar.Models;
using DefenseWar.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class CharacterService : SingletonService<CharacterService>
    {
        public List<CharacterModel> Characters = new List<CharacterModel> 
        { 
            new CharacterModel("D1", "D1", EggTypeEnum.CNY, 20), 
            new CharacterModel("D3", "D3", EggTypeEnum.Magic, 20),
            new CharacterModel("D4", "D4", EggTypeEnum.Magic, 20),
            new CharacterModel("D6", "D6", EggTypeEnum.Marine, 20)
        };

        //private GameObject SelectRandomCharactor(List<GameObject> items, int star)
        //{
        //    // Calculate the summa of all portions.
        //    int poolSize = 0;
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        var item = items[i].GetComponent<Character>();
        //        poolSize += item.Chance;
        //    }
        //    // Get a random integer from 0 to PoolSize.
        //    int randomNumber = Random.Range(0, poolSize) + 1;
        //    // Detect the item, which corresponds to current random number.
        //    int accumulatedProbability = 0;
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        var item = items[i].GetComponent<Character>();
        //        accumulatedProbability += item.Chance;
        //        if (randomNumber <= accumulatedProbability)
        //        {
        //            item.Star = star + 1;
        //            return items[i];
        //        }
        //    }
        //    return null;    // this code will never come while you use this programm right :)
        //}
    }
}
