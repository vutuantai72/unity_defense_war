using Assets.Scripts.Services;
using DefenseWar.Models;
using DefenseWar.Utils.Dragging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.VisualScripting.Member;

namespace DefenseWar.Core
{
    public class DragItem : MonoBehaviour
    {

        // PRIVATE STATE
        Vector3 startPosition;
        Transform originalParent;
        Collider2D boxCollider2D;
        SpriteRenderer spriteRenderer;

        // CACHED REFERENCES
        Transform parentTransform;

        private Vector2 mousePosition;

        private float offsetX, offsetY;

        public static bool mouseButtonReleased;

        private CharacterModel ownerCharacter;

        CharacterService characterService = CharacterService.Instance;

        private void Awake()
        {
            parentTransform = GetComponentInParent<SpawnSlot>().transform;
            boxCollider2D = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            ownerCharacter = GetComponent<CharacterData>().ReturnValue();
        }

        private void OnMouseDown()
        {
            mouseButtonReleased = false;
            offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            startPosition = transform.position;
            originalParent = transform.parent;
            boxCollider2D.enabled = false;
            spriteRenderer.sortingOrder = 2;

            transform.SetParent(parentTransform, true);
        }

        private void OnMouseDrag()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
        }

        private void OnMouseUp()
        {
            mouseButtonReleased = true;
            transform.position = startPosition;
            transform.SetParent(originalParent);
            spriteRenderer.sortingOrder = 1;
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero);
            if (hit)
            {
                // what has been hit
                if (hit.collider)
                {
                    var character = hit.collider.gameObject.GetComponent<CharacterData>();
                    var data = character.ReturnValue();
                    Debug.LogError(data.Id);
                    Debug.LogError("Owner Id" + ownerCharacter.Id);

                }
            }
            boxCollider2D.enabled = true;

            //if (!string.IsNullOrEmpty(nameSpawnPoint))
            //{
            //    GameObject spawnPoint = GameObject.Find(nameSpawnPoint);

            //    if (spawnPoint.transform.childCount != 0 && !currentSpawnPoint.Equals(nameSpawnPoint))
            //    {
            //        foreach (Transform child in spawnPoint.transform)
            //        {
            //            Character subCharacter = child.GetComponent<Character>();
            //            if (character.Id == subCharacter.Id && character.Star == subCharacter.Star)
            //            {
            //                GameObject charRandom = SelectRandomCharactor(characterService.childCharacters, subCharacter.Star);
            //                var character = Instantiate(charRandom, spawnPoint.transform);
            //                character.name = charRandom.name;
            //                Destroy(child.gameObject);
            //                Destroy(gameObject);
            //                break;
            //            }
            //            else
            //            {
            //                if (positionSpawnPoint != Vector2.zero)
            //                    transform.position = positionSpawnPoint;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        transform.position = positionSpawnPoint;
            //    }
            //}
            //else
            //{
            //    transform.position = positionSpawnPoint;
            //}
        }

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

