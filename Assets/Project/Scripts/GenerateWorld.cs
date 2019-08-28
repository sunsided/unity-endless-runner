using UnityEngine;

namespace Project.Scripts
{
    public class GenerateWorld : MonoBehaviour
    {
        public static GameObject DummyTraveller;
        public static GameObject LastPlatform;

        public static void RunDummy()
        {
            var p = Pool.Singleton.GetRandomItem();
            if (p == null) return;

            var player = PlayerController.Player;
            if (LastPlatform != null)
            {
                DummyTraveller.transform.position = LastPlatform.transform.position +
                                                    player.transform.forward * 10;
            }

            LastPlatform = p;
            p.transform.position = DummyTraveller.transform.position;
            p.transform.rotation = DummyTraveller.transform.rotation;
            p.SetActive(true);
        }

        private void Awake()
        {
            DummyTraveller = new GameObject("dummy");
        }
    }
}
