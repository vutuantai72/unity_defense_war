using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(AttackEnemy(collision));
    }

    private IEnumerator AttackEnemy(Collider2D collision)
    {
        yield return null;
        Destroy(collision.gameObject);
    }
}
