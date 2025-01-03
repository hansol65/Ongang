using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleButton : MonoBehaviour
{
    [SerializeField] private Button round1; // Field 1 ��ư
    [SerializeField] private Button round2; // Field 2 ��ư
    [SerializeField] private Button round3; // Field 3 ��ư



    void Start()
    {
        // Field 1 ��ư Ŭ�� �� Field1 ������ �̵�
        if (round1 != null)
        {
            round1.onClick.AddListener(() => SceneManager.LoadScene("Field1"));
        }

        // Field 2 ��ư Ŭ�� �� Field2 ������ �̵�
        if (round2 != null)
        {
            round2.onClick.AddListener(() => SceneManager.LoadScene("Field2"));
        }

        // Field 3 ��ư Ŭ�� �� Field3 ������ �̵�
        if (round3 != null)
        {
            round3.onClick.AddListener(() => SceneManager.LoadScene("Field3"));
        }

    }
}