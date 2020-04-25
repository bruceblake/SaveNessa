using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon",menuName = "Weapons/Weapon")]
public class WeaponsData : ScriptableObject
{
    public Text priceText;
    public new string name;
    public float shootSpeed;
    public int fullClipSize; 
    public int clipSize;
    public int fullReserveAmmoSize;
    public int reserveAmmoSize;
    public int damage;
    public int cost;
    public float reloadTime;
    public bool playerHasWeapon;
    public bool canBeAutomatic;
    public Sprite artWork;
    public AudioClip weaponShotSound;
    public AudioClip weaponReloadSound;
    public AudioClip weaponOutOfAmmoSound;
    public float weaponShotVolume;
    public int ammoPrice;
    public int startAmmoPrice;
}
