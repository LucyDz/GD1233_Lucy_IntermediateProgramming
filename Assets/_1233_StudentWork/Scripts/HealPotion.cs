using UnityEngine;

public class HealPotion : MonoBehaviour
{
    
    [SerializeField] private GameObject _potion;
    [SerializeField] private int _healamount;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"HealPotion collided with {other.gameObject.name}");
        var health = other.GetComponent<EnemyHealth>();
        if (health == null) return;

        health.Heal(_healamount);
        Destroy(_potion);
    }

    
}
