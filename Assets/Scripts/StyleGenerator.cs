using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyleGenerator : MonoBehaviour
{
    public GameObject[] stylePrefabs;
    private List<GameObject> activeStyles = new List<GameObject>();
    private float spawnPos = 0;
    private float styleLength = 100;

    [SerializeField] private Transform player;
    private int startStyles = 6;
    void Start()
    {
        for (int i = 0; i < startStyles;  i++)
        {
            SpawnStyle(Random.Range(0, stylePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 60 > spawnPos - (startStyles * styleLength))
        {
            SpawnStyle(Random.Range(0, stylePrefabs.Length));
            DeleteStyle();
        }
    }

    private void SpawnStyle(int styleIndex)
    {
        GameObject nextStyle = Instantiate(stylePrefabs[styleIndex], transform.forward * spawnPos, transform.rotation);
        activeStyles.Add(nextStyle);
        spawnPos += styleLength;
    }

    private void DeleteStyle()
    {
        Destroy(activeStyles[0]);
        activeStyles.RemoveAt(0);
    }
}
