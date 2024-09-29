using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    public GameObject close;
    // Start is called before the first frame update
    void Start()
    {
        close.gameObject.SetActive(true);
        GetComponent<Button>().onClick.AddListener(CloseUIOnClick);
    }

    public void CloseUIOnClick()
    {
        close.SetActive(false);
    }
    // Update is called once per frame

}
