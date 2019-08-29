using UnityEngine;

namespace Project.Scripts
{
    public class GameData : MonoBehaviour
    {
        public static GameData singleton;

        public int score;

        private void Awake()
        {
            // If this scene creates a new version of the game object, destroy it.
            // This solves the problem of creating a new instance of the game object
            // whenever we enter a scene that contains a reference to it.
            var gd = GameObject.FindGameObjectsWithTag("GameData");
            if (gd.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
    }
}
