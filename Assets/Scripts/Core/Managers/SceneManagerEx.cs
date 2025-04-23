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
        // 씬이 로드될 때마다 실행될 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoadedHandler;
    }

    ~SceneManagerEx()
    {
        // 소멸될 때 메서드 해제
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
            Managers.Player.EnterBattleField();
        } else {
            Managers.Player.ExitBattleField();
         }
    }
}
