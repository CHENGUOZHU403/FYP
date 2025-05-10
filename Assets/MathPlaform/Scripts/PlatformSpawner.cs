using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("生成設定")]
    public GameObject platformPrefab;
    public Transform platformParent;
    public Vector2 spawnRangeX = new Vector2(-7, 7); // 扩大X轴范围
    public float verticalSpacing = 2.5f;
    [Tooltip("每排最少生成平台数")] 
    public int minPlatformsPerRow = 2;
    [Tooltip("每排最多生成平台数")]
    public int maxPlatformsPerRow = 5;

    [Header("移動控制")]
    public float baseSpeed = 1f;
    public float speedMultiplier = 1.05f; // 每阶段速度倍增比例
    public float speedUpdateInterval = 10f;

    private float currentSpeed;
    private float nextSpeedIncreaseTime;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float nextSpawnY;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        currentSpeed = baseSpeed;
        nextSpeedIncreaseTime = Time.time + speedUpdateInterval;
        SpawnInitialPlatforms();
    }

    void SpawnInitialPlatforms()
    {
        // 初始生成3排平台
        for (int i = 0; i < 3; i++)
        {
            SpawnPlatformRow();
            nextSpawnY += verticalSpacing;
        }
    }

    void Update()
    {
        MovePlatforms();
        HandleDifficulty();
        HandleSpawning();
        RecyclePlatforms();
    }

    private void MovePlatforms()
    {
        foreach (var platform in activePlatforms)
        {
            platform.transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
        }
    }

    private void HandleDifficulty()
    {
        if (Time.time >= nextSpeedIncreaseTime)
        {
            currentSpeed *= speedMultiplier; // 指数级加速
            nextSpeedIncreaseTime = Time.time + speedUpdateInterval;
            Debug.Log($"速度提升至: {currentSpeed:F1}");
        }
    }

    void HandleSpawning()
    {
        float spawnTriggerY = mainCamera.ViewportToWorldPoint(Vector3.up).y - 3f;
        if (GetHighestPlatformY() < spawnTriggerY)
        {
            SpawnPlatformRow();
        }
    }

    void SpawnPlatformRow()
    {
        int spawnCount = Random.Range(minPlatformsPerRow, maxPlatformsPerRow + 1);
        List<float> xPositions = new List<float>();
        
        float availableWidth = spawnRangeX.y - spawnRangeX.x;
        float spacing = availableWidth / (spawnCount + 1);
        float spawnY = Camera.main.ViewportToWorldPoint(Vector3.down).y - 2f;
        
        // 智能分布算法
        for (int i = 1; i <= spawnCount; i++)
        {
            float baseX = spawnRangeX.x + spacing * i;
            float variance = spacing * 0.3f;
            float x = Mathf.Clamp(
                baseX + Random.Range(-variance, variance),
                spawnRangeX.x + 1f,
                spawnRangeX.y - 1f
            );
            
            xPositions.Add(x);
        }

        foreach (var x in xPositions)
        {
            Vector3 spawnPos = new Vector3(
                x,
                mainCamera.ViewportToWorldPoint(Vector3.down).y - 2f,
                0
            );
            
            GameObject platform = Instantiate(platformPrefab, spawnPos, Quaternion.identity, platformParent);
            SetupPlatform(platform);
            activePlatforms.Add(platform);
            platform.GetComponent<PlatformMovement>().SetSpeed(currentSpeed); // 新平台继承当前速度
        }

        Debug.Log($"生成新排：{spawnCount}个平台，间隔：{spacing:F1}单位");
    }

    void SetupPlatform(GameObject platform)
    {
        var mathPlatform = platform.GetComponent<MathPlatform>();
        int difficulty = Mathf.FloorToInt(Time.time / speedUpdateInterval);
        mathPlatform.SetQuestion(GenerateQuestion(difficulty));
    }

    string GenerateQuestion(int difficulty)
    {
        int maxValue = 5 + difficulty * 2;
        int a = Random.Range(1, maxValue);
        int b = Random.Range(1, maxValue);
        return $"{a} + {b} = ?";
    }
    // string GenerateQuestion(int difficulty)
    // {
    //     int a = Random.Range(1, 3 + difficulty);
    //     int b = Random.Range(1, 3 + difficulty);
    //     return $"{a} + {b} = ?";
    // }

    float GetHighestPlatformY()
    {
        float highestY = float.MinValue;
        foreach (var platform in activePlatforms)
        {
            if (platform.transform.position.y > highestY)
                highestY = platform.transform.position.y;
        }
        return highestY;
    }

    void RecyclePlatforms()
    {
        float destroyThreshold = mainCamera.ViewportToWorldPoint(Vector3.up).y + 5f;
        
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i].transform.position.y > destroyThreshold)
            {
                Destroy(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!mainCamera) mainCamera = Camera.main;
        
        Gizmos.color = Color.cyan;
        Vector3 spawnLineStart = new Vector3(
            spawnRangeX.x,
            mainCamera.ViewportToWorldPoint(Vector3.down).y - 2f,
            0
        );
        Vector3 spawnLineEnd = new Vector3(
            spawnRangeX.y,
            mainCamera.ViewportToWorldPoint(Vector3.down).y - 2f,
            0
        );
        Gizmos.DrawLine(spawnLineStart, spawnLineEnd);
    }
}