using System.Collections;
using UnityEngine;

public class BudBrain : MonoBehaviour
{
  public enum FireMode
    {
        FixedAxis,
        DirectAim,
        ArcFire,
    }

    [Header("Components")]
    [SerializeField] private EnemyHealth _health;

    [SerializeField] private ProjectileWeapon _weapon;
    [SerializeField] private DetectionSystem _detection;
    [SerializeField] private RotateToTarget _rotator;
    [SerializeField] private EnemyAnimatorDriver _animator;

    [Header ("Settings")]
    [SerializeField] private FireMode _mode = FireMode.DirectAim;

    [SerializeField] private Vector3 _fixedAxis = Vector3.forward;

    private ITargetProvider _targetProvider;

    private void Awake()
    {
        _targetProvider = GetComponent<ITargetProvider>();
        if (_health == null) _health = GetComponent<EnemyHealth>();
        if (_animator == null) _animator = GetComponent<EnemyAnimatorDriver>();
    }

    private void Update()
    {
        if (_health != null && _health.IsDead) return;

        switch (_mode)
        {
            case FireMode.FixedAxis:
                if (_weapon.CanFire)
                {
                    _animator?.TriggerAttack();
                    _weapon.Fire(transform.TransformDirection(_fixedAxis), true);
                }
                break;

            case FireMode.DirectAim:
                HandleDirectAim();
                break;

            case FireMode.ArcFire:
                HandleArcFire();
                break;
        }
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += HandleDamaged;
            _health.OnDied += HandleDied;
        }
    }
    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDied -= HandleDied;
        }
    }
    private void HandleDirectAim()
    {
        if (_targetProvider == null || !_targetProvider.HasTarget) return;

        var target = _targetProvider.GetTarget();
        var targetPos = _targetProvider.GetTargetPosition();
        if (_detection.IsTargetInDetectionRange(target) && _detection.HasLineOfSight(target, _targetProvider.GetOffset()))
        {
            _rotator?.FacePosition(targetPos);
            if (_weapon.CanFire)
            {
                _animator?.TriggerAttack();
                _weapon.Fire(targetPos);
            }
        }
    }

    private void HandleArcFire()
    {
        if (_targetProvider == null || !_targetProvider.HasTarget) return;

        var target = _targetProvider.GetTarget();
        var targetPos = _targetProvider.GetTargetPosition();
        if (_detection.IsTargetInDetectionRange(target) && _detection.HasLineOfSight(target, _targetProvider.GetOffset()))
        {
            _rotator?.FacePosition(targetPos);
            if (_weapon.CanFire)
            {
                _animator?.TriggerAttack();
                _weapon.FireArc(targetPos);
            }
        }
    }

    private void HandleDamaged(DamageInfo info)
    {
        Debug.Log(
            $"[Bud] Hit by " +
            $"{info.Source?.name ?? "Unknown"} " +
            $"for {info.Amount} damage. " +
            $"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
        if (_health.CurrentHealth > 0)
            _animator?.TriggerHit();
    }

    private void HandleDied()
    {
        // Stop firing, maybe play an effect
        enabled = false;
        _animator?.TriggerDie();
        StartCoroutine(WaitForDeath());
        
        
    }
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Destroy(gameObject);
    }
}
