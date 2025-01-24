using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBattle : MonoBehaviour
{
    [SerializeField] private string stageName;

    public void OnMouseDown()
    {
        Managers.Battle.SetCurrentStage(stageName);

        Managers.Scene.LoadScene("BattleScene");
    }
}
