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
        var horzDown = Input.GetButtonDown("Horizontal");
        var horz = Input.GetAxisRaw("Horizontal") * (horzDown ? 1 : 0);

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
