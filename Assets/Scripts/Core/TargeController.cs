using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DefenseWar.Core
{
    public class TargeController : MonoBehaviour
    {
        [SerializeField] private Transform enemyTransform;
        [SerializeField] private GameObject arrow;

        public float attackSpeed;
        private float timeBtwAttack;
        // Start is called before the first frame update
        void Start()
        {
            timeBtwAttack = attackSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.childCount != 0 && enemyTransform.childCount != 0)
            {
                if (timeBtwAttack <= 0)
                {
                    var arrowObject = Instantiate(arrow, transform);
                    var dir = enemyTransform.GetChild(0).position - arrowObject.transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180 + 37;
                    arrowObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    arrowObject.transform.DOMove(enemyTransform.GetChild(0).position, 0.9f);

                    timeBtwAttack = attackSpeed;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }      
    }
}
