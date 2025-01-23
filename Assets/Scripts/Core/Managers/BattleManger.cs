using UnityEngine;

public class BattleManager
{
    public static string currentStageName;
    private GameObject currentStageObject;

    public void battleInit()
    {
        // GameObject 찾기
        currentStageObject = GameObject.Find(currentStageName);

        if (currentStageObject == null)
        {
            Debug.LogError($"GameObject with name '{currentStageName}' not found in the scene!");
            return;
        }

        // 활성화
        currentStageObject.SetActive(true);

        // 활성화 상태 확인
        if (currentStageObject.activeSelf)
        {
            Debug.Log($"{currentStageObject.name} is now active!");
        }
        else
        {
            Debug.LogError($"{currentStageObject.name} could not be activated!");
        }
    }
}
