using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static GameObject Player;
        public static GameObject CurrentPlatform;

        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsMagic = Animator.StringToHash("isMagic");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private Animator _anim;
        private bool _canTurn;
        private Vector3 _startPosition;

        private void Awake()
        {
            Player = gameObject;
        }

        private void Start()
        {
            _startPosition = Player.transform.position;
            _anim = GetComponent<Animator>();
            GenerateWorld.RunDummy();
        }

        private void OnCollisionEnter([NotNull] Collision other)
        {
            if (other.gameObject.CompareTag("Fire"))
            {
                _anim.SetTrigger(IsDead);
            }
            else
            {
                CurrentPlatform = other.gameObject;
            }
        }

        private void OnTriggerEnter([NotNull] Collider other)
        {
            // Boxes mark spawning points.
            // We need to prevent spawning new instances in front of us when exiting into a T-section.
            // We're handling this special case in the movement (rotation) code.
            if (other is BoxCollider && !GenerateWorld.LastPlatform.CompareTag("platformTSection"))
            {
                GenerateWorld.RunDummy();
            }

            // Spheres mark turning points.
            if (other is SphereCollider)
            {
                _canTurn = true;
            }
        }

        private void OnTriggerExit([NotNull] Collider other)
        {
            // Spheres mark turning points.
            // TODO: This means that we can spin forever as long as we're inside the sphere collider. We should deactivate immediately in the Update() method.
            if (other is SphereCollider)
            {
                _canTurn = false;
            }
        }

        private void Update()
        {
            var delayedDummySpawn = false;

            var rotateDown = Input.GetButtonDown("Rotate");
            var rotate = Input.GetAxisRaw("Rotate") * (rotateDown ? 1 : 0);

            var shiftDown = Input.GetButtonDown("Horizontal");
            var shift = Input.GetAxisRaw("Horizontal") * (shiftDown ? 1 : 0);

            if (Input.GetAxisRaw("Jump") > 0)
            {
                _anim.SetBool(IsJumping, true);
            }
            else if (Input.GetAxisRaw("Fire1") > 0)
            {
                _anim.SetBool(IsMagic, true);
            }
            else if (rotate > 0 && _canTurn)
            {
                transform.Rotate(Vector3.up * 90);
                delayedDummySpawn = true;
            }
            else if (rotate < 0 && _canTurn)
            {
                transform.Rotate(Vector3.up * -90);
                delayedDummySpawn = true;
            }
            else if (shift > 0)
            {
                transform.Translate(0.5f, 0, 0);
            }
            else if (shift < 0)
            {
                transform.Translate(-0.5f, 0, 0);
            }

            if (!delayedDummySpawn) return;
            var tf = transform;
            GenerateWorld.DummyTraveller.transform.forward = -tf.forward;

            GenerateWorld.RunDummy();

            // Build more platforms into the future, unless we just generated a T-section
            if (!GenerateWorld.LastPlatform.CompareTag("platformTSection"))
            {
                GenerateWorld.RunDummy();
            }

            transform.position = new Vector3(_startPosition.x, tf.position.y, _startPosition.z);
        }

        [UsedImplicitly]
        private void StopJump()
        {
            _anim.SetBool(IsJumping, false);
        }

        [UsedImplicitly]
        private void StopMagic()
        {
            _anim.SetBool(IsMagic, false);
        }
    }
}
