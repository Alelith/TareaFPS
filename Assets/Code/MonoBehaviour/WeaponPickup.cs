using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : BasePickup
{
    [SerializeField]
    private Weapon weapon;

    public Weapon Weapon { get => weapon; set => weapon = value; }
}
