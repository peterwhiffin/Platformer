using System.Collections;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil.Cil;
using PetesPlatformer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PetesPlatformer
{
    public class Overworld : MonoBehaviour
    {
        private const string m_baseFileName = "saveslot";
        private const string m_fileExtension = ".sav";
        private OverworldNode m_currentNode;
        private Coroutine m_transitRoutine;

        [SerializeField] private GameSave m_activeSave;
        [SerializeField] private InputReader m_inputReader;
        [SerializeField] private List<OverworldNode> m_nodes;
        [SerializeField] private GameObject m_playerIndicator;
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


            try
            {
                string fullPath = Path.Combine(Application.persistentDataPath, m_baseFileName + m_activeSave.m_saveSlot.ToString() + m_fileExtension);
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                string dataToStore = JsonUtility.ToJson(m_activeSave);

                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }

            }
            catch (System.Exception e)
            {
                Debug.LogError("big time error town: " + e.Message);
            }


            InputReader.MoveInput += OnMoveInput;
            InputReader.JumpActivated += OnJumpInput;
        }

        private void OnDestroy()
        {
            InputReader.MoveInput -= OnMoveInput;
            InputReader.JumpActivated -= OnJumpInput;
        }

        //This is bad
        private void OnMoveInput(Vector2 direction)
        {

            if (m_transitRoutine != null)
            {
                return;
            }

            if (direction.x != 0)
            {
                if (direction.x > 0)
                {
                    if (CheckDirection(OverworldDirection.East))
                    {
                        return;
                    }
                }
                else
                {
                    if (CheckDirection(OverworldDirection.West))
                    {
                        return;
                    }
                }
            }

            if (direction.y != 0)
            {
                if (direction.y > 0)
                {
                    if (CheckDirection(OverworldDirection.North))
                    {
                        return;
                    }
                }
                else
                {
                    if (CheckDirection(OverworldDirection.South))
                    {
                        return;
                    }
                }
            }
        }

        private bool CheckDirection(OverworldDirection direction)
        {
            bool foundDirection = false;

            foreach (var connection in m_currentNode.Connections)
            {
                if (connection.Direction == direction && connection.Node.IsUnlocked)
                {
                    m_transitRoutine = StartCoroutine(MoveToNode(connection));
                    foundDirection = true;
                    break;
                }
            }

            Debug.Log("Check Direction: " + direction.ToString());

            return foundDirection;
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
            m_transitRoutine = null;
        }

        private void OnJumpInput()
        {
            if (m_currentNode == null)
            {
                return;
            }

            m_activeSave.m_currentLevel = m_currentNode.LevelInfo.ID;

            SceneManager.LoadScene(m_currentNode.LevelInfo.SceneName, LoadSceneMode.Single);
        }
    }
}
