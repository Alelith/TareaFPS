using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float mouseSensibility;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Transform lookerPoint;
    [SerializeField]
    private float runMultiplier;
    [SerializeField]
    private float maxStamina;
    [SerializeField]
    private float staminaDelay;
    private float currentStamina;

    private Rigidbody characterController;

    private float tempMoveSpeed;

    private float tempDelay = 0;

    private void Awake()
    {
        characterController = GetComponent<Rigidbody>();
        tempMoveSpeed = moveSpeed;

        currentStamina = maxStamina;
    }

    private void FixedUpdate()
    {
        Move();

        GetInputs();

        Debug.Log(Time.deltaTime);
        Debug.Log(currentStamina);
    }

    private void GetInputs()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > tempDelay && currentStamina > 0)
            Run();
        else
            ChargeStamina();
    }

    private void Run()
    {
        currentStamina -= Time.deltaTime * 10;

        moveSpeed = tempMoveSpeed * runMultiplier;

        GUIController.instance.UpdateStamina(currentStamina);
    }

    private void ChargeStamina()
    {
        if (currentStamina < staminaDelay)
            tempDelay = staminaDelay;
        if (currentStamina > staminaDelay)
            tempDelay = 0;

        currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime * 10, 0, maxStamina);

        moveSpeed = tempMoveSpeed;

        GUIController.instance.UpdateStamina(currentStamina);
    }

    private void Move()
    {
        characterController.velocity = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed);

        var turner = Input.GetAxis("Mouse X") * mouseSensibility;
        var looker = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(0, turner, 0);
        lookerPoint.position += new Vector3 (0, looker, 0);
    }

    public float MaxStamina { get => maxStamina; set => maxStamina = value; }
}
