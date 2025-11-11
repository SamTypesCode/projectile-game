using UnityEngine;

namespace SkyBackgroundsPixelArt2
{
    public class ParallaxEffect : MonoBehaviour
    {
        private Transform mainCamera;

        [Header("Parallax Settings")]
        public float parallaxIntensityX = 0.5f;
        public float parallaxIntensityY = 0.5f;
        public float independantSpeed = 1f;

        private float cameraSize;
        private float spriteWidth;
        private Vector2 initialPos;
        private float translationOffset = 0;

        private void Start()
        {
            mainCamera = Camera.main.transform;
            cameraSize = Camera.main.orthographicSize;
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 3;

            initialPos = transform.position;
        }

        private void LateUpdate()
        {
            translationOffset += independantSpeed * Time.deltaTime * parallaxIntensityX;

            float parallaxOffsetX = (mainCamera.position.x * (1 - (parallaxIntensityX / 2))) + translationOffset;
            float parallaxOffsetY = initialPos.y;

            transform.position = new Vector2(initialPos.x + parallaxOffsetX, parallaxOffsetY);

            float cameraOffsetX = mainCamera.position.x - transform.position.x;
            if (cameraOffsetX > spriteWidth / 2)
                initialPos.x += spriteWidth;
            else if (cameraOffsetX < -spriteWidth / 2)
                initialPos.x -= spriteWidth;
        }
    }
}
