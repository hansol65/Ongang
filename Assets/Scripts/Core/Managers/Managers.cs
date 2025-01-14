using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    GameManager s_game = new GameManager();
    SceneManagerEx s_scene = new SceneManagerEx();

    public static GameManager Game { get { return Instance.s_game; } }
    public static SceneManagerEx Scene { get { return Instance.s_scene; } }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Manager");
            if (go == null)
            {
                go = new GameObject { name = "@Manager" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
