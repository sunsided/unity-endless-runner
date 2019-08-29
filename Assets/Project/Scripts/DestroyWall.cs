using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class DestroyWall : MonoBehaviour
    {
        public GameObject[] bricks;
        private readonly List<Rigidbody> _bricksRbs = new List<Rigidbody>();
        private readonly List<Vector3> _bricksPositions = new List<Vector3>();
        private readonly List<Quaternion> _bricksRolations = new List<Quaternion>();
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            foreach (var brick in bricks)
            {
                _bricksRbs.Add(brick.GetComponent<Rigidbody>());
                _bricksPositions.Add(brick.transform.localPosition);
                _bricksRolations.Add(brick.transform.rotation);
            }
        }

        private void OnEnable()
        {
            // Re-enable dying collision.
            _collider.enabled = true;

            // Reset all the bricks.
            for (var index = 0; index < bricks.Length; index++)
            {
                var brick = bricks[index];
                _bricksRbs[index].isKinematic = true;
                brick.transform.localPosition = _bricksPositions[index];
                brick.transform.rotation = _bricksRolations[index];
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
