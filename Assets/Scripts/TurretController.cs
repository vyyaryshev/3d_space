using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] string enemyTag;
    [SerializeField] float fireRange;
    [SerializeField, Range(0, 10f)] float reloadTime;
    [SerializeField] BulletController bulletPrefab;
    [SerializeField] Transform towerHead;
    [SerializeField] Transform firePoint;
    Transform target = null;
    bool canShoot;
    float timer;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(enemyTag).transform;
        timer = reloadTime;
        canShoot = false;
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, target.position);
            if (distanceToEnemy < fireRange)
            {
                if (canShoot)
                {
                    BulletController newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    newBullet.transform.LookAt(target.position);
                    canShoot = false;
                    timer = reloadTime;
                }
                else ReloadTimeControl();
            }
        }
        else
        {
            target = GameObject.FindGameObjectWithTag(enemyTag).transform;
        }
    }
    private void ReloadTimeControl()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) canShoot = true;
    }
}