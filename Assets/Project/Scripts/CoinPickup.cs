using UnityEngine;

namespace Project.Scripts
{
    public class CoinPickup : MonoBehaviour
    {
        public int value = 10;

        private MeshRenderer[] _renderers;

        private void Start()
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
        }

        private void OnEnable()
        {
            if (_renderers == null) return;
            for (var i = 0; i < _renderers.Length; ++i)
            {
                _renderers[i].enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            Debug.Log("Coin picked up.");

            GameData.Singleton.AddScore(value);

            // Instead of destroying the coin, just hide it.
            for (var i = 0; i < _renderers.Length; ++i)
            {
                _renderers[i].enabled = false;
            }
        }
    }
}
