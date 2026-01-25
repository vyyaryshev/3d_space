
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletController : MonoBehaviour
{
    [SerializeField, Range(1, 10)] int startPower;
    [SerializeField] int damage;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
        rb.linearVelocity = transform.forward * startPower * 100f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("BulletController.OnCollisionEnter");
        Health obj = collision.gameObject.GetComponentInParent<Health>();
        if (obj)
        {
            obj.GetDamage(damage);
        }
        Destroy(gameObject);
    }
}