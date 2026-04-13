using UnityEngine;

public class AmmoPotion : MonoBehaviour
{
    
    [SerializeField] private GameObject _potion;
    [SerializeField] private int _ammoamount;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"AmmoPotion collided with {other.gameObject.name}");
        var player = other.GetComponentInChildren<PlayerController>();
        if (player == null) return;

        player.BombAmmo += _ammoamount;
        Destroy(_potion);

        
    }

    
}
