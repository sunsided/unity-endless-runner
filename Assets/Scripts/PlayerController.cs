using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsMagic = Animator.StringToHash("isMagic");
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Jump") > 0)
        {
            _anim.SetBool(IsJumping, true);
        }

        if (Input.GetAxisRaw("Fire1") > 0)
        {
            _anim.SetBool(IsMagic, true);
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
