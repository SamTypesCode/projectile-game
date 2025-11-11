using UnityEngine;

namespace SkyBackgroundsPixelArt2
{
    public class MainCamera : MonoBehaviour
    {
        public bool smoothCamera = true;
        public bool lockVerticalAxis = false;
        public bool lockCameraSize = false;
        public float cameraSize = 5f;
        public Vector3 independentPositionOffset = Vector3.zero;

        private void Update()
        {
            Camera.main.orthographicSize = lockCameraSize ? 5f : cameraSize;

            float smoothSpeed = 5f;

            Vector3 desiredPosition = transform.position + independentPositionOffset;

            if (lockVerticalAxis)
                desiredPosition.y = transform.position.y;

            transform.position = smoothCamera
                ? Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime)
                : desiredPosition;
        }
    }
}
