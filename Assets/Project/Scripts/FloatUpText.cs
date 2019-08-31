using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class FloatUpText : MonoBehaviour
    {
        public float floatSpeed = 100;
        public float fadeSpeed = 5f;

        private Text _text;
        private float _alpha;

        private void Awake()
        {
            _text = GetComponent<Text>();
            _text.color = Color.white;
        }

        private void OnEnable()
        {
            _alpha = 1;
        }

        private void Update()
        {
            // TODO: We may want to use the time delta in here ...
            transform.Translate(0, floatSpeed * Time.deltaTime, 0);

            _alpha -= fadeSpeed * Time.deltaTime;
            if (_alpha <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            var color = _text.color;
            _text.color = new Color(color.r, color.g, color.b, _alpha);
        }
    }
}
