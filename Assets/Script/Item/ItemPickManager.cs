using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class ItemPickManager : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public PlayerData playerData;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
               if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
               {
                    interactObj.Interact();
               }
           }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Debug.Log("111");
        }
    }
}




