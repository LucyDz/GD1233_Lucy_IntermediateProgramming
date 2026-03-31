using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// Settings menu
/// Should include sliders and toggles for player preferences
/// Such as audio settings or accessibility settings
/// </summary>
public class Settings : MenuBase
{
    [FormerlySerializedAs("BackButton")] [SerializeField] private Button _backButton;
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;

    private void OnEnable()
    {
        
        _backButton.Select();

        _masterSlider.value = AudioMgr.Instance.GlobalVolume;
        _musicSlider.value = AudioMgr.Instance.MusicVolume;
        _soundSlider.value = AudioMgr.Instance.SfxVolume;
    }

    public override GameMenus MenuType()
    {
        return GameMenus.SettingsMenu;
    }

    public void Close()
    {
        UIMgr.Instance.HideMenu(GameMenus.SettingsMenu);
        SaveUtil.Save();
    }

    public void SetMasterVol(float value)
    {
        Debug.Log($"Master Volume is {value}");
        AudioMgr.Instance.GlobalVolume = value;
    }
    public void SetMusicVol(float value)
    {
        Debug.Log($"Music Volume is {value}");
        AudioMgr.Instance.MusicVolume = value;
    }
    public void SetSoundVol(float value)
    {
        Debug.Log($"Sound Volume is {value}");
        AudioMgr.Instance.SfxVolume = value;
    }
}
