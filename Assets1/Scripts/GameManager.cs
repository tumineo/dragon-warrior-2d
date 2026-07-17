using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private float gameOverDelay = 1.5f; // delay for death animation

    private void Awake()
    {
        instance = this;
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverAfterDelay());
    }

    private IEnumerator ShowGameOverAfterDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}