using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public void ProcessBattle(Player player, Enemy enemy)
    {
        Debug.Log("Battle Start!");

        // �÷��̾ ���� ����
        player.Attack(enemy);

        // ���� ��� �ִٸ� �ݰ�
        if (enemy.currentHP > 0)
        {
            enemy.Attack(player);
        }

        // ���� ��� ���
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
