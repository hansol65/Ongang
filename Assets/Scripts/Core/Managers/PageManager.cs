using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject DevilMap;
    public GameObject Lobby;

    private void OnMouseDown()
    {
        // DevilMap Ȱ��ȭ
        DevilMap.SetActive(true);
        // Lobby ��Ȱ��ȭ
        Lobby.SetActive(false);

        Debug.Log("���������� �̵�");
    }
}
