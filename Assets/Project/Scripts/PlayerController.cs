using JetBrains.Annotations;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public static GameObject Player;
        public static GameObject CurrentPlatform;
        public static bool Dead;

        public float jumpForce = 5;
        public GameObject magic;
        public Transform magicStartPosition;

        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsMagic = Animator.StringToHash("isMagic");
        private static readonly int IsDead = Animator.StringToHash("isDead");

        private Animator _anim;
        private Rigidbody _rb;
        private Rigidbody _magicRb;
        private bool _canTurn;
        private Vector3 _startPosition;

        private void Awake()
        {
            Player = gameObject;
            _anim = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _startPosition = Player.transform.position;
            _magicRb = magic.GetComponent<Rigidbody>();

            GenerateWorld.RunDummy();
        }

        private void OnCollisionEnter([NotNull] Collision other)
        {
            if (other.gameObject.CompareTag("Fire") || other.gameObject.CompareTag("Wall"))
            {
                _anim.SetTrigger(IsDead);
                Dead = true;
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
            if (Dead) return;
            var delayedDummySpawn = false;

            var rotateDown = Input.GetButtonDown("Rotate");
            var rotate = Input.GetAxisRaw("Rotate") * (rotateDown ? 1 : 0);

            var shiftDown = Input.GetButtonDown("Horizontal");
            var shift = Input.GetAxisRaw("Horizontal") * (shiftDown ? 1 : 0);

            if (Input.GetButtonDown("Jump"))
            {
                _anim.SetBool(IsJumping, true);
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (Input.GetButtonDown("Fire1"))
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

        private void CastMagic()
        {
            magic.transform.position = magicStartPosition.position;
            magic.SetActive(true);
            _magicRb.AddForce(transform.forward * 20, ForceMode.Impulse);
            Invoke(nameof(KillMagic), 1);
        }

        private void KillMagic()
        {
            magic.SetActive(false);
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
