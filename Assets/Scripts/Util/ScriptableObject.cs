using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game Data/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public GameObject[] monsterPrefabs; // 스테이지에서 스폰할 몬스터 프리팹 목록
    public int[] monsterCounts; // 각 몬스터별 스폰 개수
}
