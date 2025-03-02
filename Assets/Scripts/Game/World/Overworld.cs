using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil.Cil;
using PetesPlatformer;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PetesPlatformer
{
    public class Overworld : MonoBehaviour
    {
        private OverworldNode m_currentNode;
        private Coroutine m_transitRoutine;
        private bool m_isPaused = false;

        [SerializeField] private SceneRoot m_sceneRoot;
        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private List<OverworldNode> m_nodes;
        [SerializeField] private GameObject m_playerIndicator;
        [SerializeField] private Transform m_cameraPosition;
        [SerializeField] private float m_transitSpeed;

        void Start()
        {
            foreach (OverworldNode node in m_nodes)
            {
                if (m_activeSave.m_unlockedLevels.Contains(node.LevelInfo.ID))
                {
                    node.Initialize(true);
                    if (m_activeSave.m_currentLevel == node.LevelInfo.ID)
                    {
                        m_playerIndicator.transform.position = node.transform.position;
                        m_currentNode = node;
                    }
                }
            }

            Camera.main.transform.position = m_cameraPosition.position;
            SceneRoot.GamePaused += OnGamePaused;
            SubscribeToInput();
        }

        private void OnDestroy()
        {
            SceneRoot.GamePaused -= OnGamePaused;
            UnsubscribeFromInput();
        }

        private void SubscribeToInput()
        {
            InputReader.MoveInput += OnMoveInput;
            InputReader.JumpActivated += OnJumpInput;           
        }

        private void UnsubscribeFromInput()
        {
            InputReader.MoveInput -= OnMoveInput;
            InputReader.JumpActivated -= OnJumpInput;          
        }

        private void OnGamePaused(bool isPaused)
        {
            m_isPaused = isPaused;

            if (m_isPaused)
            {
                UnsubscribeFromInput();
            }
            else
            {
                SubscribeToInput();
            }
        }

        //This is bad
        private void OnMoveInput(Vector2 direction)
        {          
            if (m_transitRoutine != null || direction == Vector2.zero)
            {
                return;
            }

            int finalDirection = 0;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                finalDirection = (int)(Mathf.Sign(direction.x) * 2);
            }
            else if(direction.y != 0)
            {
                finalDirection = (int)Mathf.Sign(direction.y);
            }

            CheckDirection(finalDirection);
        }

        private void CheckDirection(int direction)
        {
            foreach (var connection in m_currentNode.Connections)
            {
                if ((int)connection.Direction == direction && connection.Node.IsUnlocked)
                {
                    m_transitRoutine = StartCoroutine(MoveToNode(connection));
                    break;
                }
            }
        }

        private IEnumerator MoveToNode(OverworldConnection connection)
        {
            m_currentNode = null;

            int currentTargetIndex = 0;

            while (currentTargetIndex < connection.AnimationPath.Count)
            {
                Vector3 currentPosition = m_playerIndicator.transform.position;
                Vector3 targetPosition = connection.AnimationPath[currentTargetIndex].position;

                currentPosition = Vector3.MoveTowards(currentPosition, targetPosition, m_transitSpeed * Time.deltaTime);

                if (Vector3.Distance(currentPosition, targetPosition) < .1f)
                {
                    currentPosition = targetPosition;
                    currentTargetIndex++;
                }

                m_playerIndicator.transform.position = currentPosition;
                yield return null;
            }

            m_currentNode = connection.Node;
            m_activeSave.m_currentLevel = m_currentNode.LevelInfo.ID;
            m_transitRoutine = null;
        }

        private void OnJumpInput()
        {            
            if (m_currentNode == null)
            {
                return;
            }

            m_sceneRoot.SceneLoader.LoadScene(m_currentNode.LevelInfo.SceneName);
            m_sceneRoot.gameObject.SetActive(false);
        }
    }
}
