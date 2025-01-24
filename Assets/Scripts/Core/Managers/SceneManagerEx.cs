using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleScene")
        {
            Managers.Battle.ActivateCurrentStage();
        }

        // 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}