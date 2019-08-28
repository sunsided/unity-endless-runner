using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class DestroyWall : MonoBehaviour
    {
        public GameObject[] bricks;
        private readonly List<Rigidbody> _bricksRbs = new List<Rigidbody>();
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            foreach (var brick in bricks)
            {
                _bricksRbs.Add(brick.GetComponent<Rigidbody>());
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Spell")) return;

            // By turning off the dummy wall's collider, we're not really hitting it,
            // effectively disabling the possibility to die.
            _collider.enabled = false;

            // Explode the bricks into all directions. Since they're overlapped, they
            // should blast apart.
            foreach (var rb in _bricksRbs)
            {
                rb.isKinematic = false;
            }
        }
    }
}
