using PetesPlatformer;
using UnityEngine;

public class Overworld : MonoBehaviour
{
    [SerializeField] private GameSave m_activeSave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("save slot: " + m_activeSave.m_saveSlot.ToString());
        Debug.Log("lives: " + m_activeSave.m_lives.ToString());
        Debug.Log("fruit: " + m_activeSave.m_fruit.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
