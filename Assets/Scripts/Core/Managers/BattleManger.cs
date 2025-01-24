using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private GameObject stage1;
    private GameObject stage2;
    private GameObject stage3;

    private GameObject currentStage;

    public string CurrentStageName { get; private set; }

    public void SetCurrentStage(string stageName)
    {
        CurrentStageName = stageName;

        if (stageName == "Stage_1") currentStage = stage1;
        else if (stageName == "Stage_2") currentStage = stage2;
        else if (stageName == "Stage_3") currentStage = stage3;
        else currentStage = null;

        Debug.Log($"현재 스테이지 설정됨: {CurrentStageName}");
    }

    public void ActivateCurrentStage()
    {
        if (currentStage != null)
        {
            currentStage.SetActive(true);
            Debug.Log($"{CurrentStageName} 활성화 완료!");
        }
        else
        {
            Debug.LogError("현재 스테이지가 설정되지 않았거나, 스테이지 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void OnEnable()
    {
        // BattleScene에서 오브젝트 동적 찾기
        stage1 = GameObject.Find("Stage_1");
        stage2 = GameObject.Find("Stage_2");
        stage3 = GameObject.Find("Stage_3");
    }
}