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
    public class GameDataService : SingletonService<GameDataService>
    {
        public List<CharacterModel> CharactersUsed = new List<CharacterModel>();
        public List<EnemyModel> Enemies = new List<EnemyModel>();

        public int totalStar;

        public CharacterModel SelectRandomCharacter()
        {
            // Calculate the summa of all portions.
            int poolSize = 0;
            for (int i = 0; i < CharactersUsed.Count; i++)
            {
                poolSize += CharactersUsed[i].Chance;
            }
            // Get a random integer from 0 to PoolSize.
            int randomNumber = UnityEngine.Random.Range(0, poolSize) + 1;
            // Detect the item, which corresponds to current random number.
            int accumulatedProbability = 0;
            for (int i = 0; i < CharactersUsed.Count; i++)
            {
                accumulatedProbability += CharactersUsed[i].Chance;
                if (randomNumber <= accumulatedProbability)
                {
                    return CharactersUsed[i];
                }
            }
            return null;    // this code will never come while you use this programm right :)
        }
    }
}
