using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject diamond;
    public GameObject clouds;
    public Vector3 lastPos;
    private float size;

    // Start is called before the first frame update
    private void Start()
    {
        lastPos = platform.transform.position;
        size = platform.transform.localScale.x;

        for (int i = 1; i < 20; i++)
        {
            SpawnPlatform();
        }
    }

    public void StartSpawningPlatforms()
    {
        InvokeRepeating("SpawnPlatform", 1f, 0.2f);
    }

    private void Update()
    {
        if (JasperMovement.gameOverIs == true)
        {
            CancelInvoke("SpawnPlatform");
        }
    }

    public void SpawnPlatform()
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

        if (Random.Range(0, 6) == 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 1.2f, pos.z), diamond.transform.rotation);
        }

        if (Random.Range(0, 10) == 2)
        {
            Instantiate(clouds, new Vector3(pos.x, pos.y + 10, pos.z), Quaternion.identity);
        }
    }

    public void SpawnZ()
    {
        Vector3 pos = lastPos;
        pos.z += size;
        lastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        if (Random.Range(0, 6) == 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 1.2f, pos.z), diamond.transform.rotation);
        }

        if (Random.Range(0, 10) == 2)
        {
            Instantiate(clouds, new Vector3(pos.x, pos.y - 10, pos.z - 20), Quaternion.identity);
        }
    }
}
