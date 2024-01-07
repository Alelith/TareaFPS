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
        var bullet = Instantiate(bulletPrefab, outPosition);
        bullet.GetComponent<BallController>().IsFromPlayer = false;

        bullet.GetComponent<Rigidbody>().velocity = outPosition.forward * ballSpeed * (-1);
    }
}
