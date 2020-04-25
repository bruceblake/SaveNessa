using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    public void MaxAmmo()
    {
        gameManager.weapons.reserveAmmoSize = gameManager.weapons.fullReserveAmmoSize;
    }
}
