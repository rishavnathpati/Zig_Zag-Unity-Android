using UnityEngine;

public class JasperMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public GameObject particle;
    bool started;
    public static bool gameOver;
    public static JasperMovement instance;
    public AudioSource collectDiamond, fallingDown;
    public Animator animator;

    Rigidbody rbJasper;

    private void Awake()
    {
        rbJasper = GetComponent<Rigidbody>();

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rbJasper.velocity = new Vector3(speed, 0, 0);
                started = true;
                animator.SetInteger("states", 1);

                GameManager.instance.startGame();
            }

        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (!Physics.Raycast(transform.position, Vector3.down, 0.8f))
        {
            rbJasper.velocity = new Vector3(0, -10f, 0);
            //fallingDown.Play();
            Invoke("fallDown", 1f);
        }

        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }

    void SwitchDirection()
    {
        if (rbJasper.velocity.z > 0)
        {
            rbJasper.velocity = new Vector3(speed, 0, 0);
            transform.SetPositionAndRotation(transform.position,Quaternion.Euler(0,90,0));
        }
        else if (rbJasper.velocity.x > 0)
        {
            rbJasper.velocity = new Vector3(0, 0, speed);
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("diamond"))
        {
            animator.SetInteger("states", 2);
            GameObject parti = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);
            Destroy(parti, 2f);
            collectDiamond.Play();
            Invoke("getUp", 0.2f);
            ScoreManager.instance.incrementDiamondScore();
        }
    }

    public void getUp()
    {
        animator.SetInteger("states", 3);
    }

    public void speedUp()
    {
        speed++;
        Debug.Log("Current speed is: " + speed);
    }

    public void fallDown()
    {
        gameOver = true;
        Camera.main.GetComponent<CameraFollow>().gameOver = true;
        GameManager.instance.gameOver();

    }
}
