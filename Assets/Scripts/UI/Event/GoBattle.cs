using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBattle : MonoBehaviour
{
    public void OnMouseDown()
    {
        BattleManager.stageData = Resources.Load<StageData>($"StageData/{gameObject.name}");

        Managers.Scene.LoadScene("BattleScene");

        Debug.Log("Battle Scene load complete");
    }
}
