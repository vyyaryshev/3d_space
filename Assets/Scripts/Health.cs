using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] GameObject destroyEffect;
    [SerializeField] bool isPlayer = false;
    int currHP;
    HealthBar healthBar;
    private void Start()
    {
        currHP = maxHP;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetBarValue(currHP, maxHP);
    }
    public void GetDamage(int damage)
    {
        currHP = Mathf.Clamp(currHP - damage, 0, maxHP);
        healthBar.SetBarValue(currHP, maxHP);
        if (currHP == 0)
        {
            if (isPlayer == true)
            {
                if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
                GameManager.instance.PlayerDefeat(gameObject);
            }
            else
            {
                if (destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}