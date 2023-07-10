using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawn;
    public float repeatCount;

    public EnemiesSpawnGroup(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        this.enemyData = enemyToSpawn;
        this.count = count;
        this.isBoss = isBoss;
    }

    public void SetRepeatSpawn(float timeBetweenSpawns, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }
}

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] Vector2 spawnArea;
    GameObject player;

    List<Enemy> bossEnemiesList;
    int totalBossHealth;
    int currentBossHealth;

    [SerializeField] Slider bossHealthBar;

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    int spawnPerFrame = 2;

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHpBar>(true).GetComponent<Slider>();
        stageProgress = FindObjectOfType<StageProgress>();
    }

    private void Update()
    {
        ProcessSpawn();
        ProcessRepeatedSpawnGroups();
        UpdateBossHealth();
    }

    private void ProcessRepeatedSpawnGroups()
    {
        if(repeatedSpawnGroupList == null) { return; }

        for(int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);

                repeatedSpawnGroupList[i].repeatCount -= 1;

                if (repeatedSpawnGroupList[i].repeatCount <= 0) 
                { 
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if(enemiesSpawnGroupList == null) { return; }

        for(int i = 0; i < spawnPerFrame; i++)
        {
            if (enemiesSpawnGroupList.Count > 0)
            {
                if (enemiesSpawnGroupList[0].count <= 0) { return; }

                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
                enemiesSpawnGroupList[0].count -= 1;

                if (enemiesSpawnGroupList[0].count <= 0)
                {
                    enemiesSpawnGroupList.RemoveAt(0);
                }
            }
        }

    }

    private void UpdateBossHealth()
    {
        if(bossEnemiesList == null) { return; }
        if(bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for(int i = 0; i < bossEnemiesList.Count; i++)
        {
            if (bossEnemiesList[i] == null) { continue; }

            currentBossHealth += bossEnemiesList[i].stats.hp;
        }

        bossHealthBar.value = currentBossHealth;

        if(currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    public void AddGroupToSpawn(EnemyData enemyToSpawn,int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if(enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup> (); }

        enemiesSpawnGroupList.Add(newGroupSpawn);
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)
    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);

        position += player.transform.position;

        //spawning main object
        GameObject newEnemy = Instantiate(enemyToSpawn.animatedPrefab);
        newEnemy.transform.position = position;

        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);

        newEnemy.transform.parent = transform;

        if(isBoss == true)
        {
            SpawnBossEnemy(newEnemyComponent);
        }
    }

    private void SpawnBossEnemy(Enemy newBoss)
    {
        if(bossEnemiesList == null) { bossEnemiesList = new List<Enemy>();}

        bossEnemiesList.Add(newBoss);
        totalBossHealth += newBoss.stats.hp;

        bossHealthBar.gameObject.SetActive(true);
        bossHealthBar.maxValue = totalBossHealth;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? - 1f : 1f;

        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        position.z = 0f;

        return position;
    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);

        if(repeatedSpawnGroupList == null)
        {
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }
}
