using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechange : MonoBehaviour
{
    public void Map1()
    {
        SceneManager.LoadScene("Level_1_Map");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Level_2_Map");
    }
}
