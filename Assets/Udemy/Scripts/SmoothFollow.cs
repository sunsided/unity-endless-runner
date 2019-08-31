using Project.Scripts;
using UnityEngine;

namespace Udemy.Scripts
{
    /// <summary>
    /// Apparently an old Unity asset that makes e.g. a camera follow the target smoothly.
    /// </summary>
    /// <remarks>
    /// Some nice benefits here are the delayed turning around of the camera after the player
    /// changes directions and the "looking down" at the player falling off into space.
    /// </remarks>
    public class SmoothFollow : MonoBehaviour
    {
        public Transform target;

        private readonly float _distance = 3.0f;
        private readonly float _height = 0.7f;
        private readonly float _heightOffset = 0.0f;
        private readonly float _heightDamping = 5.0f;
        private readonly float _rotationDamping = 3.0f;

        void LateUpdate()
        {
            if (target == null) return;

            var position = transform.position;
            var targetPosition = target.position;

            if (!PlayerController.Dead)
            {
                var wantedRotationAngle = target.eulerAngles.y;
                var wantedHeight = targetPosition.y + _height;

                var currentRotationAngle = transform.eulerAngles.y;
                var currentHeight = position.y;

                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

                var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                position = targetPosition;

                var distance = Vector3.forward * _distance;
                position -= currentRotation * distance;

                transform.position = new Vector3(position.x, currentHeight + _heightOffset, position.z);
            }

            transform.LookAt(target);
        }
    }
}
