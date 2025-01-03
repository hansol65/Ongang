using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonHandler : MonoBehaviour
{
    [Tooltip("Go to Battle!")]
    [SerializeField] private Button battleButton;

    [Tooltip("Go to Lobby!")]
    [SerializeField] private Button lobbyButton;


    void Start()
    {
        // 버튼 클릭 시 Managers의 Scene 매니저를 통해 Battlefield 씬으로 이동
        battleButton.onClick.AddListener(() => SceneManager.LoadScene("Battlefield"));
        // 버튼 클릭 시 MainScene으로 이동
        lobbyButton.onClick.AddListener(() => SceneManager.LoadScene("MainScene"));

    }
}