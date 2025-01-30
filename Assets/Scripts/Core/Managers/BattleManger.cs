using UnityEngine;

public class BattleManager
{
    public static string currentStageName;

    public static StageData stageData;

    public void OnBattleSceneLoaded()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < stageData.monsterPrefabs.Length; i++)
        {
            for (int j = 0; j < stageData.monsterCounts[i]; j++)
            {
                Vector2 spawnPos = GetRandomSpawnPosition();
                Object.Instantiate(stageData.monsterPrefabs[i], spawnPos, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(-5, 5), 0);
    }
}
