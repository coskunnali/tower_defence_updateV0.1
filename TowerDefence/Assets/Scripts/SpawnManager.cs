using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //private Vector3 spawnPosition = new Vector3(1.7f, 0.6f, -0.2f);
    //private float startDelay = 2f;
    //private float repeatRate = 2f;
    //public GameObject enemy;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    InvokeRepeating("Enemy", startDelay, repeatRate);
    //}

    //void Enemy()
    //{
    //    Debug.Log("EnemySpawned");
    //    Instantiate(enemy, spawnPosition, enemy.transform.rotation);
    //    //if (playerControllerScript.gameOver == false)
    //    //{

    //    //}
    //}
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public TextMeshProUGUI waveCountdownText;

    public float timeBetWaves = 4f;  //Dalga aras�ndaki s�re, saniye cinsinden belirlenir ve varsay�lan  4 saniye olarak ayarlan�r.

    private float countdown = 0f; // Geri say�m s�resi, dalga aralar� aras�ndaki s�reyi hesaplamak i�in kullan�l�r.

    private int waveIndex = 0; // Dalga dizinini takip etmek i�in bir de�i�ken.


    void Update()
    {
        if (countdown <= 0 && !GameManager.gameOver)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetWaves;
        }
        countdown -= Time.deltaTime;
        if (countdown >= 0)
        {
            waveCountdownText.text = Mathf.Round(countdown).ToString();
        }
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
