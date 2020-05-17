using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
        UIManager.instance.gameStart();
        ScoreManager.instance.startScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().startSpawningPlatforms();

    }

    public void gameOver()
    {

        UIManager.instance.gameOver();
        ScoreManager.instance.stopScore();
        GameOver = true;
    }
}
