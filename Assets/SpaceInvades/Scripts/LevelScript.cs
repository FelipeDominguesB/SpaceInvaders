using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{


    //Enemy Info
    public bool spawnEnemies = true;
    public int enemyQuantity= 5;
    public GameObject enemyObject;
    public float shootingRate = 0.5F;


    //UI Info
    public GameObject gameOverScreen;
    public GameObject nextLevelScreen;


    //Level Control
    private float nextUpdate = 1;
    private float startSpawnPoint = -7;
    private bool finishedLevel = false;
    private bool finishedSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        if(spawnEnemies)
        {
            StartCoroutine(SpawnEnemies());        
            nextUpdate = shootingRate;
        }
    }

    IEnumerator SpawnEnemies()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5F);

        for (int i = 0; i < enemyQuantity; i++)
        {
            EnemyScript enemyScript = Instantiate<GameObject>(enemyObject, new Vector3((transform.position.x + 1 + startSpawnPoint), transform.position.y), transform.rotation).GetComponents<EnemyScript>()[0]; ;
            enemyScript.forward = (i%2 == 0);
            startSpawnPoint += (15.0F/enemyQuantity);
            yield return new WaitForSecondsRealtime(0.5F);
        }

        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        finishedSpawn = true;


    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextUpdate && !finishedLevel)
        {
            nextUpdate = Time.time + shootingRate;
            MakeRandomEnemyShoot();
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FinishLevel()
    {
        nextLevelScreen.SetActive(true);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void CloseGame()
    {
        Application.Quit();
    }


    private void MakeRandomEnemyShoot()
    {
        EnemyScript[] enemies = GameObject.FindObjectsOfType<EnemyScript>();

        if(enemies.Length > 0)
        {
            enemies[Random.Range(0, enemies.Length)].Shoot();

        }
        else if(!finishedLevel && finishedSpawn)
        {
            finishedLevel = true;
            FinishLevel();
        }
    }


}
