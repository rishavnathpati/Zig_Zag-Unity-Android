using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject zigZagPanel;
    public GameObject gameOverPanel;
    public Text score;
    public Text HighScore2;
    public Text tapText;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameStart()
    {
        tapText.gameObject.SetActive(false);
        zigZagPanel.GetComponent<Animator>().Play("panelUp");
    }

    public void GameOver()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        HighScore2.text = PlayerPrefs.GetInt("highScore").ToString();
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        JasperMovement.instance.RespawnPlayer();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
