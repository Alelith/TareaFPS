using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Lives")]
    [SerializeField]
    private int currentLives;
    [SerializeField]
    private int maxLives;
    [SerializeField]
    private int defense;

    private WeaponController weaponController;

    private void Awake()
    {
        weaponController = GetComponent<WeaponController>();

        //Hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() => GetInputs();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            currentLives = Mathf.Clamp(currentLives + 5, 0, maxLives);
            GUIController.instance.UpdateLives(currentLives);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            DamagePlayer(1);
        }
    }

    /// <summary>
    /// Get the player's inputs
    /// </summary>
    private void GetInputs()
    {
        //Fire
        if (Input.GetButtonDown("Fire1"))
            if (weaponController.CanShoot())
                weaponController.Shoot();
    }

    public void DamagePlayer(int quantity)
    {
        currentLives -= (quantity / defense);
        if (currentLives <= 0)
            GUIController.instance.EndGame();
        GUIController.instance.UpdateLives(currentLives);
            
    }

    public int MaxLives { get => maxLives; set => maxLives = value; }
    public int CurrentLives { get => currentLives; set => currentLives = value; }
}
