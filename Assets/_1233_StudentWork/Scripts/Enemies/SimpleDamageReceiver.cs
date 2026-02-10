using UnityEngine;

/// <summary>
/// A basic implementation of IDamageReceiver that
/// forwards damage to a Health component.
/// This allows the collider to be on a different
/// GameObject than the Health component
/// </summary>
public class SimpleDamageReceiver : MonoBehaviour, IDamageReceiver
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private float _damageMultiplier = 1f;

    private void Awake()
    {
        //Fallback to local if not assigned
        if (_health == null ) _health = GetComponent<EnemyHealth>();
    }

    public void ApplyDamage(DamageInfo info)
    {
        if (_health == null) return;

        info.Amount = Mathf.RoundToInt(info.Amount * _damageMultiplier);

        _health.ApplyDamage(info);
    }
}
