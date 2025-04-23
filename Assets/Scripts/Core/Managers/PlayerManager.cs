using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerManager
{
    private GameObject playerUnitInstance;
    public GameObject getPlayer() { return playerUnitInstance; }

    public void SpawnPlayerUnit(GameObject playerPrefab, Vector2 spawnPosition)
    {
        if (playerUnitInstance != null)
        {
            Debug.Log("Player Unit already exist.");
            return;
        }

        playerUnitInstance = Object.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Object.DontDestroyOnLoad(playerUnitInstance);

        // Player layer
        playerUnitInstance.layer = LayerMask.NameToLayer("Player");

        // Attach: MovementAI
        if (playerUnitInstance.GetComponent<MovementAI>() == null)
        {
            playerUnitInstance.AddComponent<MovementAI>();
        }

        // Attach: Unit
        if(playerUnitInstance.GetComponent<Unit>() == null)
        {
            playerUnitInstance.AddComponent<Unit>();
            initUnit(playerUnitInstance.GetComponent<Unit>());
            Debug.Log($"현재 경험치: {playerUnitInstance.GetComponent<Unit>().stat.Exp}");
        }

        // Attach: AttackAI
        if (playerUnitInstance.GetComponent<AttackAI>() == null)
        {
            var attackAI = playerUnitInstance.AddComponent<AttackAI>();
            attackAI.enabled = false;
        }

        Debug.Log("[PlayerManager] Player Unit created.");
    }

    private void initUnit(Unit unit)
    {
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        string jsonData = File.ReadAllText(path);
        unit.stat = JsonUtility.FromJson<Stat>(jsonData);
        playerUnitInstance.GetComponent<Unit>().team = TeamType.Ally; // 플레이어 유닛의 팀을 Ally 로 설정
    }

    public void EnterBattleField()
    {
        if (playerUnitInstance == null) return;

        var movementAI = playerUnitInstance.GetComponent<MovementAI>();
        if (movementAI != null)
        {
            movementAI.StopMovement();
            movementAI.enabled = false;
        }

        var attackAI = playerUnitInstance.GetComponent<AttackAI>();
        if (attackAI != null)
        {
            attackAI.enabled = true;
        }

        // BattleManager 에 Unit 등록
        Managers.Battle.RegisterUnit(playerUnitInstance.GetComponent<Unit>());

        Debug.Log("[PlayerManager] Entered Battle Field.");
    }

    public void ExitBattleField()
    {
        if (playerUnitInstance == null) return;

        // MovementAI Ȱ��ȭ
        var movementAI = playerUnitInstance.GetComponent<MovementAI>();
        if (movementAI != null)
        {
            movementAI.enabled = true;
            movementAI.ResumeMovement();
        }

        // AttackAI ��Ȱ��ȭ
        var attackAI = playerUnitInstance.GetComponent<AttackAI>();
        if (attackAI != null)
        {
            attackAI.enabled = false;
        }

        Debug.Log("[PlayerManager] Exited Battle Field.");
    }
}
