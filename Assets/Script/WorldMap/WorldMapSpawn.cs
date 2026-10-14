using UnityEngine;

public class WorldPlayerSpawn : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.HasSavedPosition)
        {
            transform.position =
                GameManager.Instance.SavedPlayerPosition;

            GameManager.Instance.RemoveSavedPosition();
        }
    }
}

