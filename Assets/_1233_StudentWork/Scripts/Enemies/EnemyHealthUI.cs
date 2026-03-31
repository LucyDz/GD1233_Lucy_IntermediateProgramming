using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image _enemyHealthFill;
    private EnemyHealth _enemyHealth;

    private void Start()
    {
        HandlePlayerAssigned(gameObject);
    }

    private void HandlePlayerAssigned(GameObject enemyObject)
    {
        if (enemyObject == null)
        {
            RefreshHealthBar(null);
            return;
        }

        _enemyHealth = enemyObject.GetComponentInChildren<EnemyHealth>();
        if (_enemyHealth == null)
        {
            Debug.LogError("GameUI: Enemy object does not have a Health component");
            return;
        }

        _enemyHealth.OnHealthChanged += RefreshHealthBar;
        RefreshHealthBar(_enemyHealth);
    }

    private void RefreshHealthBar(EnemyHealth health)
    {
        if (_enemyHealthFill == null) return;

        _enemyHealthFill.fillAmount = health != null ? health.NormalizedHealth : 0f;
    }
}
