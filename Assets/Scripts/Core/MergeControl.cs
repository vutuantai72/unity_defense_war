using Assets.Scripts.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefenseWar.Core
{
    public class MergeControl : MonoBehaviour
    {
        private Vector2 mousePosition;

        private float offsetX, offsetY;

        private static Vector2 positionSpawnPoint;

        public static bool mouseButtonReleased;

        public string nameSpawnPoint;

        public string currentSpawnPoint;

        private Character character;

        CharacterService characterService = CharacterService.Instance;

        private void Start()
        {
            character = gameObject.GetComponent<Character>();
        }

        private void OnMouseDown()
        {
            mouseButtonReleased = false;
            offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            positionSpawnPoint = transform.position;
            currentSpawnPoint = nameSpawnPoint;
        }

        private void OnMouseDrag()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
        }

        private void OnMouseUp()
        {
            mouseButtonReleased = true;

            if (!string.IsNullOrEmpty(nameSpawnPoint))
            {
                GameObject spawnPoint = GameObject.Find(nameSpawnPoint);

                if (spawnPoint.transform.childCount != 0 && !currentSpawnPoint.Equals(nameSpawnPoint))
                {
                    foreach (Transform child in spawnPoint.transform)
                    {
                        Character subCharacter = child.GetComponent<Character>();
                        if (character.Id == subCharacter.Id && character.Star == subCharacter.Star)
                        {
                            GameObject charRandom = SelectRandomCharactor(characterService.childCharacters, subCharacter.Star);
                            var character = Instantiate(charRandom, spawnPoint.transform);
                            character.name = charRandom.name;
                            Destroy(child.gameObject);
                            Destroy(gameObject);
                            break;
                        }
                        else
                        {
                            if (positionSpawnPoint != Vector2.zero)
                                transform.position = positionSpawnPoint;
                        }
                    }
                }
                else
                {
                    transform.position = positionSpawnPoint;
                }
            }
            else
            {
                transform.position = positionSpawnPoint;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //Debug.LogError($"TriggerStay: {collision.transform.name}");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            nameSpawnPoint = string.Empty;
            if (collision.tag == "SpawnPoint" && collision.transform.name != "NotSpawnPoints")
            {
                nameSpawnPoint = collision.transform.name;
            }
        }

        private GameObject SelectRandomCharactor(List<GameObject> items, int star)
        {
            // Calculate the summa of all portions.
            int poolSize = 0;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i].GetComponent<Character>();
                poolSize += item.Chance;
            }
            // Get a random integer from 0 to PoolSize.
            int randomNumber = Random.Range(0, poolSize) + 1;
            // Detect the item, which corresponds to current random number.
            int accumulatedProbability = 0;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i].GetComponent<Character>();
                accumulatedProbability += item.Chance;
                if (randomNumber <= accumulatedProbability)
                {
                    item.Star = star + 1;
                    return items[i];
                }
            }
            return null;    // this code will never come while you use this programm right :)
        }
    }
}

