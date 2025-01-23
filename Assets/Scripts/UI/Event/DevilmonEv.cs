using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DevilmonEv : MonoBehaviour
{
    public GameObject DevilMap;
    public GameObject Lobby;

    // Lobby -> DevilMap
    public void OnMouseDown()
    {
        Managers.Page.ChangePage(Lobby, DevilMap);
    }
}
