using UnityEngine;
using TMPro;

public class BattleScript : MonoBehaviour
{
    [SerializeField] private Character playerCharacter;
    [SerializeField] private Character enemyCharacter;
    [SerializeField, Range(1, 100)] private int playerLvl;
    [SerializeField] private GameObject[] attackButtons;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private TMP_Text descriptionText;
    
    
    private int playerCurrentHealth;
    private int enemyCurrentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCurrentHealth = playerCharacter.health;
        enemyCurrentHealth = enemyCharacter.health;
        for (int i = 0; i < attackButtons.Length; i++)
        {
            if (i < playerCharacter.attacks.Length)
            {
                attackButtons[i].GetComponentInChildren<TMP_Text>().text = playerCharacter.attacks[i].attackName;
            }
            else
            {
                attackButtons[i].SetActive(false);
            }
        }

        playerNameText.text = playerCharacter.characterName;
        playerHealthText.text = playerCurrentHealth.ToString() + " / " + playerCharacter.health.ToString();
        enemyNameText.text = enemyCharacter.characterName;
        enemyHealthText.text = enemyCurrentHealth.ToString() + " / " + enemyCharacter.health.ToString();
    }

    void Awake()
    {
        foreach (GameObject button in attackButtons)
        {
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnAttackButtonClicked(button));
        }
    }

    void OnAttackButtonClicked(GameObject button)
    {
        int index = System.Array.IndexOf(attackButtons, button);

        descriptionText.text = "Current Attack Stats: \n" +playerCharacter.attacks[index].description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
