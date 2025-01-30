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

    Debug.Log("[PlayerManager] Player Unit created.");
    }
}
