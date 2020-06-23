using UnityEngine;

public class JasperMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public GameObject particle;
    private bool started;
    public static bool gameOverIs;
    public static JasperMovement instance;
    public AudioSource collectDiamond;
    public Animator animator;
    private Rigidbody rbJasper;
    private Vector3 playerPos;

    private void Awake()
    {
        rbJasper = GetComponent<Rigidbody>();

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        started = false;
        gameOverIs = false;
        animator = GetComponent<Animator>();
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
        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (!Physics.Raycast(transform.position, Vector3.down, 0.8f))
        {
            Invoke("FallDown", 1f);
        }

        if (Input.GetMouseButtonDown(0) && !gameOverIs)
        {
            playerPos = SwitchDirection();
        }
    }

    private Vector3 SwitchDirection()
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

        return transform.position;
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
        gameOverIs = true;
        Camera.main.GetComponent<CameraFollow>().gameOver = true;
        GameManager.instance.GameOver();
    }
}