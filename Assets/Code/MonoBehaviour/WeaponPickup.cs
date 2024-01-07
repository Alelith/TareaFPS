using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }
}
