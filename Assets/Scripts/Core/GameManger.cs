using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public BattleManager battleManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        Debug.Log("GameManager Initialized");
    }

    private void Start()
    {
        // 게임 초기화 작업
        InitializeGame();
    }

    private void InitializeGame()
    {
        Debug.Log("Game Initialized!");
        battleManager = new GameObject("BattleManager").AddComponent<BattleManager>();

        BattleTest();
    }

    private void BattleTest()
    {
        // 플레이어와 적 유닛 생성
        Player player = CreatePlayer("Hero", 100, 20, 5);
        Enemy enemy = CreateEnemy("Goblin", 50, 15, 3);

        // 전투 시뮬레이션
        battleManager.ProcessBattle(player, enemy);
    }

    private Player CreatePlayer(string name, int health, int attack, int defense)
    {
        Player player = new GameObject(name).AddComponent<Player>();
        player.Initialize(name, health, attack);
        return player;
    }

    private Enemy CreateEnemy(string name, int health, int attack, int defense)
    {
        Enemy enemy = new GameObject(name).AddComponent<Enemy>();
        enemy.Initialize(name, health, attack);
        return enemy;
    }

    public void GoToBattleField()
    {
        SceneManager.LoadScene("Battlefield");
    }
}
