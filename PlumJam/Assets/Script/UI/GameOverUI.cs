using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;

    public void Awake()
    {
        PlayerVital.OnPlayerDie += (s, e) => GameOver();
        restartButton.onClick.AddListener(Restart);

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        restartButton.onClick.RemoveAllListeners();
    }

    private void GameOver()
    {
        gameObject.SetActive(true);
    }

    private void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
