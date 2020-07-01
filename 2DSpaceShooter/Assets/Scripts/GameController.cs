using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;
    public int enemyCount;
    public Vector3 spawnValue;
    public float waitSpawn;
    public float waitStart;
    public float waitWave;

    // Score table
    public Text scoreText;
    public Text restartText;
    public Text gameoverText;
    private int score;

    private bool gameOver;
    private bool restart;
    
    // Set variables to default and initiate spawn waves
    private void Start()
    {

        gameOver = false;
        restart = false;
        score = 0;

        gameoverText.text = "";
        restartText.text = "";

        UpdateScore();

        StartCoroutine(SpawnEnemyWave());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
     
            if (gameOver)
            {
                restartText.text = "Press 'R' for restart";
                restart = true;
            }

        }
    }

    private IEnumerator SpawnEnemyWave()
    {
        while (true)  // (Exits internally)
        {
            // Spawn required enemies
            for (int index = 0; index < enemyCount; ++index)
            {
                // Select enemy by random
                GameObject enemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];

                // Force the enemy spawn base on waitStart
                yield return new WaitForSeconds(waitStart);


                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(1, 19), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemy, spawnPos, spawnRotation);

                // Force the enemy spawn base on waitSpawn
                yield return new WaitForSeconds(waitSpawn);
            }

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }

        yield return new WaitForSeconds(waitWave);

    }

    public void IncrementScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameoverText.text = "You lose!";
        gameOver = true;
    }
}
