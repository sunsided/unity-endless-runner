using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateWorld : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    private GameObject _dummyTraveller;

    void Start()
    {
        _dummyTraveller = new GameObject("dummy");

        for (var i = 0; i < 20; ++i)
        {
            var platformNumber = Random.Range(0, platformPrefabs.Length);
            var go = Instantiate(platformPrefabs[platformNumber],
                _dummyTraveller.transform.position,
                _dummyTraveller.transform.rotation);

            if (go.CompareTag("stairsUp"))
            {
                _dummyTraveller.transform.Translate(0, 5, 0);
            }
            else if (go.CompareTag("stairsDown"))
            {
                _dummyTraveller.transform.Translate(0, -5, 0);
                go.transform.position = _dummyTraveller.transform.position;
                go.transform.Rotate(new Vector3(0, 180, 0));
            }
            else if (go.CompareTag("platformTSection"))
            {
                var sign = Mathf.Sign(Random.Range(0, 2) - 0.5f);
                _dummyTraveller.transform.Rotate(new Vector3(0, sign * 90, 0));

                // T-sections are a bit longer than other platforms, so we need an extra translation here.
                _dummyTraveller.transform.Translate(Vector3.forward * -10);
            }

            _dummyTraveller.transform.Translate(Vector3.forward * -10);
        }
    }
}
