using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject zigZagPanel;
    public GameObject gameOverPanel;
    public GameObject pauseButtonObject, resumePanelObject;
    public Text score;
    public Text HighScore1;
    public Text HighScore2;
    public Text tapText;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HighScore1.text = PlayerPrefs.GetInt("highScore").ToString();

    }

    public void gameStart()
    {
        //HighScore1.text = PlayerPrefs.GetInt("highScore").ToString();
        tapText.gameObject.SetActive(false);
        zigZagPanel.GetComponent<Animator>().Play("panelUp");
    }

    public void gameOver()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        HighScore2.text = PlayerPrefs.GetInt("highScore").ToString();
        gameOverPanel.SetActive(true);

    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        resumePanelObject.gameObject.SetActive(true);
        pauseButtonObject.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumePanelObject.gameObject.SetActive(false);
        pauseButtonObject.gameObject.SetActive(true);
    }
}
