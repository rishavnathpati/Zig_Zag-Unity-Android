using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        JasperMovement.gameOverIs = false;
    }

    public void StartGame()
    {
        UIManager.instance.GameStart();
        ScoreManager.instance.StartScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().StartSpawningPlatforms();
    }

    public void GameOver()
    {
        UIManager.instance.GameOver();
        ScoreManager.instance.StopScore();
        JasperMovement.gameOverIs = true;
        Debug.Log("Game Over");
    }
}
