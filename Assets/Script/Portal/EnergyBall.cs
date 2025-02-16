using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HeroKnight>().hasEnergyBall = true;
            Destroy(gameObject); 
        }
        Debug.Log("Player picked up the energyball");
    }
}