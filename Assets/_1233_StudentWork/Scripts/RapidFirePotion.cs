using System.Collections;
using UnityEngine;

public class RapidFirePotion : MonoBehaviour
{
    [SerializeField] private float _rapidFireSeconds = 5f;
    [SerializeField] private GameObject _potion;
    [SerializeField] private GameObject _pickupVFX;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"RapidFirePotion collided with {other.gameObject.name}");
        var weapon = other.GetComponent<ProjectileWeapon>();
        if (weapon == null) return;

        if (_pickupVFX != null)
            Instantiate(_pickupVFX, transform.position, Quaternion.identity);
        weapon.StartCoroutine(ApplyRapidFire(weapon));
        Destroy(_potion);
    }

    private IEnumerator ApplyRapidFire(ProjectileWeapon weapon)
    {
        weapon._fireRate = 10f;
        yield return new WaitForSeconds(_rapidFireSeconds);
        weapon._fireRate = 2f;
    }
}
