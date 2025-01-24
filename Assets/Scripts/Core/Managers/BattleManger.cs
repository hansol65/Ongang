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

        Debug.Log($"���� �������� ������: {CurrentStageName}");
    }

    public void ActivateCurrentStage()
    {
        if (currentStage != null)
        {
            currentStage.SetActive(true);
            Debug.Log($"{CurrentStageName} Ȱ��ȭ �Ϸ�!");
        }
        else
        {
            Debug.LogError("���� ���������� �������� �ʾҰų�, �������� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void OnEnable()
    {
        // BattleScene���� ������Ʈ ���� ã��
        stage1 = GameObject.Find("Stage_1");
        stage2 = GameObject.Find("Stage_2");
        stage3 = GameObject.Find("Stage_3");
    }
}