using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private Button round1; // Field 1 버튼
    [SerializeField] private Button round2; // Field 2 버튼
    [SerializeField] private Button round3; // Field 3 버튼



    void Start()
    {
        // Field 1 버튼 클릭 시 Field1 씬으로 이동
        if (round1 != null)
        {
            round1.onClick.AddListener(() => SceneManager.LoadScene("Field1"));
        }

        // Field 2 버튼 클릭 시 Field2 씬으로 이동
        if (round2 != null)
        {
            round2.onClick.AddListener(() => SceneManager.LoadScene("Field2"));
        }

        // Field 3 버튼 클릭 시 Field3 씬으로 이동
        if (round3 != null)
        {
            round3.onClick.AddListener(() => SceneManager.LoadScene("Field3"));
        }

    }
}