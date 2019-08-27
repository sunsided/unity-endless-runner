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
            var go = Instantiate(platformPrefabs[platformNumber], pos, Quaternion.identity);

            if (go.CompareTag("stairsUp")) pos.y += 5;
            else if (go.CompareTag("stairsDown"))
            {
                pos.y -= 5;
                go.transform.position = pos;
                go.transform.Rotate(new Vector3(0, 180, 0));
            }

            pos.z -= 10;
        }
    }
}
