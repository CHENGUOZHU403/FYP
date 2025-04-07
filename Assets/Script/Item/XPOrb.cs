
using UnityEngine;
using System.Collections; 

public class XPOrb : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatHeight = 0.5f;
    
    private int xpValue = 20;
    private Vector3 startPos;

    public void Initialize(int xp)
    {
        xpValue = xp;
        startPos = transform.position;
        StartCoroutine(FloatAnimation());
    }

    private IEnumerator FloatAnimation()
    {
        while(true)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HeroKnight player = other.GetComponent<HeroKnight>();
            if(player != null)
            {
                player.AddXP(xpValue);
                Destroy(gameObject);
                Debug.Log($"Gain {xpValue} Exp");
            }
        }
    }
}