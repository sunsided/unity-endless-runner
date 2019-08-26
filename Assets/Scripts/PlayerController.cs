using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
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
    }

    private void StopJump()
    {
        _anim.SetBool(IsJumping, false);
    }
}
