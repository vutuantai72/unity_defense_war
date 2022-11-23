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

        public static string nameSpawnPoint;

        private void OnMouseDown()
        {
            mouseButtonReleased = false;
            offsetX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            offsetY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
            positionSpawnPoint = transform.position;
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

                if (spawnPoint.transform.childCount == 0)
                {
                    positionSpawnPoint = spawnPoint.transform.position;
                    transform.position = spawnPoint.transform.position;
                    var character = Instantiate(gameObject, spawnPoint.transform, true);
                    character.name = gameObject.name;
                    Destroy(gameObject);
                }
                else
                {
                    foreach (Transform character in spawnPoint.transform)
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            //Debug.LogError($"TriggerStay: {collision.transform.name}");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            nameSpawnPoint = collision.transform.name != "NotSpawnPoints" ? collision.transform.name : string.Empty;
        }
    }
}

