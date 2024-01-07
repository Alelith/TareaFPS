using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "FPS/Create new weapon")]
public class Weapon : ScriptableObject
{
    [Header("HUD")]
    [SerializeField]
    private List<Sprite> frames;
    [SerializeField]
    private Sprite gunImage;
    [Header("Weapon attributes")]
    [SerializeField]
    private float damageMultiplier;
    [SerializeField]
    private float recoilTime;
    [SerializeField]
    private float chargeTime;
    [SerializeField]
    private int magazineBullets;

    public List<Sprite> Frames { get => frames; set => frames = value; }
    public Sprite GunImage { get => gunImage; set => gunImage = value; }
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public float RecoilTime { get => recoilTime; set => recoilTime = value; }
    public int MagazineBullets { get => magazineBullets; set => magazineBullets = value; }
}
