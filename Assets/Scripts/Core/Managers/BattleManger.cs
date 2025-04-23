using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager
{
    // 배틀 초기화에 필요한 정보들
    public static string currentStageName;
    public static StageData stageData;

    // 배틀 진행 상황
    public static List<Unit> allies = new List<Unit>();
    public static List<Unit> enemies = new List<Unit>();
    private BattleResult battleResult = BattleResult.Win;

    public void RegisterUnit(Unit unit)
    {
        if (unit.team == TeamType.Ally)
        {
            allies.Add(unit);
        } else
        {
            enemies.Add(unit);
        }
    }

    public void OnUnitDied()
    {        
        CheckBattleEnd();
    }

    private void CheckBattleEnd()
    {
        bool alliesDead = allies.All(u => u.getState() == UnitState.Dead);
        bool enemiesDead = enemies.All(u => u.getState() == UnitState.Dead);

        Debug.Log(allies + ": " + alliesDead);
        GameObject player = Managers.Player.getPlayer();
        Debug.Log(player.GetComponent<Unit>().getState());

        if (alliesDead && enemiesDead)
        {
            EndBattle(BattleResult.Draw);
        }
        else if (alliesDead)
        {
            EndBattle(BattleResult.Lose);
        } 
        else if (enemiesDead)
        {
            EndBattle(BattleResult.Win);
        }
    }

    private void EndBattle(BattleResult result)
    {
        foreach(Unit ally in allies)
        {
            if (ally.getState() != UnitState.Dead)
            {
                ally.setState(UnitState.Idle);
            }
        }

        foreach (Unit enemy in allies)
        {
            if (enemy.getState() != UnitState.Dead)
            {
                enemy.setState(UnitState.Idle);
            }
        }

        Debug.Log("배틀 종료: " + result);
        battleResult = result;

        // UI 호출
    }

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
                GameObject enemy = Object.Instantiate(stageData.monsterPrefabs[i], spawnPos, Quaternion.identity);
                
                Unit enemyUnit = enemy.GetComponent<Unit>();
                enemyUnit.team = TeamType.Enemy; // 생성한 적의 팀을 Enemy 로 설정
                RegisterUnit(enemyUnit);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(-5, 5), 0);
    }
}
