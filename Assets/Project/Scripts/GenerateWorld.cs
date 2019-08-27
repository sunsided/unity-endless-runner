using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject[] platformPrefabs;

    void Start()
    {
        var pos = Vector3.zero;
        for (var i = 0; i < 20; ++i)
        {
            var platformNumber = Random.Range(0, platformPrefabs.Length);
            Instantiate(platformPrefabs[platformNumber], pos, Quaternion.identity);
            pos.z += 10;
        }
    }
}
