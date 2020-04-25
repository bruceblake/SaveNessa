using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPos;
    public GameObject enemy;
    public GameManager gameManager;
    public enum SpawnState { SPAWNING,WAITING,COUNTING}

    [System.Serializable]
    public class Round
    {
        public int count;
        public float rate;
        public Enemy enemy;

    }

    public Round[] rounds;
    private int nextRound = 0;

    

    public float timeBtwRounds = 8f;
    public float roundCountdown = 8f;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;


    private void Start()
    {
        roundCountdown = timeBtwRounds;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                RoundCompleted();
                gameManager.listOfSounds[4].gameObject.SetActive(true);
            }
            else
            {
                return;
            }
        }
        if (roundCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnRound(rounds[nextRound]));
            }
        }
        else
        {
            roundCountdown -= Time.deltaTime;
        }
        
        bool EnemyIsAlive()
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0)
            {
                searchCountdown = 1f;

                if (GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    return false;
                }
                              
            }
            return true;
        }

    }

    IEnumerator SpawnRound(Round _round)
    {
        gameManager.listOfSounds[4].gameObject.SetActive(false);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _round.count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1/_round.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy()
    {
        int rand = Random.Range(0, spawnPos.Length);
        Instantiate(enemy, spawnPos[rand].transform.position, Quaternion.identity);
    }
    void RoundCompleted()
    {
        state = SpawnState.COUNTING;
        roundCountdown = timeBtwRounds;
        gameManager.rounds++;
        if (nextRound + 1 > rounds.Length - 1)
        {
            nextRound = rounds.Length - 1;
            Multiply(rounds[nextRound]);

        }
        else
        {
            nextRound++;
        }
    }
    void Multiply(Round _count)
    {
        _count.count = _count.count + 1;
        _count.enemy.enemyHealth += 50;
    }
}
