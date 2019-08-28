using JetBrains.Annotations;
using UnityEngine;

namespace Project.Scripts
{
    public class Deactivate : MonoBehaviour
    {
        private bool _deactivationScheduled;

        private void OnCollisionExit([NotNull] Collision other)
        {
            if (PlayerController.Dead) return;
            if (!other.gameObject.CompareTag("Player") || _deactivationScheduled) return;

            // Make sure that the element is behind the camera enough
            // to not see it de-spawning.
            // TODO: The T-section is rather long - we need to ensure we actually left it before de-spawning.
            _deactivationScheduled = true;
            Invoke(nameof(SetInactive), 4.0f);
        }

        private void SetInactive()
        {
            gameObject.SetActive(false);
            _deactivationScheduled = false;
        }
    }
}
