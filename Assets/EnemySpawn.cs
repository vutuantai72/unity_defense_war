using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private float timeMinion;
    private float timeMinionSpeed;
    [SerializeField] private float baseSpawnInSecond  = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IsTimeToSpawnMinion()
    {
        timeMinion += Time.deltaTime;
        float second = Mathf.FloorToInt(timeMinion % 60);
        if (second == 10)
        {
            baseSpawnInSecond = baseSpawnInSecond * (1 + 0.05f);
        }
        var timeToSpawn = 1 / baseSpawnInSecond;
        timeMinion = 0;
    }

    private bool IsTimeToSpawnMinionSpeed()
    {
        timeMinionSpeed += Time.deltaTime;
        float second = Mathf.FloorToInt(timeMinionSpeed % 60);
        if (second == 15)
        {
            timeMinionSpeed = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
