    using UnityEngine;

namespace Project.Scripts
{
    public class CoinPickup : MonoBehaviour
    {
        public int value = 10;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            Debug.Log("Coin picked up.");

            GameData.Singleton.AddScore(value);
            Destroy(gameObject, 0.5f);
        }
    }
}
