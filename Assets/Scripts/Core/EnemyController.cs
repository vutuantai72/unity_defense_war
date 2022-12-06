using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenseWar.Core
{
    public class EnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.LogError(collision.gameObject.name);
        }
    }
}