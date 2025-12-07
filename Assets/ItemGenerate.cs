using UnityEngine;

public class ItemGenerate : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefab;
    [SerializeField] Vector2 spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private LayerMask OverLapLayer;

    private float timer;

    private Vector3 randomPos;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnItem();
            timer = spawnInterval;
            spawnInterval *= 1.01f;
        }
    }

    private void SpawnItem()
    {
        Vector3 center = transform.position;

        randomPos.x = Random.Range(center.x - spawnPoints.x / 2, center.x + spawnPoints.x / 2);
        randomPos.y = Random.Range(center.y - spawnPoints.y / 2, center.y + spawnPoints.y / 2);
        GameObject item = Instantiate(itemPrefab[Random.Range(0,itemPrefab.Length)], randomPos, Quaternion.identity);

        bool overlap = Physics2D.OverlapCircle(item.transform.position, 1f, OverLapLayer);

        while (overlap)
        {
            Destroy(item);
            randomPos.x = Random.Range(center.x - spawnPoints.x / 2, center.x + spawnPoints.x / 2);
             randomPos.y = Random.Range(center.y - spawnPoints.y / 2, center.y + spawnPoints.y / 2);
            item = Instantiate(itemPrefab[Random.Range(0,itemPrefab.Length)], randomPos, Quaternion.identity);
            overlap = Physics2D.OverlapCircle(item.transform.position, 1f, OverLapLayer);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnPoints);
    }
}