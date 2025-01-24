using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 싱글톤 인스턴스
    public static Managers Instance { get { Init(); return s_instance; } }

    GameManager s_game = new GameManager();
    SceneManagerEx s_scene = new SceneManagerEx();
    PageManager s_page = new PageManager();
    public BattleManager s_battle; // MonoBehaviour 참조

    public static GameManager Game { get { return Instance.s_game; } }
    public static SceneManagerEx Scene { get { return Instance.s_scene; } }
    public static PageManager Page { get { return Instance.s_page; } }
    public static BattleManager Battle { get { return Instance.s_battle; } }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject managerObject = GameObject.Find("@Manager");
            if (managerObject == null)
            {
                managerObject = new GameObject("@Manager");
                managerObject.AddComponent<Managers>();
            }

            // Managers 초기화
            DontDestroyOnLoad(managerObject);
            s_instance = managerObject.GetComponent<Managers>();

            // BattleManager 추가
            s_instance.s_battle = managerObject.AddComponent<BattleManager>();
        }
    }
}
