using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform[] guns;

    [SerializeField] private BulletController bulletPrefab;
    [SerializeField, Range(0.1f, 20f)] private float reloadTime;

    //private int currentGunIndex;
    private float timer;
    private bool canShoot;

    private void Start()
    {
        //currentGunIndex = 0;
        timer = 0f;
        canShoot = true;
    }

    private void Update()
    {
        if (!canShoot)
            ReloadTimeControl();

        if (Input.GetButton("Fire1") && canShoot)
        {
            foreach (Transform gun in guns) { 
                var newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation);

                canShoot = false;
                timer = reloadTime;
            }

            //currentGunIndex++;

            //if (currentGunIndex == guns.Length)
            //    currentGunIndex = 0;
        }
    }

    private void ReloadTimeControl()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            canShoot = true;
    }
}