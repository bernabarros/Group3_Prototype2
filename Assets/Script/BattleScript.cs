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
    [SerializeField] private GameObject confirmButton;
    
    
    private int playerCurrentHealth;
    private int enemyCurrentHealth;
    private Attack selectedAttack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confirmButton.SetActive(false);

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
            confirmButton.SetActive(false);
        }
        else
        {
            Debug.Log("Selecciona um ataque");
        }
    }
}
