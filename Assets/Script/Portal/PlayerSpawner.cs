using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public static Vector3 spawnPosition;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPosition;
        }
    }

    public static void SetSpawnPosition(Vector3 pos)
    {
        spawnPosition = pos;
    }
}