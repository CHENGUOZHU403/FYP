using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("生成設定")]
    public GameObject platformPrefab;
    public Transform platformParent;
    public Vector2 spawnRangeX = new Vector2(-5, 5);
    public float initialSpawnY = -5f; // 初始生成Y位置（改名为initialSpawnY）
    public float verticalSpacing = 2f; // 平台垂直間距
    public int platformsPerRow = 3; // 每排生成幾個平台
    
    [Header("移動控制")]
    public float baseSpeed = 1f;
    public float speedIncreasePerPhase = 0.2f;
    public float speedUpdateInterval = 30f;

    private float currentSpeed;
    private float nextSpeedIncreaseTime;
    private List<GameObject> activePlatforms = new List<GameObject>();
    private float nextSpawnY; // 下次生成的高度基準

    void Start()
    {
        currentSpeed = baseSpeed;
        nextSpeedIncreaseTime = Time.time + speedUpdateInterval;
        nextSpawnY = initialSpawnY; // 使用initialSpawnY初始化
        SpawnPlatformRow(); // 初始生成第一排
    }

    void Update()
    {
        // 平台移動
        foreach (var platform in activePlatforms)
        {
            platform.transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
        }

        // 動態加速
        if (Time.time >= nextSpeedIncreaseTime)
        {
            currentSpeed += speedIncreasePerPhase;
            nextSpeedIncreaseTime = Time.time + speedUpdateInterval;
        }

        // 當最高平台超過一定高度時，在底部生成新的一排
        if (GetHighestPlatformY() > nextSpawnY - verticalSpacing * 2f)
        {
            SpawnPlatformRow();
        }

        RecyclePlatforms();
    }

    void SpawnPlatformRow()
    {
        // 決定這排要生成幾個平台 (1到platformsPerRow之間)
        int spawnCount = Random.Range(1, platformsPerRow + 1);
        
        // 隨機選擇生成位置
        List<float> xPositions = new List<float>();
        for (int i = 0; i < spawnCount; i++)
        {
            float x;
            do {
                x = Random.Range(spawnRangeX.x, spawnRangeX.y);
            } while (xPositions.Exists(pos => Mathf.Abs(pos - x) < 1.5f)); // 避免平台太近
            
            xPositions.Add(x);
            
            GameObject platform = Instantiate(platformPrefab, platformParent);
            platform.transform.position = new Vector3(x, nextSpawnY, 0);
            
            // 設定數學題目 (難度隨時間增加)
            int difficulty = Mathf.FloorToInt(Time.time / speedUpdateInterval);
            string question = GenerateQuestion(difficulty);
            platform.GetComponent<MathPlatform>().SetQuestion(question);
            
            activePlatforms.Add(platform);
        }

        nextSpawnY -= verticalSpacing; // 下次生成位置往下移
    }

    string GenerateQuestion(int difficulty)
    {
        int a = Random.Range(1, 3 + difficulty);
        int b = Random.Range(1, 3 + difficulty);
        return $"{a} + {b} = ?";
    }

    float GetHighestPlatformY()
    {
        float highestY = Mathf.NegativeInfinity;
        foreach (var platform in activePlatforms)
        {
            if (platform.transform.position.y > highestY)
                highestY = platform.transform.position.y;
        }
        return highestY;
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

    // 除錯用繪製
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            new Vector3(spawnRangeX.x, nextSpawnY, 0),
            new Vector3(spawnRangeX.y, nextSpawnY, 0)
        );
    }
}