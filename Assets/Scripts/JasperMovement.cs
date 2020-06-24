using UnityEngine;

public class JasperMovement : MonoBehaviour
{
    public float speed;
    private bool startRaycast;
    public static bool gameOverIs;
    public static JasperMovement instance;
    public AudioSource collectDiamond;
    public Animator animator;
    public GameObject PlatformStart;
    public GameObject particle;

    private bool started;
    private Rigidbody rbJasper;

    private void Awake()
    {
        rbJasper = GetComponent<Rigidbody>();

        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        started = false;
        gameOverIs = false;
        startRaycast = true;
        animator = GetComponent<Animator>();
    }

    private void StartRaycast()
    {
        startRaycast = true;
    }

    public void StartGame()
    {
        if (!started)
        {
            rbJasper.velocity = new Vector3(0, 0, speed);
            started = true;
            animator.SetInteger("states", 1);
            GameManager.instance.StartGame();
        }
    }

    private void Update()
    {
        if (startRaycast && !gameOverIs)
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);

            if (!Physics.Raycast(transform.position, Vector3.down, 0.8f))
            {
                gameOverIs = true;
                rbJasper.velocity = new Vector3(0, -10f, 0);
                Debug.Log("Invoked Falldown");
                Invoke("RespawnPlayer", 2f);
            }
        }

        if (Input.GetMouseButtonDown(0) && !gameOverIs)
        {
            SwitchDirection();
        }
    }

    private void SwitchDirection()
    {
        if (rbJasper.velocity.z > 0)
        {
            rbJasper.velocity = new Vector3(speed, 0, 0);
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 90, 0));
        }
        else if (rbJasper.velocity.x > 0)
        {
            rbJasper.velocity = new Vector3(0, 0, speed);
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    public void RespawnPlayer()
    {
        started = false;
        gameOverIs = false;
        startRaycast = false;
        Invoke("StartRaycast", 2f);
        StartGame();
        transform.position = new Vector3(transform.position.x, 1.11f, transform.position.z);
        Instantiate(PlatformStart, new Vector3(transform.position.x, 0.55f, transform.position.z), Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamond"))
        {
            animator.SetInteger("states", 2);
            GameObject parti = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(parti, 2f);
            collectDiamond.Play();
            ScoreManager.instance.IncrementDiamondScore();
        }
    }

    public void SpeedUp()
    {
        speed++;
        Debug.Log("Current speed is: " + speed);
    }

    public void FallDown()
    {
        Debug.Log("Fall down reached, calling Game over");
        gameOverIs = true;
        Camera.main.GetComponent<CameraFollow>().gameOver = true;
        GameManager.instance.GameOver();
    }
}