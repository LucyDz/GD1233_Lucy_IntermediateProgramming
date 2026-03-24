
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Manager to apply level based data to the game state before the game loop begins
/// Might contain a list of difficulties, levels, etc.
/// </summary>
public class LevelMgr : Singleton<LevelMgr>
{
    //[SerializeField] private string[] _levelSceneNames;
    //public string[] LevelSceneNames => _levelSceneNames;
    [Serializable]
    public class LevelData
    {
        public string SceneName;
        public string LevelName;
        public Sprite LevelIcon;
    }
    [SerializeField] private LevelData[] _allLevelData;
    public LevelData[] AllLevelData => _allLevelData;

    private int _currentLevelIndex;
    public bool IsLevelLoaded {  get; private set; }

    public void LoadCurrentLevel()
    {
        IsLevelLoaded = false;
        StartCoroutine(LoadLevelRoutine());
    }
    public void SetCurrentLevel(int currentLevelIndex)
    {
        _currentLevelIndex = currentLevelIndex;
    }
    public void LevelIncrease()
    {
        
        if (_currentLevelIndex >= _allLevelData.Length -1) { _currentLevelIndex = 0; }
        else _currentLevelIndex++;
    }

    private IEnumerator LoadLevelRoutine()
    {
        string levelName = _allLevelData[_currentLevelIndex].SceneName;

        Debug.Log($"LevelMgr: Loading {levelName} additively");

        var asyncOperation = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        while (asyncOperation is { isDone: false }) yield return null;

        Debug.Log($"LevelMgr: {levelName} loaded");

        IsLevelLoaded= true;
    }
}