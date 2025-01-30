using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public event Action<Scene, LoadSceneMode> OnSceneLoaded;

    public SceneManagerEx()
    {
        SceneManager.sceneLoaded += OnSceneLoadedHandler;
    }

    ~SceneManagerEx()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedHandler;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnSceneLoadedHandler(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene}");

        OnSceneLoaded?.Invoke(scene, mode);

        if (scene.name == "BattleScene")
        {
            Managers.Battle.OnBattleSceneLoaded();
        }
    }
}
