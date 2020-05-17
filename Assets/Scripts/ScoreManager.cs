using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scoreValue;
    public int highScore;
    public Text OnScreenScore;

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
        scoreValue = 0;
        PlayerPrefs.SetInt("score", scoreValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementScore()
    {
        scoreValue++;
        OnScreenScore.text = scoreValue.ToString();
        if (scoreValue % 100 == 0)
        {
            JasperMovement.instance.speedUp();
        }

    }



    public void startScore()
    {
        InvokeRepeating("incrementScore", 0.1f, 0.5f);
    }

    public void stopScore()
    {
        CancelInvoke("incrementScore");

        PlayerPrefs.SetInt("score", scoreValue);

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (scoreValue > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", scoreValue);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", scoreValue);
        }
    }
}
