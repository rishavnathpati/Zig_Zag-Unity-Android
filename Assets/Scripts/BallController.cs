using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public GameObject particle;
    bool started;
    public static bool gameOver;
    public static BallController instance;
    public AudioSource collectDiamond;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0, 0);
                started = true;

                GameManager.instance.startGame();
            }

        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            rb.velocity = new Vector3(0, -20f, 0);

            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            GameManager.instance.gameOver();
        }

        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }

    void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0, 0);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0, 0, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamond"))
        {
            GameObject parti = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);
            Destroy(parti, 2f);
            collectDiamond.Play();
        }
    }

    public void speedUp()
    {
        speed++;
        Debug.Log("Current speed is: " + speed);
    }
}
