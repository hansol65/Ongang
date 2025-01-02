using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
    }

    public void GoToBattleField()
    {
        SceneManager.LoadScene("Battlefield");
    }
}
