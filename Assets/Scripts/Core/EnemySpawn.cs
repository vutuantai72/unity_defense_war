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
    [SerializeField] private float coefficientSpawn = 0.5f;
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
        timeMinion = (float)Math.Round(1 / coefficientSpawn, 1);
        timeForIncreaseBaseSecond = 10;
        timeMinionSpeed = 15;
        timeMiniBoss = 20;
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

        if (timeForIncreaseBaseSecond <= 0)
        {
            coefficientSpawn = coefficientSpawn * (1 + 0.05f);
            timeForIncreaseBaseSecond = 10;
        }
        else
        {
            timeForIncreaseBaseSecond -= Time.deltaTime;
        }

        if (timeMinion <= 0)
        {
            InstantiateEnemy(EnemyTypeEnum.Minion);

            timeMinion = (float)Math.Round(1 / coefficientSpawn, 1);
        }
        else
        {
            timeMinion -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnMinionSpeed()
    {
        yield return null;

        if (timeMinionSpeed <= 0)
        {
            InstantiateEnemy(EnemyTypeEnum.MinionSpeed);

            timeMinionSpeed = 15;
        }
        else
        {
            timeMinionSpeed -= Time.deltaTime;
        }
    }

    private IEnumerator SpawnMiniBoss()
    {
        yield return null;

        if (timeMiniBoss <= 0)
        {
            InstantiateEnemy(EnemyTypeEnum.MiniBoss);

            timeMiniBoss = 20;
        }
        else
        {
            timeMiniBoss -= Time.deltaTime;
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
        coefficientSpawn = coefficientSpawn * (1 + 0.1f * totalStar);
        timeMinion = (float)Math.Round(1 / coefficientSpawn, 1);
    }
}
