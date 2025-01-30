using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBattle : MonoBehaviour
{
    public void OnMouseDown()
    {
        string suffix = gameObject.name.Substring(gameObject.name.Length - 1);
        BattleManager.currentStageName = suffix;

        Managers.Scene.LoadScene("BattleScene");

        Debug.Log("Battle Scene load complete");
    }
}
