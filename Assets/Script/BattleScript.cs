using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    [SerializeField] private Character playerCharacter;
    [SerializeField] private Character enemyCharacter;
    [SerializeField, Range(1, 100)] private int playerLvl;
    [SerializeField, Range(1, 100)] private int enemyLvl;
    [SerializeField] private GameObject[] attackButtons;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private SpriteRenderer enemySR;
    [SerializeField] private Image enemyPortrait;

    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;
    
    
    private int playerCurrentHealth;
    private int enemyCurrentHealth;
    private Attack selectedAttack;
    private Attack[] enemyAttacks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confirmButton.SetActive(false);

        playerCurrentHealth = playerCharacter.health;
        enemyCurrentHealth = enemyCharacter.health;
        levelText.text = "LV: " + playerLvl.ToString();

        enemyAttacks = enemyCharacter.attacks;
        enemySR.sprite = enemyCharacter.characterSprite;
        enemyPortrait.sprite = enemyCharacter.portraitSprite;

        playerHealthBar.fillAmount = (float)playerCurrentHealth / playerCharacter.health;

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

        confirmButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnConfirmButtonClicked());
    }

    void OnAttackButtonClicked(GameObject button)
    {
        int index = System.Array.IndexOf(attackButtons, button);

        selectedAttack = playerCharacter.attacks[index];
        descriptionText.text = "Current Attack Stats: \n" + selectedAttack.description;
        confirmButton.SetActive(true);
    }

    void OnConfirmButtonClicked()
    {
        if (selectedAttack != null)
        {
            Debug.Log("Aqui usarias o ataque: " + selectedAttack.attackName);
            if (selectedAttack.type == "Physical")
            {
                enemyCurrentHealth -= selectedAttack.value * playerLvl;
                if (enemyCurrentHealth < 0)
                {
                    enemyCurrentHealth = 0;
                }
                enemyHealthText.text = enemyCurrentHealth.ToString() + " / " + enemyCharacter.health.ToString();
                enemyHealthBar.fillAmount = (float)enemyCurrentHealth / enemyCharacter.health;
            }
            confirmButton.SetActive(false);
            EnemyTurn();
        }
        else
        {
            Debug.Log("Selecciona um ataque");
        }
    }

    void EnemyTurn()
    {
        float chance = Random.Range(0f, 1f);

        float cumulativeChance = 0f;
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            cumulativeChance += enemyCharacter.attackChances[i];
            if (chance <= cumulativeChance)
            {
                Attack enemyAttack = enemyAttacks[i];
                Debug.Log("Inimigo usa  " + enemyAttack.attackName);
                if (enemyAttack.type == "Physical")
                {
                    playerCurrentHealth -= enemyAttack.value * enemyLvl;
                    if (playerCurrentHealth < 0)
                    {
                        playerCurrentHealth = 0;
                    }
                    playerHealthText.text = playerCurrentHealth.ToString() + " / " + playerCharacter.health.ToString();
                    playerHealthBar.fillAmount = (float)playerCurrentHealth / playerCharacter.health;
                }
                break;
            }
        }
    }
}
