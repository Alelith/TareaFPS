using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : BasePickup
{
    [SerializeField]
    private Weapon weapon;

    private void Start() => GetComponent<SpriteRenderer>().sprite = weapon.GunImage;

    public Weapon Weapon { get => weapon; set => weapon = value; }
}
