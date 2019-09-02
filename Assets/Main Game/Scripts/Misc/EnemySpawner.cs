using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] EnemyType;
    public GameObject BossObj;
    public bool InstantAggro;

    public int WaveDelay = 5;
    public int NumberOfWaves = 3;
    public int EnemiesPerWave = 3;

    private Enemy boss;
    private int spawnedEnemies;
    private int currentWave;
    private float nextWaveSpawn;

    void Start()
    {
        if (BossObj != null)
            boss = BossObj.GetComponent<Enemy>();
    }

    void Update()
    {
        if (boss != null && boss.IsDying || currentWave >= NumberOfWaves)
        {
            return;
        }

        if (Time.time > nextWaveSpawn)
        {
            nextWaveSpawn = Time.time + WaveDelay;
            currentWave++;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(Random.Range(2, 5));

        var randomSpawn = Random.Range(0, SpawnPoints.Length);

        for (int i = 0; i < EnemiesPerWave; i++)
        {
            var randomEnemy = Random.Range(0, EnemyType.Length);
            var enemyObj = EnemyType[randomEnemy];
            var enemy = enemyObj.GetComponent<Enemy>();
            enemy.AlwaysChasePlayer = InstantAggro;
            Instantiate(enemy, SpawnPoints[randomSpawn].position, Quaternion.identity);
            yield return wait;
        }
    }
}
