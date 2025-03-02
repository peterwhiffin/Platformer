using UnityEngine;
using System.Collections.Generic;

namespace PetesPlatformer
{
    public class BackgroundScroll : MonoBehaviour
    {
        public struct BackgroundData
        {
            public Vector2 position;
            public float length;

            public BackgroundData(Vector2 position, float length)
            {
                this.position = position;
                this.length = length;
            }
        }

        //private float m_Length;
        [SerializeField] private List<Transform> m_AllBackgrounds;
        private Dictionary<Transform, BackgroundData> m_Backgrounds = new();
        [SerializeField] private Transform m_Camera;

        private void Start()
        {
            m_Camera = Camera.main.transform;
            //m_Length = m_AllBackgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x * 1.5f;

            foreach (Transform t in m_AllBackgrounds)
            {
                var data = new BackgroundData(t.position, m_AllBackgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x * 1.5f);
                m_Backgrounds.Add(t, data);
            }
        }

        private void Update()
        {
            foreach (var t in m_AllBackgrounds)
            {
                BackgroundData data = m_Backgrounds[t];
                float temp = (m_Camera.position.x * (1 - t.position.z));
                float distance = (m_Camera.position.x * t.position.z);

                t.position = new Vector3(data.position.x + distance, t.position.y, t.position.z);

                if (temp > data.position.x + data.length)
                {
                    Vector2 current = data.position;
                    current.x += (data.length * 2);
                    data.position = current;
                }
                else if (temp < data.position.x - data.length)
                {
                    Vector2 current = data.position;
                    current.x -= (data.length * 2);
                    data.position = current;
                }
            }
        }
    }
}
