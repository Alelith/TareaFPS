using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float lifetime;

    private bool isFromPlayer;

    private void Start() => Destroy(gameObject, lifetime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isFromPlayer) {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.DamageEnemy(damage);
            Destroy(this);
        }
        else if (other.CompareTag("Player") && !isFromPlayer) {
            other.GetComponent<PlayerController>().DamagePlayer(Mathf.RoundToInt(damage));
            Destroy(this);
        }
        else if (other.CompareTag("Map"))
        {
            Destroy(this);
        }
    }

    public float Damage { get => damage; set => damage = value; }
    public bool IsFromPlayer { get => isFromPlayer; set => isFromPlayer = value; }
}
