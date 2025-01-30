using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game Data/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public GameObject[] monsterPrefabs; // ������������ ������ ���� ������ ���
    public int[] monsterCounts; // �� ���ͺ� ���� ����
}
