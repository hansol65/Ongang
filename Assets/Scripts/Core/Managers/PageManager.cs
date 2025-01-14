using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject DevilMap;
    public GameObject Lobby;

    private void OnMouseDown()
    {
        // DevilMap 활성화
        DevilMap.SetActive(true);
        // Lobby 비활성화
        Lobby.SetActive(false);

        Debug.Log("데빌맵으로 이동");
    }
}
