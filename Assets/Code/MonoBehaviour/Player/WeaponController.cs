using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform outPosition;
    [Header("Ammo")]
    [SerializeField]
    private int currentAmmo;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private bool infiniteAmmo;
    [Header("Performance")]
    [SerializeField]
    private float ballSpeed;
    [SerializeField]
    private List<Weapon> weapons;

    private int currentWeapon = 0;

    private float lastShootTime;

    private int magazines;
    private int bullets;

    private void Awake()
    {
        SetupMagazines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeapon++;
            if (currentWeapon >= weapons.Count)
                currentWeapon = 0;
            GUIController.instance.SetupNewWeapon(weapons[currentWeapon]);
            SetupMagazines();
        }
        if (Input.GetKeyDown(KeyCode.R) && bullets == 0)
        {
            if (magazines > 0)
            {
                magazines--;
                bullets = weapons[currentWeapon].MagazineBullets;
                GUIController.instance.UpdateAmmo(bullets, magazines);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Weapons.Add(other.GetComponent<WeaponPickup>().Weapon);
            Destroy(other.gameObject);
        }
        /*else if (other.CompareTag("Magazines") && isPlayer)
        {
            currentAmmo += 15;
            SetupMagazines();
            Destroy(other.gameObject);
        }*/
    }

    private void SetupMagazines()
    {
        magazines = currentAmmo / weapons[currentWeapon].MagazineBullets;
        bullets = currentAmmo % weapons[currentWeapon].MagazineBullets;
        GUIController.instance.UpdateAmmo(bullets, magazines);
    }

    /// <summary>
    /// check if it is possible to shoot
    /// </summary>
    /// <returns>bool</returns>
    public bool CanShoot()
    {
        //1. shootRate
        if (Time.time - lastShootTime >= weapons[currentWeapon].RecoilTime && (bullets > 0 || infiniteAmmo))
            return true;

        return false;
    }

    /// <summary>
    /// Handle Weapon Shoot
    /// </summary>
    public void Shoot()
    {
        //update lastShootTime
        lastShootTime = Time.time;
        //reduce the Ammo
        if (!infiniteAmmo)
        {
            bullets--;
            currentAmmo--;
        }

        //Get a new ball
        GameObject ball = Instantiate(bulletPrefab, outPosition.position, Quaternion.identity);
        ball.GetComponent<BallController>().Damage *= weapons[currentWeapon].DamageMultiplier;

        ball.GetComponent<BallController>().IsFromPlayer = true;

        //create a Ray from the camera to the middel of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;

        //Check if your are pointing to sth and adjust the direction
        if (Physics.Raycast(ray, out RaycastHit hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(5); //5m

        ball.GetComponent<Rigidbody>().velocity = (targetPoint - ball.transform.position).normalized * ballSpeed;

        GUIController.instance.Animator.Play();
        GUIController.instance.UpdateAmmo(bullets, magazines);
    }

    public List<Weapon> Weapons { get => weapons; set => weapons = value; }
    public int CurrentWeapon { get => currentWeapon; set => currentWeapon = value; }
}
