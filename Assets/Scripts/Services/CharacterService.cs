using Assets.Scripts.Services;
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
        public List<GameObject> childCharacters = new List<GameObject>();
    }
}
