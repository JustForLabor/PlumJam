using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    public void Awake()
    {
        global::Goal.onGoal += (s, e) => Goal();
        restartButton.onClick.AddListener(Restart);

        gameObject.SetActive(false);
    }

    private void Goal()
    {
        gameObject.SetActive(true);
    }

    private void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
