using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBattle : MonoBehaviour
{
    public void OnMouseDown()
    {
        Managers.Scene.LoadScene("BattleScene");

        Debug.Log("Battle Scene load complete");

        BattleManager.currentStageName = gameObject.name;

        Managers.Battle.battleInit();
    }
}
