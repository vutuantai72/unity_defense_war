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
        private int ownerStar;

        CharacterService characterService = CharacterService.Instance;

        private void Awake()
        {
            parentTransform = GetComponentInParent<SpawnSlots>().transform;
            boxCollider2D = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
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
            (ownerCharacter, ownerStar) = GetComponent<CharacterData>().ReturnValue();
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero);
            if (hit)
            {
                // what has been hit
                if (hit.collider)
                {
                    var character = hit.collider.gameObject.GetComponent<CharacterData>();
                    var (characterData, star) = character.ReturnValue();
                    if (characterData.Id == ownerCharacter.Id && star == ownerStar)
                    {
                        var newCharacter = characterService.SelectRandomCharacter();
                        if (newCharacter != null)
                        {
                            character.SetData(newCharacter, star + 1);
                            Destroy(gameObject);
                        }
                    }
                }
            }
            boxCollider2D.enabled = true;
        }
    }
}

