using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class CoinPickup : MonoBehaviour
    {
        [Range(0, 100)]
        public float highValueProbability = 10;

        public int value = 10;
        public int highValue = 50;

        public Material regularMaterial;
        public Material highValueMaterial;
        public GameObject scorePrefab;
        public GameObject particlesPrefab;

        private bool _isBigCoin;
        private int _score;
        private Canvas _canvas;
        private MeshRenderer[] _renderers;

        private void Start()
        {
            _canvas = FindObjectOfType<Canvas>();
            _renderers = GetComponentsInChildren<MeshRenderer>();

            RandomizeScore();
        }

        private void OnEnable()
        {
            RandomizeScore();

            if (_renderers == null) return;
            for (var i = 0; i < _renderers.Length; ++i)
            {
                _renderers[i].enabled = true;
            }
        }

        private void OnTriggerEnter([NotNull] Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            var position = transform.position;

            GameData.Singleton.SoundPickup.Play();
            GameData.Singleton.AddScore(_score);

            // Add particles.
            var pe = Instantiate(particlesPrefab, position, Quaternion.identity, transform);
            Destroy(pe, 2.5f); // TODO: Destroy after particles are finished

            // Show the score text float.
            // TODO: The score doesn't seem to be positioned exactly over the coin.
            var scoreText = Instantiate(scorePrefab, _canvas.transform, true);
            scoreText.GetComponent<Text>().text = _score.ToString();

            Debug.Assert(Camera.main != null, "Camera.main != null");
            var screenPoint = Camera.main.WorldToScreenPoint(position);
            scoreText.transform.position = screenPoint;

            // Instead of destroying the coin, just hide it.
            for (var i = 0; i < _renderers.Length; ++i)
            {
                _renderers[i].enabled = false;
            }
        }

        private void RandomizeScore()
        {
            _isBigCoin = Random.Range(1, 101) <= highValueProbability;
            _score = _isBigCoin ? highValue : value;
            var material = _isBigCoin ? highValueMaterial : regularMaterial;

            if (_renderers == null) return;
            for (var i = 0; i < _renderers.Length; ++i)
            {
                _renderers[i].enabled = true;
                _renderers[i].material = material;
            }
        }
    }
}
