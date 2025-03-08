using UnityEngine;

namespace PetesPlatformer
{
    public class BackgroundController : MonoBehaviour
    {
        public float startPos;
        public float length;
        public float parallaxEffect;
        public GameObject cam;

        private void Start()
        {
            cam = Camera.main.gameObject;
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            float distance = cam.transform.position.x * parallaxEffect;
            float movement = cam.transform.position.x * (1 - parallaxEffect);

            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

            if (movement > startPos + (length / 2))
            {
                startPos += length;
            }
            else if (movement < startPos - (length / 2))
            {
                startPos -= length;
            }
        }
    }
}
