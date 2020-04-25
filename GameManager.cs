using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
 
    public List<AudioSource> listOfSounds;
    public Text roundText;
    public Text gameOverScreenTimePlayed;
    public Text gunSelectedText;
    public Text cashText;
    public Text storeCashText;
    public Text ammo;
    public Text gameOverRoundsStats;
    public Image gunSelectedImage;
    public WeaponsData weapons;
    public GameObject shopButton;
    public GameObject PauseMenu;
    public GameObject crosshair;
    public Store store;
    public GameObject nessaPlayer;
    public GameObject storeUI;
    public GameObject gameOverScreen;
    public Text gameOverScreenFinalCash;
    public Text gameOverScreenFinalEnemiesKilled;
    public int finalEnemiesKilled;
    public int finalCash = 500;
    public  WeaponsManager weaponsManager;
    public float attackDamage;
    public int rounds = 1;
    public int selectedWeaponIndex = 0;
    public int cash;
    public bool isPaused;
    public bool needToReload = false;
    public bool canShoot = true;
    public int playerWeaponAmount = 0;
    public bool isReloading;
    public float startTime;
    public bool stillPlaying = true;
    public int weaponSlot1;
    public int weaponSlot2;
    public NessaHealth nessaHealth;
    public bool justBought;
    public int sauceAmountOfHealing;
    public Text amountOfSauceText;
    public AudioClip pressButtonSound;
    public GameObject upgradesPanelMenu;
    public Text nessaHealthText;
   
   
    

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        cash = 500;
        selectedWeaponIndex = 0;
        store.hasWeapon = 1;
        weapons.playerHasWeapon = false;
        weaponsManager.weapons[0].playerHasWeapon = true;
        playerWeaponAmount = 0;
        for (int i = 1; i < weaponsManager.weapons.Count; i++)
        {
            weaponsManager.weapons[i].playerHasWeapon = false;
            weaponsManager.weapons[i].ammoPrice = weaponsManager.weapons[i].startAmmoPrice;
        }
        playerWeaponAmount = 2;
        playerWeaponAmount = 0;
        weaponSlot1 = 0;
        storeUI.SetActive(false);
        Cursor.visible = false;
        cash = PlayerPrefs.GetInt("Cash", 500);
        store = FindObjectOfType<Store>().GetComponent<Store>();
        weaponsManager = this.GetComponent<WeaponsManager>();
        selectedWeaponIndex = PlayerPrefs.GetInt("SelectedWeaponIndex", 0);
        weapons = weaponsManager.weapons[0];
        weapons.clipSize = weapons.fullClipSize;
        weapons.reserveAmmoSize = weapons.fullReserveAmmoSize;
        nessaHealth = this.GetComponent<NessaHealth>();
        
        
        
    }

    private void Update()
    {
        
        nessaHealthText.text = "Health: " + nessaHealth.nessaHealth;
        amountOfSauceText.text = "x" + sauceAmountOfHealing;
            gunSelectedText.text = weaponsManager.weapons[selectedWeaponIndex].name;
            attackDamage = weaponsManager.weapons[selectedWeaponIndex].damage;
            gunSelectedImage.sprite = weaponsManager.weapons[selectedWeaponIndex].artWork;
            ammo.text = weaponsManager.weapons[selectedWeaponIndex].clipSize.ToString() + "/" + weaponsManager.weapons[selectedWeaponIndex].reserveAmmoSize.ToString();
            nessaHealth.healthBar.fillAmount = nessaHealth.nessaHealth / nessaHealth.startNessaHealth;
        if (weaponsManager.weapons[selectedWeaponIndex].name != "Nothing")
        {
            gunSelectedImage.gameObject.SetActive(true);
            ammo.gameObject.SetActive(true);
            gunSelectedImage.sprite = weaponsManager.weapons[selectedWeaponIndex].artWork;
            ammo.text = weaponsManager.weapons[selectedWeaponIndex].clipSize.ToString() + "/" + weaponsManager.weapons[selectedWeaponIndex].reserveAmmoSize.ToString();
        }
        else
        {
            gunSelectedImage.gameObject.SetActive(false);
            ammo.gameObject.SetActive(false);
        }

        if (stillPlaying)
        {
            startTime += Time.deltaTime;
        }
        roundText.text = "Round: " + rounds;
        if (nessaHealth.nessaHealth <= 0)
        {
            listOfSounds[0].gameObject.SetActive(true);
            DeathScreen();
        }
           
        if (Cursor.visible && storeUI.activeSelf)
        {
            crosshair.SetActive(false);
            canShoot = false;
        }else
        {
            crosshair.SetActive(true);
            canShoot = true;
        }
        if (weapons.clipSize == 0)
        {
            needToReload = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isPaused || Input.GetKeyDown(KeyCode.RightShift) && !isPaused)
        {
            if (storeUI.activeSelf)
            {
                store.Back();
                Cursor.visible = false;
                AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                Shop();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isPaused)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
            AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        } 
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Resume();
            AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !upgradesPanelMenu.activeSelf)
        {
            UpgradeMenu();
            AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && upgradesPanelMenu.activeSelf)
        {
            upgradesPanelMenu.SetActive(false);
            AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
            Cursor.visible = false;
            Time.timeScale = 1f;
        }



        if (Input.GetKeyDown(KeyCode.Alpha1) || justBought && selectedWeaponIndex == weaponSlot1)
            {

            if (playerWeaponAmount == 0) //player has bought nothing so far
            {
                selectedWeaponIndex = 0;
                weaponSlot1 = 0;
            }else if (playerWeaponAmount == 1) //player has 1 weapon
            {
                selectedWeaponIndex = weaponSlot1;  //selected weapon is whatever is in slot 1
            }
            else if (playerWeaponAmount == 2) //player has 2 weapons
            {
                selectedWeaponIndex = weaponSlot1;  //selected weapon is whatever is in slot 1
            }
            if (weaponsManager.weapons[selectedWeaponIndex].playerHasWeapon)
            {
                weapons = weaponsManager.weapons[selectedWeaponIndex];
                if (weapons.reserveAmmoSize < 0)
                {
                    weapons.reserveAmmoSize = 0;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && playerWeaponAmount > 1 || justBought && playerWeaponAmount > 1) //player has full slots
        {
            selectedWeaponIndex = weaponSlot2;
            if (weaponsManager.weapons[selectedWeaponIndex].playerHasWeapon) //if player bought the weapon
            {
                weapons = weaponsManager.weapons[selectedWeaponIndex]; //current weapon is that weapon
                if (weapons.reserveAmmoSize < 0)
                {
                    weapons.reserveAmmoSize = 0;
                }
            }
        }
          
        PlayerPrefs.SetInt("SelectedWeaponIndex", selectedWeaponIndex);
        PlayerPrefs.SetInt("Cash", cash);
        cashText.text = "Cash: " + cash.ToString();
        storeCashText.text = "Cash: " + cash.ToString();
       
        
    



    }


    public void UpgradeMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 0.5f;
        canShoot = false;
        upgradesPanelMenu.SetActive(true);
    }
    public void Shop()
    {
        storeUI.SetActive(true);
        Cursor.visible = true;
        shopButton.SetActive(false);
        Time.timeScale = 0.5f;
        
       
    }
        public void Resume()
    {
        listOfSounds[1].gameObject.SetActive(true);
        canShoot = true;
        isPaused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    void Pause()
    {
        listOfSounds[1].gameObject.SetActive(false);
        canShoot = false;
        isPaused = true;
        PauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        cash = 500;
        selectedWeaponIndex = 0;
        store.hasWeapon = 1;
        weapons.playerHasWeapon = false;
        weaponsManager.weapons[0].playerHasWeapon = true;
        playerWeaponAmount = 0;
        rounds = 0;
        for (int i = 1; i < weaponsManager.weapons.Count; i++)
        {
            weaponsManager.weapons[i].playerHasWeapon = false;
        }
        store.healingManager[0].price = 0;
    }
    IEnumerator Reload()
    {
        if (weapons.clipSize != weapons.fullClipSize && weapons.reserveAmmoSize != 0) //if clip size isnt full and there is reserve ammo
        {
            isReloading = true;
            listOfSounds[2].gameObject.SetActive(true);
            yield return new WaitForSeconds(weapons.reloadTime);
            if (weapons.reserveAmmoSize > weapons.clipSize)
            {
                weapons.reserveAmmoSize -= (weapons.fullClipSize - weapons.clipSize);
                weapons.clipSize = weapons.fullClipSize;
                needToReload = false;

            } else if (weapons.reserveAmmoSize < weapons.clipSize)
            {
                weapons.clipSize = weapons.fullClipSize;
                weapons.reserveAmmoSize -= (weapons.fullClipSize - weapons.clipSize);
                needToReload = false;

            } else if (weapons.reserveAmmoSize == weapons.clipSize) {
                weapons.clipSize = weapons.reserveAmmoSize;
                weapons.reserveAmmoSize = 0;
                needToReload = false;
            }
              
            }
        
        isReloading = false;
        listOfSounds[2].gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public void DeathScreen()
    {
        selectedWeaponIndex = 0;
        weaponSlot1 = 0;
        weaponSlot2 = 0;
        gameOverScreen.SetActive(true);
        canShoot = false;
        Cursor.visible = true;
        stillPlaying = false;
        if (rounds == 1) {
            gameOverRoundsStats.text = "1 Round";
        }
        else
        {
            gameOverRoundsStats.text = rounds + " Rounds";
        }
        gameOverScreenFinalCash.text = finalCash.ToString();
        gameOverScreenFinalEnemiesKilled.text = finalEnemiesKilled.ToString();
        int conversionPlayTime;
        if (startTime <= 59)
        {
            gameOverScreenTimePlayed.text = startTime.ToString("F1") + " Seconds";
        }else
        {
            conversionPlayTime = Mathf.RoundToInt(startTime / 60);
            if (conversionPlayTime > 1)
            {
                gameOverScreenTimePlayed.text = conversionPlayTime + " Minutes";
            }
            else
            {
                gameOverScreenTimePlayed.text = conversionPlayTime + " Minute";
            }
        }
        for (int i = 1; i < weaponsManager.weapons.Count; i++)
        {
            weaponsManager.weapons[i].playerHasWeapon = false;
        }
        
    }
    public void PlayAgain()
    {
        AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        SceneManager.LoadScene("GameScene");
        Debug.Log("End");
      
    }
    public void UseSauce()
    {
        if (sauceAmountOfHealing != 0)
        {
            if (this.nessaHealth.nessaHealth >= nessaHealth.startNessaHealth)
            {
                sauceAmountOfHealing--;
              
            }
            else
            {
                sauceAmountOfHealing--;
                this.nessaHealth.nessaHealth += 75;
                Debug.Log("Healed");
            }
        }
    }
    public void PressButtonSound()
    {
        AudioSource.PlayClipAtPoint(pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
    }
}
