using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }
    public Wave[] waves;
    public Transform[] spawmPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;
    public bool finishedSpawning;
    public GameObject boss,healthBar;
    public Transform bossSpawnPoint;
    private void Start() {
        player=GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }
    private void Update() {
        if(finishedSpawning && GameObject.FindGameObjectsWithTag("Enemy").Length==0)
        {
            finishedSpawning=false;
            if(currentWaveIndex+1<waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                healthBar.SetActive(true);
                Instantiate(boss,bossSpawnPoint.position ,bossSpawnPoint.rotation);
            }
        }   
    }
    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }
    IEnumerator SpawnWave(int index)
    {
        currentWave=waves[index];
        for(int i=0;i<currentWave.count;i++)
        {
            if(player==null)
            {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[Random.Range(0,currentWave.enemies.Length)];
            Transform randomSpot=spawmPoints[Random.Range(0,spawmPoints.Length)];
            Instantiate(randomEnemy,randomSpot.position,randomSpot.rotation);

            if(i==currentWave.count-1)
            {
                finishedSpawning=true;
            }
            else
            {
                finishedSpawning=false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }
}
