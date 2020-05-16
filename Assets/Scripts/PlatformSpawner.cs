using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject diamond;
    public GameObject clouds;
    public Vector3 lastPos;
    float size;
    public bool gameOver;

    public static PlatformSpawner instance;


    // Start is called before the first frame update
    void Start()
    {
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;

        for (int i = 1; i < 20; i++)
        {
            spawnPlatform();
        }

    }

    public void startSpawningPlatforms()
    {
        InvokeRepeating("spawnPlatform", 1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GameOver == true)
        {
            CancelInvoke("spawnPlatform");
        }
    }

    public void spawnPlatform()
    {

        int rand = Random.Range(0, 6);
        if (rand < 3)
        {
            SpawnX();
        }
        else if (rand >= 3)
        {
            SpawnZ();
        }
    }

    public void SpawnX()
    {
        Vector3 pos = lastPos;
        pos.x += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);
        int rand = Random.Range(0, 6);
        if (rand == 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 1.2f, pos.z), diamond.transform.rotation);
        }
        rand = Random.Range(0, 10);
        if (rand == 2)
            Instantiate(clouds, new Vector3(pos.x, pos.y + 10, pos.z), Quaternion.identity);
    }

    public void SpawnZ()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);
        int rand = Random.Range(0, 6);
        if (rand == 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 1.2f, pos.z), diamond.transform.rotation);
        }
        rand = Random.Range(0, 10);
        if (rand == 3)
            Instantiate(clouds, new Vector3(pos.x, pos.y - 10, pos.z - 20), Quaternion.identity);
    }
}
