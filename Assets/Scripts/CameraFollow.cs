using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    private Vector3 offset;
    public float lerpRate;
    public bool gameOver;

    // Start is called before the first frame update
    private void Start()
    {
        offset = ball.transform.position - transform.position;
        gameOver = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = ball.transform.position - offset;
        pos = Vector3.Lerp(pos, targetPos, lerpRate * Time.deltaTime);
        transform.position = pos;
    }
}
