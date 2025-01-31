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
            Debug.Log("Player Unit already exists.");
            return;
        }

        // Instantiate the player unit
        playerUnitInstance = Object.Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Object.DontDestroyOnLoad(playerUnitInstance);

        // Set layer to "Player" (Layer 6)
        playerUnitInstance.layer = 6; // Assuming Layer 6 corresponds to "Player"
        Debug.Log($"[PlayerManager] Player Unit layer set to: {LayerMask.LayerToName(6)}");

        // Attach: MovementAI
        if (playerUnitInstance.GetComponent<MovementAI>() == null)
        {
            playerUnitInstance.AddComponent<MovementAI>();
        }

        // Attach: Unit
        if (playerUnitInstance.GetComponent<Unit>() == null)
        {
            playerUnitInstance.AddComponent<Unit>();
        }

        // Attach: AttackAI
        if (playerUnitInstance.GetComponent<AttackAI>() == null)
        {
            var attackAI = playerUnitInstance.AddComponent<AttackAI>();
            attackAI.enabled = false; // 처음부터 AttackAI 비활성화
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

        // MovementAI 활성화
        var movementAI = playerUnitInstance.GetComponent<MovementAI>();
        if (movementAI != null)
        {
            movementAI.enabled = true;
            movementAI.ResumeMovement();
        }

        // AttackAI 비활성화
        var attackAI = playerUnitInstance.GetComponent<AttackAI>();
        if (attackAI != null)
        {
            attackAI.enabled = false;
        }

        Debug.Log("[PlayerManager] Exited Battle Field.");
    }
}
