using Assets.Scripts.Services;
using DefenseWar.Core;
using DefenseWar.Models;
using DefenseWar.Models.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private float baseSpawnInSecond = 0.5f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPoint;

    private float timeMinion;
    private float timeForIncreaseBaseSecond;
    private float timeMinionSpeed;
    private float timeMiniBoss;
    GameDataService gameDataService = GameDataService.Instance;
    EventManage eventManage = EventManage.Instance;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        eventManage.onUpdateTotalStar.AddListener(UpdateBaseSpawnInSecond);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnMinion());
        StartCoroutine(SpawnMinionSpeed());
        StartCoroutine(SpawnMiniBoss());
    }

    private IEnumerator SpawnMinion()
    {
        yield return null;
        timeMinion += Time.deltaTime;
        timeForIncreaseBaseSecond += Time.deltaTime;
        double second = Math.Round(timeMinion, 1);
        var secondBase = Mathf.FloorToInt(timeForIncreaseBaseSecond % 60);
        if (secondBase == 10)
        {
            baseSpawnInSecond = baseSpawnInSecond * (1 + 0.05f);
            timeForIncreaseBaseSecond = 0;
        }
        var timeToSpawn = Math.Round(1 / baseSpawnInSecond, 1);
        if (second == timeToSpawn)
        {
            InstantiateEnemy(EnemyTypeEnum.Minion);

            timeMinion = 0;
        }
    }

    private IEnumerator SpawnMinionSpeed()
    {
        yield return null;
        timeMinionSpeed += Time.deltaTime;
        float second = Mathf.FloorToInt(timeMinionSpeed % 60);
        if (second == 15)
        {
            InstantiateEnemy(EnemyTypeEnum.MinionSpeed);

            timeMinionSpeed = 0;
        }
    }

    private IEnumerator SpawnMiniBoss()
    {
        yield return null;
        timeMiniBoss += Time.deltaTime;
        float second = Mathf.FloorToInt(timeMiniBoss % 60);
        if (second == 20)
        {
            InstantiateEnemy(EnemyTypeEnum.MiniBoss);

            timeMiniBoss = 0;
        }
    }

    private void InstantiateEnemy(EnemyTypeEnum enemyType)
    {
        var enemyModel = gameDataService.Enemies.First(x => x.EnemyType == enemyType);

        var enemyGameObject = Instantiate(enemy, spawnPoint);

        enemyGameObject.name = enemyModel.Id;

        var enemyData = enemyGameObject.GetComponent<EnemyData>();

        enemyData.SetData(enemyModel);
    }

    private void UpdateBaseSpawnInSecond(int totalStar)
    {
        baseSpawnInSecond = baseSpawnInSecond * (1 + 0.1f * totalStar);
        timeMinion = 0;
    }
}
