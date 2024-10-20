using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUI : MonoBehaviour
{
    public GameObject close;
    public GameObject closefont;
    // Start is called before the first frame update
    void Start()
    {
        close.gameObject.SetActive(true);
        closefont.gameObject.SetActive(false);
        GetComponent<Button>().onClick.AddListener(CloseUIOnClick);
    }

    public void CloseUIOnClick()
    {
        close.SetActive(false);
        closefont.gameObject.SetActive(true);
    }
    // Update is called once per frame

}
