using DefenseWar.Models;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefenseWar.Core
{
    public class PlayfabManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Login();
        }

        void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
        }

        public void SaveCharacters(List<CharacterModel> characters)
        {
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { "Character", JsonConvert.SerializeObject(characters) }
                }
            };
            PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
        }

        private void OnDataSend(UpdateUserDataResult result)
        {
            Debug.Log("Successful user data send");
        }

        private void OnError(PlayFabError error)
        {
            Debug.Log("fail");
            Debug.Log(error.GenerateErrorReport());
        }

        private void OnSuccess(LoginResult obj)
        {
            Debug.Log("Successful login/account create");

            var childCharacters = Resources.LoadAll<GameObject>("Prefabs/Characters").ToList();

            List<CharacterModel> characterModels = new List<CharacterModel>();

            foreach (var item in childCharacters)
            {
                var character = item.GetComponent<Character>();

                characterModels.Add(new CharacterModel(Guid.Parse(character.Id), character.Name, character.Type, character.Star));
            }
            SaveCharacters(characterModels);
        }
    }
}
