using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 savedPlayerPosition;
    private string currentEnemyId;
    private bool hasSavedPosition = false;

    public Vector3 SavedPlayerPosition => savedPlayerPosition;
    public bool HasSavedPosition => hasSavedPosition;
    public string CurrentEnemyId => currentEnemyId;

    //private bool battleResult;

    private HashSet<string> defeatedEnemies = new HashSet<string>();

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartBattle(Vector3 playerPosition, string enemyId)
    {
        savedPlayerPosition = playerPosition;
        hasSavedPosition = true;
        currentEnemyId = enemyId;

        SceneManager.LoadScene("BattleSceneMockup");
    }

    public void MarkEnemyDefeated(string enemyId)
    {
        defeatedEnemies.Add(enemyId);
    }

    public bool IsEnemyDefeated(string enemyId)
    {
        return defeatedEnemies.Contains(enemyId);
    }

    public void ReturnToWorldMap()
    {
        //battleResult = result;
        SceneManager.LoadScene("WorldMapScene");
    }

    public void RemoveSavedPosition()
    {
        hasSavedPosition = false;
    }
}
