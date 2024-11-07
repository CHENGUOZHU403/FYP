using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManagers inventoryManagers;

    public ItemType itemType;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManagers = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagers>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Pick Up");
        if (collision.gameObject.tag == "Player")
        {

            int leftOverItems = inventoryManagers.AddItem(itemName, quantity, sprite,itemDescription, itemType);
            if (leftOverItems == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
            //Destroy(gameObject);
        }
    }

    //private void Oncollision2D(Collision2D collision)
    //{

    //    Debug.Log("hi");
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        inventoryManagers.AddItem(itemName, quantity, sprite);

    //        Destroy(gameObject);
    //    }
    //}
}