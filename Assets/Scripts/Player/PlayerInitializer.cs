using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public GameObject playerPrefab;
    public Vector2 spawnPosition = new Vector2(-3, 0);

    private void Start()
    {
        Managers.Player.SpawnPlayerUnit(playerPrefab, spawnPosition);
    }
}
