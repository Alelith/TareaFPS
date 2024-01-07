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

    private void Start()
    {
        //TODO htealth bar
        //GUIController.instance.UpdateHealthBar(currentLives);
    }

    private void Update()
    {
        GetInputs();
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
        GUIController.instance.UpdateLives(currentLives);
        if (currentLives <= 0)
        {
            //TODO : GameManager , HUD
            Debug.Log("GAME OVER!!!");
        }
            
    }

    public int MaxLives { get => maxLives; set => maxLives = value; }
    public int CurrentLives { get => currentLives; set => currentLives = value; }
}
