using UnityEngine;
/// <summary>
/// Level complete screen
/// Allows for quitting or next level
/// </summary>

public class LevelCompleteUI : MenuBase
{
   
    public override GameMenus MenuType()
    {
        return GameMenus.LevelCompleteMenu;
    }

    public void ButtonNextLevel()
    {
        //LevelMgr._currentLevelIndex++;
        SceneMgr.Instance.LoadScene(GameScenes.Gameplay, GameMenus.InGameUI);
    }

    public void ButtonMainMenu()
    {
        SceneMgr.Instance.LoadScene(GameScenes.MainMenu, GameMenus.MainMenu);
    }
}
