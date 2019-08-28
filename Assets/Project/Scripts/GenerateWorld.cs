using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts
{
    public class GenerateWorld : MonoBehaviour
    {
        public GameObject[] platformPrefabs;
        private GameObject _dummyTraveller;

        void Start()
        {
            _dummyTraveller = new GameObject("dummy");
            var pool = Pool.Singleton;

            for (var i = 0; i < 20; ++i)
            {
                var p = pool.GetRandomItem();
                if (p == null) return;

                p.transform.position = _dummyTraveller.transform.position;
                p.transform.rotation = _dummyTraveller.transform.rotation;
                p.SetActive(true);

                if (p.CompareTag("stairsUp"))
                {
                    _dummyTraveller.transform.Translate(0, 5, 0);
                }
                else if (p.CompareTag("stairsDown"))
                {
                    _dummyTraveller.transform.Translate(0, -5, 0);
                    p.transform.position = _dummyTraveller.transform.position;
                    p.transform.Rotate(new Vector3(0, 180, 0));
                }
                else if (p.CompareTag("platformTSection"))
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
}
