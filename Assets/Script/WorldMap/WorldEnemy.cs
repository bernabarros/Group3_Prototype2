using UnityEngine;

public class WorldEnemy : MonoBehaviour
{
    [SerializeField] private string enemyId;

    private void Start()
    {
        if (GameManager.Instance.IsEnemyDefeated(enemyId))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerMovement player))
        {
            gameObject.SetActive(false);
            GameManager.Instance.StartBattle(
                player.transform.position, enemyId
            ); 
        }
    }
}
