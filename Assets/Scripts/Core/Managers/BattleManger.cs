using UnityEngine;

public class BattleManager
{
    public static string currentStageName;

    public void OnBattleSceneLoaded()
    {
        GameObject stageContainer = GameObject.Find("StageContainer");

        if (stageContainer != null)
        {
            Transform childTransform = stageContainer.transform.Find(currentStageName);

            if(childTransform != null)
            {
                GameObject stage1 = childTransform.gameObject;
                if (stage1 != null)
                {
                    stage1.SetActive(true);
                }
            }
            else
            {
                Debug.Log($"Stage {currentStageName} not found in BattleScene.");
            }
        }
        else
        {
            Debug.LogError("StageContainer not found in BattleScene.");
        }
    }
}
