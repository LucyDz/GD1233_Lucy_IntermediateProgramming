using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// In game HUD shown when not paused
/// </summary>
public class GameUI : MenuBase
{
    public override GameMenus MenuType()
    {
        return GameMenus.InGameUI;
    }

    [SerializeField] private Image _healthFillImage;

    private EnemyHealth _playerHealth;

    [SerializeField] private TextMeshProUGUI _bombcounter;
    private int _bombammo;

    private void OnEnable()
    {
        if (PlayerMgr.Instance == null)
        {
            Debug.LogError("GameUI: PlayerMgr is null");
            return;
        }

        //if the player was set already
        if (PlayerMgr.Instance.HasSpawnedPlayer)
        {
            HandlePlayerAssigned(PlayerMgr.Instance.PlayerObject);
            return;
        }
        //Otherwise wait for the player to spawn
        PlayerMgr.Instance.OnPlayerAssigned += HandlePlayerAssigned;

        
    }

    private void OnDisable()
    {
        if (PlayerMgr.Instance != null) PlayerMgr.Instance.OnPlayerAssigned -= HandlePlayerAssigned;
    }

    private void HandlePlayerAssigned(GameObject playerObject)
    {
        if (playerObject == null)
        {
            RefreshHealthBar(null);
            return;
        }

        _playerHealth = playerObject.GetComponentInChildren<EnemyHealth>();
        _bombammo = playerObject.GetComponentInChildren<PlayerController>().BombAmmo;
        if (_playerHealth == null)
        {
            Debug.LogError("GameUI: Player object does not have a Health component");
            return;
        }

        _playerHealth.OnHealthChanged += RefreshHealthBar;
        playerObject.GetComponentInChildren<PlayerController>().OnBombAmmoChanged += RefreshBombAmmo;

        RefreshHealthBar(_playerHealth);
        RefreshBombAmmo(_bombammo);
    }

    private void RefreshHealthBar(EnemyHealth health)
    {
        if (_healthFillImage == null) return;

        _healthFillImage.fillAmount = health != null ? health.NormalizedHealth : 0f;
    }

    private void RefreshBombAmmo(int addedammo)
    {
        if (_bombcounter == null) return;
       
        _bombcounter.text = addedammo.ToString();
        
    } 
}
