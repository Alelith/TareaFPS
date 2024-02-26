using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{

    [SerializeField]
    private Transform outPosition;
    [Header("Ammo")]
    [SerializeField]
    private GameObject bulletPrefab;
    [Header("Performance")]
    [SerializeField]
    private float ballSpeed;

    public void Attack(Transform objetive)
    {
        var bullet = Instantiate(bulletPrefab, outPosition.position, Quaternion.identity);
        bullet.GetComponent<BallController>().IsFromPlayer = false;

        bullet.GetComponent<Rigidbody>().velocity = (-1) * ballSpeed * outPosition.forward;
    }
}
