using UnityEngine;
using System.Collections.Generic;

namespace PetesPlatformer
{
    public class BackgroundScroll : MonoBehaviour
    {
        public struct BackgroundData
        {
            public Transform transform;
            public Vector2 position;
            public float length;

            public BackgroundData(Transform transform, Vector2 position, float length)
            {
                this.transform = transform;
                this.position = position;
                this.length = length;
            }
        }

        //private float m_Length;
        [SerializeField] private List<Transform> m_AllBackgrounds;
        private Dictionary<Transform, BackgroundData> m_Backgrounds = new();
        [SerializeField] private Transform m_Camera;
        [SerializeField] private float m_spriteBoundsMultiplier;


        private void Start()
        {
            m_Camera = Camera.main.transform;
            //m_Length = m_AllBackgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x * 1.5f;

            foreach (Transform t in m_AllBackgrounds)
            {
                var data = new BackgroundData(t, t.position, m_AllBackgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x * m_spriteBoundsMultiplier);
                m_Backgrounds.Add(t, data);
            }
        }

        private void OnValidate()
        {
            var updatedBackgrounds = new Dictionary<Transform, BackgroundData>();
            foreach (var kvp in m_Backgrounds)
            {
                var data = kvp.Value;
                data.length = data.transform.GetComponent<SpriteRenderer>().bounds.size.x * m_spriteBoundsMultiplier;
                updatedBackgrounds.Add(kvp.Key, data);
            }
            m_Backgrounds = updatedBackgrounds;
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
                    Debug.Log("1");
                    Vector2 current = data.position;
                    current.x += (data.length * 2);
                    data.position = current;
                    m_Backgrounds[t] = data;
                }
                else if (temp < data.position.x - data.length)
                {
                    Debug.Log("2");
                    Vector2 current = data.position;
                    current.x -= (data.length * 2);
                    data.position = current;
                    m_Backgrounds[t] = data;
                }
            }
        }
    }
}
