using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void PlayerDefeat(GameObject playerShip)
    {
        Camera.main.transform.SetParent(null);
        Destroy(playerShip);
    }
}