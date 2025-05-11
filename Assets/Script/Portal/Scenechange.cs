using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    public Vector3 lv1dungeonEntrancePosition = new Vector3(0, 0, 0);
    public void Map1()
    {
        GameManager.Instance.playerPosition = lv1dungeonEntrancePosition;
        SceneManager.LoadScene("Level_1_Map");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Level_2_Map");
    }
}
