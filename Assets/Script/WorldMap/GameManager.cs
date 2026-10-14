using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector3 savedPlayerPosition;
    private string currentEnemyId;
    private bool hasSavedPosition = false;

    private string battleResult;

    private int? playerHealth = null;
    private int? playerMana = null;

    public Vector3 SavedPlayerPosition => savedPlayerPosition;
    public bool HasSavedPosition => hasSavedPosition;
    public string CurrentEnemyId => currentEnemyId;
    public string BattleResult => battleResult;

    public int? PlayerHealth => playerHealth;
    public int? PlayerMana => playerMana;


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

    public void ReturnToWorldMap(string result)
    {
        if(result == "Defeat")
        {
            defeatedEnemies.Clear();

            hasSavedPosition = false;
        }

        SceneManager.LoadScene("WorldMapScene");
    }

    public void RemoveSavedPosition()
    {
        hasSavedPosition = false;
    }

    public void PlayerVictory()
    {
        battleResult = "Victory";
    }

    public void PlayerDefeat()
    {
        battleResult = "Defeat";
    }

    public void SetPlayerHealth(int healthValue)
    {
        playerHealth = healthValue;
    }

    public void SetPlayerMana(int manaValue)
    {
        playerMana = manaValue;
    }
}
