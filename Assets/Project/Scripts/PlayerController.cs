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
        private Animator _anim;

        private void Awake()
        {
            Player = gameObject;
        }

        private void Start()
        {
            _anim = GetComponent<Animator>();
            GenerateWorld.RunDummy();
        }

        private void OnCollisionEnter([NotNull] Collision other)
        {
            CurrentPlatform = other.gameObject;
        }

        private void OnTriggerEnter([NotNull] Collider other)
        {
            GenerateWorld.RunDummy();
        }

        private void Update()
        {
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
            else if (rotate > 0)
            {
                transform.Rotate(Vector3.up * 90);
            }
            else if (rotate < 0)
            {
                transform.Rotate(Vector3.up * -90);
            }
            else if (shift > 0)
            {
                transform.Translate(0.5f, 0, 0);
            }
            else if (shift < 0)
            {
                transform.Translate(-0.5f, 0, 0);
            }
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
