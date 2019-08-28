using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static GameObject Player;

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
        }

        private void Update()
        {
            var horzDown = Input.GetButtonDown("Horizontal");
            var horz = Input.GetAxisRaw("Horizontal") * (horzDown ? 1 : 0);

            var vertDown = Input.GetButtonDown("Vertical");
            var vert = Input.GetAxisRaw("Vertical") * (vertDown ? 1 : 0);

            if (Input.GetAxisRaw("Jump") > 0)
            {
                _anim.SetBool(IsJumping, true);
            }
            else if (Input.GetAxisRaw("Fire1") > 0)
            {
                _anim.SetBool(IsMagic, true);
            }
            else if (horz > 0)
            {
                transform.Rotate(Vector3.up * 90);
            }
            else if (horz < 0)
            {
                transform.Rotate(Vector3.up * -90);
            }
            else if (vert > 0)
            {
                transform.Translate(0.1f, 0, 0);
            }
            else if (vert < 0)
            {
                transform.Translate(-0.1f, 0, 0);
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
