using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}