using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private GameObject playerUnitInstance;

    public void SpawnPlayerUnit(GameObject playerPrefab, Vector2 spawnPosition)
    {
        if (playerUnitInstance != null)
        {
            Debug.Log("Player Unit already exist.");
            return;
        }

        playerUnitInstance = Object.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Object.DontDestroyOnLoad(playerUnitInstance);

        // Player layer�� ����
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
        }

        // Attach: AttackAI
        if (playerUnitInstance.GetComponent<AttackAI>() == null)
        {
            var attackAI = playerUnitInstance.AddComponent<AttackAI>();
            attackAI.enabled = false; // ó������ AttackAI ��Ȱ��ȭ
        }

        Debug.Log("[PlayerManager] Player Unit created.");
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
