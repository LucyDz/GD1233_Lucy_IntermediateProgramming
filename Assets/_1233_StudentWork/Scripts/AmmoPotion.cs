using UnityEngine;

public class AmmoPotion : MonoBehaviour
{
    
    [SerializeField] private GameObject _potion;
    [SerializeField] private int _ammoamount;
    [SerializeField] private GameObject _pickupVFX;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"AmmoPotion collided with {other.gameObject.name}");
        var player = other.GetComponentInChildren<PlayerController>();
        if (player == null) return;

        player.BombAmmo += _ammoamount;
        if (_pickupVFX != null)
            Instantiate(_pickupVFX, transform.position, Quaternion.identity);
        Destroy(_potion);

        
    }

    
}
