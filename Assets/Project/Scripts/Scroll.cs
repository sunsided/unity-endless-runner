using UnityEngine;

namespace Project.Scripts
{
    public class Scroll : MonoBehaviour
    {
        private GameObject _player;

        private void Start()
        {
            _player = PlayerController.Player;
        }

        private void FixedUpdate()
        {
            if (PlayerController.Dead) return;

            const float speed = -0.1f;
            transform.position += _player.transform.forward * speed;

            var currentPlatform = PlayerController.CurrentPlatform;
            if (currentPlatform == null) return;

            const float stairSlope = 0.06f;
            if (currentPlatform.CompareTag("stairsUp"))
            {
                // Stairs are at a 60 degree angle.
                // For every one step forward, move the "world" 6 steps down.
                transform.Translate(0, -stairSlope, 0);
            }
            else if (currentPlatform.CompareTag("stairsDown"))
            {
                // Same logic as above, just in reverse.
                transform.Translate(0, stairSlope, 0);
            }
        }
    }
}
