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

                GameManager.instance.startGame();
            }

        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        Debug.DrawRay(transform.position, Vector3.up, Color.red);


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

    public void fallDown()
    {
        gameOver = true;
        Camera.main.GetComponent<CameraFollow>().gameOver = true;
        GameManager.instance.gameOver();

    }
}
