using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public void ProcessBattle(Player player, Enemy enemy)
    {
        Debug.Log("Battle Start!");

        // 플레이어가 먼저 공격
        player.Attack(enemy);

        // 적이 살아 있다면 반격
        if (enemy.currentHP > 0)
        {
            enemy.Attack(player);
        }

        // 전투 결과 출력
        if (player.currentHP > 0 && enemy.currentHP <= 0)
        {
            Debug.Log($"{player.unitName} wins!");
        }
        else if (player.currentHP <= 0 && enemy.currentHP > 0)
        {
            Debug.Log($"{enemy.unitName} wins!");
        }
        else
        {
            Debug.Log("Both units are still alive.");
        }
    }
}
