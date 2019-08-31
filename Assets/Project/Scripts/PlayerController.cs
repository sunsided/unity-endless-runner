using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        public Texture aliveIcon;
        public Texture deadIcon;
        public RawImage[] icons;

        public GameObject gameOverPanel;

        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsMagic = Animator.StringToHash("isMagic");
        private static readonly int IsDead = Animator.StringToHash("isDead");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");

        private Animator _anim;
        private Rigidbody _rb;
        private Rigidbody _magicRb;
        private bool _canTurn;
        private Vector3 _startPosition;
        private int _livesLeft;
        private bool _falling;

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

            Dead = false;
            _livesLeft = PlayerPrefs.GetInt(PlayerPrefKeys.Lives);

            UpdateLivesLeftUI();
        }

        // ReSharper disable once InconsistentNaming
        private void UpdateLivesLeftUI()
        {
            for (var i = 0; i < icons.Length; ++i)
            {
                if (i >= _livesLeft)
                {
                    icons[i].texture = deadIcon;
                }
            }
        }

        private void OnCollisionEnter([CanBeNull] Collision other)
        {
            var isDangerousCollision = other != null && (other.gameObject.CompareTag("Fire") ||
                                                         other.gameObject.CompareTag("Wall") ||
                                                         other.gameObject.CompareTag("OuterSpace"));
            var isLifeThreat = _falling || isDangerousCollision;
            if (isLifeThreat && !Dead)
            {
                _anim.SetTrigger(_falling ? IsFalling : IsDead);

                Dead = true;
                GameData.Singleton.SoundDying.Play();

                --_livesLeft;
                PlayerPrefs.SetInt(PlayerPrefKeys.Lives, _livesLeft);
                UpdateLivesLeftUI();

                if (_livesLeft > 0)
                {
                    Invoke(nameof(RestartGame), 2);
                }
                else
                {
                    gameOverPanel.SetActive(true);
                }
            }
            else
            {
                if (other != null) CurrentPlatform = other.gameObject;
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

            if (CurrentPlatform != null)
            {
                // Make the player appear falling immediately.
                const float shortFallingDistance = 2f;
                if (transform.position.y < CurrentPlatform.transform.position.y - shortFallingDistance)
                {
                    _anim.SetTrigger(IsFalling);
                }

                const float fallingDistance = 10f;
                if (transform.position.y < CurrentPlatform.transform.position.y - fallingDistance)
                {
                    _falling = true;

                    // Trigger the restarting and everything.
                    OnCollisionEnter(null);
                    return;
                }
            }

            var rotateDown = Input.GetButtonDown("Rotate");
            var rotate = Input.GetAxisRaw("Rotate") * (rotateDown ? 1 : 0);

            var shiftDown = Input.GetButtonDown("Horizontal");
            var shift = Input.GetAxisRaw("Horizontal") * (shiftDown ? 1 : 0);

            var delayedDummySpawn = false;
            if (Input.GetButtonDown("Jump"))
            {
                _anim.SetBool(IsJumping, true);
                GameData.Singleton.SoundJump.Play();
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                _anim.SetBool(IsMagic, true);
            }
            else if (rotate > 0 && _canTurn)
            {
                transform.Rotate(Vector3.up * 90);
                GameData.Singleton.SoundWhoosh.Play();
                delayedDummySpawn = true;
            }
            else if (rotate < 0 && _canTurn)
            {
                transform.Rotate(Vector3.up * -90);
                GameData.Singleton.SoundWhoosh.Play();
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

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        [UsedImplicitly]
        private void PreCastMagic()
        {
            GameData.Singleton.SoundCastMagic.Play();
        }

        [UsedImplicitly]
        private void CastMagic()
        {
            magic.transform.position = magicStartPosition.position;
            magic.SetActive(true);
            _magicRb.AddForce(transform.forward * 20, ForceMode.Impulse);
            Invoke(nameof(KillMagic), 1);
        }

        [UsedImplicitly]
        private void FootStepLeft() => GameData.Singleton.SoundFootstep1.Play();

        [UsedImplicitly]
        private void FootStepRight() => GameData.Singleton.SoundFootstep2.Play();

        private void KillMagic()
        {
            magic.SetActive(false);

            // Reset spell forces.
            _magicRb.velocity = Vector3.zero;
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
