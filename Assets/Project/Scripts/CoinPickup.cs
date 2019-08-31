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

            GameData.Singleton.SoundPickup.Play();
            GameData.Singleton.AddScore(_score);

            // Show the score text float.
            var scoreText = Instantiate(scorePrefab, _canvas.transform);
            scoreText.GetComponent<Text>().text = _score.ToString();

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
