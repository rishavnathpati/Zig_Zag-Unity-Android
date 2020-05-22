using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int scoreValue;
    public int highScore;
    public int diamondValue;
    public Text OnScreenScore;
    public Text OnScreenDiamondScore;

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
        diamondValue = 0;
        PlayerPrefs.SetInt("score", scoreValue);
        PlayerPrefs.SetInt("diamondscore", diamondValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void incrementScore()
    {
        scoreValue++;
        OnScreenScore.text = "Distance: " + scoreValue.ToString() + "m";
        OnScreenDiamondScore.text = "Diamonds: " + diamondValue.ToString();
        if (scoreValue % 100 == 0)
        {
            JasperMovement.instance.speedUp();
        }

    }

    public void incrementDiamondScore()
    {
        diamondValue++;
    }



    public void startScore()
    {
        InvokeRepeating("incrementScore", 0.1f, 0.5f);
    }

    public void stopScore()
    {
        CancelInvoke("incrementScore");

        PlayerPrefs.SetInt("score", scoreValue);
        PlayerPrefs.SetInt("diamondscore", diamondValue);

        if (PlayerPrefs.HasKey("highScore"))
        {
            if (scoreValue > PlayerPrefs.GetInt("highScore"))
            {
                PlayerPrefs.SetInt("highScore", diamondValue);
            }
        }
        else
        {
            PlayerPrefs.SetInt("highScore", diamondValue);
        }
    }
}
