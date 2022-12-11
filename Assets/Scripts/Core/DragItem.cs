using Assets.Scripts.Services;
using DefenseWar.Models;
using Spine;
using Spine.Unity;
using System;
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
        MeshRenderer meshSkeleton;

        // CACHED REFERENCES
        Transform parentTransform;

        private Vector2 mousePosition;

        private float offsetX, offsetY;

        public static bool mouseButtonReleased;

        GameObject tempGameObject;

        GameDataService gameDataService = GameDataService.Instance;

        private void Awake()
        {
            parentTransform = GetComponentInParent<SpawnSlots>().transform;
            boxCollider2D = GetComponent<Collider2D>();
            
            //meshSkeleton = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
        }


        private void OnMouseDown()
        {
            mouseButtonReleased = false;
            tempGameObject = Instantiate(gameObject, transform.parent);
            offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - tempGameObject.transform.position.x;
            offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - tempGameObject.transform.position.y;
            startPosition = tempGameObject.transform.position;
            originalParent = tempGameObject.transform.parent;
            boxCollider2D.enabled = false;

            tempGameObject.transform.SetParent(parentTransform, true);
        }

        private void OnMouseDrag()
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempGameObject.transform.position = new Vector2(mousePosition.x - offsetX, mousePosition.y - offsetY);
        }

        private void OnMouseUp()
        {
            mouseButtonReleased = true;
            Destroy(tempGameObject);
            var ownerCharacterData = GetComponentInParent<CharacterData>();
            var (data, ownerStar) = ownerCharacterData.ReturnValue();
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero);
            if (hit)
            {
                // what has been hit
                if (hit.collider)
                {
                    var character = hit.collider.gameObject.GetComponentInParent<CharacterData>();
                    if (character != null)
                    {
                        var (characterData, star) = character.ReturnValue();
                        if (characterData.Id == data.Id && star == ownerStar)
                        {
                            var newCharacter = gameDataService.SelectRandomCharacter();
                            if (newCharacter != null)
                            {
                                gameDataService.totalStar += 1 - star;
                                var newSprite = character.GetComponentInChildren<SkeletonAnimation>();
                                newSprite.skeletonDataAsset = newCharacter.skeletonDataAsset;
                                newSprite.name = newCharacter.Id;
                                ownerCharacterData.ResetData();
                                character.SetData(newCharacter, star + 1);
                                Destroy(gameObject);
                            }
                        }
                    }
                    
                }
            }
            boxCollider2D.enabled = true;
        }
    }
}

