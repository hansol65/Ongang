using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonHandler : MonoBehaviour
{
    [Tooltip("Go to Battle!")]
    [SerializeField] private Button sceneButton;

    void Start()
    {
        // 버튼 클릭 시 Managers의 Scene 매니저를 통해 Battlefield 씬으로 이동
        sceneButton.onClick.AddListener(() => SceneManager.LoadScene("Battlefield"));
    }
}