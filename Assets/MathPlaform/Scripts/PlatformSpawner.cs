using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("生成設定")]
    public GameObject platformPrefab;
    public Transform platformParent;
    public Vector2 spawnRangeX = new Vector2(-5, 5);
    public float spawnY = -10f; // 生成在畫面下方
    public float initialSpawnInterval = 2f;
    
    [Header("速度控制")]
    public float baseSpeed = 1f;
    public float speedIncreasePerPhase = 0.5f;
    public float speedUpdateInterval = 30f;

    private float nextSpawnTime;
    private float currentSpeed;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        currentSpeed = baseSpeed;
        nextSpawnTime = Time.time + initialSpawnInterval;
        InvokeRepeating("IncreaseSpeed", speedUpdateInterval, speedUpdateInterval);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPlatform();
            nextSpawnTime = Time.time + initialSpawnInterval;
        }

        // 回收超出畫面的平台
        RecyclePlatforms();
    }

    void SpawnPlatform()
    {
        GameObject platform = Instantiate(platformPrefab, platformParent);
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        platform.transform.position = new Vector3(randomX, spawnY, 0);
        
        // 設定數學題目
        MathPlatform mathComp = platform.GetComponent<MathPlatform>();
        mathComp.SetQuestion(GenerateQuestion());
        
        activePlatforms.Add(platform);
    }

        string GenerateQuestion()
        {
            // 根據難度動態生成題目
            int difficulty = Mathf.FloorToInt(Time.time / speedUpdateInterval);
            int a = Random.Range(1, 3 + difficulty);
            int b = Random.Range(1, 3 + difficulty);
            return $"{a} + {b} = ?";
            
        }

    void RecyclePlatforms()
    {
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i].transform.position.y > Camera.main.ViewportToWorldPoint(Vector3.up).y + 2f)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    void IncreaseSpeed()
    {
        currentSpeed += speedIncreasePerPhase;
        foreach (var platform in activePlatforms)
        {
            platform.GetComponent<PlatformMovement>().speed = currentSpeed;
        }
    }
}