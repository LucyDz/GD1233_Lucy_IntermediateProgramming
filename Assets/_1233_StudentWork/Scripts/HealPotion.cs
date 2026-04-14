using UnityEngine;

public class HealPotion : MonoBehaviour
{
    
    [SerializeField] private GameObject _potion;
    [SerializeField] private int _healamount;
    [SerializeField] private GameObject _pickupVFX;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"HealPotion collided with {other.gameObject.name}");
        var health = other.GetComponent<EnemyHealth>();
        if (health == null) return;

        if(_pickupVFX != null)
            Instantiate(_pickupVFX, transform.position, Quaternion.identity);
        health.Heal(_healamount);
        Destroy(_potion);
    }

    
}
