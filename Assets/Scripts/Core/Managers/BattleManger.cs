using UnityEngine;

public class BattleManager
{
    public static string currentStageName;
    private GameObject currentStageObject;

    public void battleInit()
    {
        // GameObject ã��
        currentStageObject = GameObject.Find(currentStageName);

        if (currentStageObject == null)
        {
            Debug.LogError($"GameObject with name '{currentStageName}' not found in the scene!");
            return;
        }

        // Ȱ��ȭ
        currentStageObject.SetActive(true);

        // Ȱ��ȭ ���� Ȯ��
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
