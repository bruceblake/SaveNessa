using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    public GameObject clickToSeeMorePanel;
    public GameObject healingClickToSeeMore;
    public GameObject swapWeaponPanel;
    public Image weaponSwap1;
    public Image weaponSwap2;
    public List<Text> boughtStatus;
    public List<Healing> healingManager;
    public GameManager gameManager;
    public WeaponsData weapons;
    public WeaponsManager weaponsManager;
    public WeaponsDetails weaponsDetails;
    public HealingDetails healingDetails;
    public Text buyAmmo1;
    public Text buyAmmo2;
    public Text buyAmmo3;
    public Text buyAmmo4;
    public List<GameObject> buyAmmoButtons;
    public int hasWeapon;
    public AudioClip errorBuySound;
    
 
    
    //hasWeapon = 1 is false and hasWeapon = 0 = true
    

    private void Start()
    {
        hasWeapon = PlayerPrefs.GetInt("HasWeapon", 1);
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        weaponsManager = FindObjectOfType<WeaponsManager>().GetComponent<WeaponsManager>();
    
   
      
    }
    public void Update()
    {
       
        


        buyAmmo1.text = "Buy Ammo: $" + gameManager.weaponsManager.weapons[1].ammoPrice;
        buyAmmo2.text = "Buy Ammo: $" + gameManager.weaponsManager.weapons[2].ammoPrice;
        buyAmmo3.text = "Buy Ammo: $" + gameManager.weaponsManager.weapons[3].ammoPrice;
        buyAmmo4.text = "Buy Ammo: $" + gameManager.weaponsManager.weapons[4].ammoPrice;





    }
    public void BuyOne()
    {
        if (gameManager.cash >= weaponsManager.weapons[1].cost && weaponsManager.weapons[1].playerHasWeapon == false)
        {
            if (gameManager.weaponSlot1 == 0) //if weapon slot 1 has nothing in it
            {
                gameManager.cash -= weaponsManager.weapons[1].cost;
                weaponsManager.weapons[1].playerHasWeapon = true;
                weaponsManager.weapons[0].playerHasWeapon = false;
                gameManager.weaponSlot1 = 1; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 1;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
                gameManager.justBought = true;
                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }
            else if (gameManager.weaponSlot1 != 0 && gameManager.playerWeaponAmount != 2)
            {
                gameManager.cash -= weaponsManager.weapons[1].cost;
                weaponsManager.weapons[1].playerHasWeapon = true;
                gameManager.weaponSlot2 = 1; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 1;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.justBought = true;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;

                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }

            else if (gameManager.playerWeaponAmount == 2) // else if weapons slot 1 and 2 are full
            {
                weaponsDetails.swapButton1.onClick.AddListener(SwapWeaponSlot1001);
                weaponsDetails.swapButton2.onClick.AddListener(SwapWeaponSlot2001);
                AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                SeeSwapWeaponPanel();

            }


        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }
    public void BuyTwo()
    {
        if (gameManager.cash >= weaponsManager.weapons[2].cost && weaponsManager.weapons[2].playerHasWeapon == false)
        {
            if (gameManager.weaponSlot1 == 0) //if weapon slot 1 has nothing in it
            {
                gameManager.cash -= weaponsManager.weapons[2].cost;
                weaponsManager.weapons[2].playerHasWeapon = true;
                gameManager.weaponSlot1 = 2; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 2;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.justBought = true;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;

                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }
            else if (gameManager.weaponSlot1 != 0 && gameManager.playerWeaponAmount != 2)
            {
                gameManager.cash -= weaponsManager.weapons[2].cost;
                weaponsManager.weapons[2].playerHasWeapon = true;
                gameManager.weaponSlot2 = 2; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 2;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
                gameManager.justBought = true;

                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }

            else if (gameManager.playerWeaponAmount == 2) // else if weapons slot 1 and 2 are full
            {
                weaponsDetails.swapButton1.onClick.AddListener(SwapWeaponSlot101);
                weaponsDetails.swapButton2.onClick.AddListener(SwapWeaponSlot201);
                AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                SeeSwapWeaponPanel();

            }



        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }
    public void BuyThree()
    {
        if (gameManager.cash >= weaponsManager.weapons[3].cost && weaponsManager.weapons[3].playerHasWeapon == false)
        {
            if (gameManager.weaponSlot1 == 0) //if weapon slot 1 has nothing in it
            {
                gameManager.cash -= weaponsManager.weapons[3].cost;
                weaponsManager.weapons[3].playerHasWeapon = true;
                gameManager.weaponSlot1 = 3; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 3;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
                gameManager.justBought = true;

                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }
            else if (gameManager.weaponSlot1 != 0 && gameManager.playerWeaponAmount == 1) //if weapon slot 1 has nothing in it
            {
                gameManager.cash -= weaponsManager.weapons[3].cost;
                weaponsManager.weapons[3].playerHasWeapon = true;
                gameManager.weaponSlot2 = 3; //weapon slot 1 gets weapon index of 3
                gameManager.selectedWeaponIndex = 3;
                gameManager.listOfSounds[3].gameObject.SetActive(true);
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
                gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
                gameManager.justBought = true;

                if (gameManager.playerWeaponAmount != 2)
                {
                    gameManager.playerWeaponAmount++;
                }
            }

            else if (gameManager.playerWeaponAmount == 2) // else if weapons slot 1 and 2 are full
                {
                weaponsDetails.swapButton1.onClick.AddListener(SwapWeaponSlot102);
                weaponsDetails.swapButton2.onClick.AddListener(SwapWeaponSlot202);
                AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                SeeSwapWeaponPanel();
                    
                }



        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }
    public void BuyFour()
    {
        if (gameManager.cash >= weaponsManager.weapons[4].cost && weaponsManager.weapons[4].playerHasWeapon == false)
        {            
                
           if (gameManager.playerWeaponAmount == 2) // else if weapons slot 1 and 2 are full
            {
                weaponsDetails.swapButton1.onClick.AddListener(SwapWeaponSlot103);
                weaponsDetails.swapButton2.onClick.AddListener(SwapWeaponSlot203);
                AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                SeeSwapWeaponPanel();
            }

            if (gameManager.playerWeaponAmount != 2)
            {
                gameManager.playerWeaponAmount++;
            }

        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }
    public void BuyFive()
    {
        if (gameManager.cash >= weaponsManager.weapons[5].cost && weaponsManager.weapons[5].playerHasWeapon == false)
        { 
            

           if (gameManager.playerWeaponAmount == 2) // else if weapons slot 1 and 2 are full
            {
                weaponsDetails.swapButton1.onClick.AddListener(SwapWeaponSlot104);
                weaponsDetails.swapButton2.onClick.AddListener(SwapWeaponSlot204);
                AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
                SeeSwapWeaponPanel();
            }

            if (gameManager.playerWeaponAmount != 2)
            {
                gameManager.playerWeaponAmount++;
            }

        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }
    public void HealingBuyOne()
    {
        if (gameManager.cash >= healingManager[0].price && gameManager.sauceAmountOfHealing != 5)
        {
            gameManager.cash -= healingManager[0].price;
            healingManager[0].price += 500;
            gameManager.sauceAmountOfHealing++;
            gameManager.listOfSounds[3].gameObject.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }
    }


    public void Back()
    {
        gameManager.storeUI.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        Cursor.visible = false;
        Time.timeScale = 1f;
        gameManager.listOfSounds[3].gameObject.SetActive(false);
        gameManager.justBought = false;
        clickToSeeMorePanel.SetActive(false);
    }



    public void ClickToSeeMoreOne()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[1].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[1].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[1].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[1].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[1].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyOne);
       

    }
    public void ExitClickToSeeMorePanelOne()
    {
        clickToSeeMorePanel.SetActive(false);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyOne);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);

    }
    public void ClickToSeeMoreTwo()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[2].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[2].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[2].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[2].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[2].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyTwo);

    }
    public void ExitClickToSeeMorePanelTwo()
    {
        clickToSeeMorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyTwo);

    }
    public void ClickToSeeMoreThree()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[3].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[3].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[3].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[3].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[3].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyThree);


    }
    public void ExitClickToSeeMorePanelThree()
    {
        clickToSeeMorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyThree);

    }
    public void ClickToSeeMoreFour()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[4].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[4].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[4].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[4].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[4].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyFour);


    }
    public void ExitClickToSeeMorePanelFour()
    {
        clickToSeeMorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyFour);

    }
    public void ClickToSeeMoreFive()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[5].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[5].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[5].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[5].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[5].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyFour);


    }
    public void ExitClickToSeeMorePanelFive()
    {
        clickToSeeMorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyFour);

    }
    public void ClickToSeeMoreSix()
    {
        clickToSeeMorePanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.weaponName.text = weaponsManager.weapons[6].name;
        weaponsDetails.weaponPrice.text = "Price: " + weaponsManager.weapons[6].cost;
        weaponsDetails.weaponDamage.text = "Damage: " + weaponsManager.weapons[6].damage;
        weaponsDetails.weaponFireRate.text = "Firerate: " + weaponsManager.weapons[6].shootSpeed;
        weaponsDetails.weaponImage.sprite = weaponsManager.weapons[6].artWork;
        weaponsDetails.buyButton.onClick.RemoveAllListeners();
        weaponsDetails.buyButton.onClick.AddListener(BuyFive);


    }
    public void ExitClickToSeeMorePanelSix()
    {
        clickToSeeMorePanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.buyButton.onClick.RemoveListener(BuyFive);

    }
    public void ClickToSeeMoreHealingPanel01()
    {
        healingClickToSeeMore.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        healingDetails.healingName.text = healingManager[0].name;
        healingDetails.healingSprite.sprite = healingManager[0].artWork;
        healingDetails.healingAmount.text = "Heal Amount: " + healingManager[0].healAmount;
        healingDetails.healingSpeed.text = "Heal Speed: " + healingManager[0].healTime;

    }
    public void ExitClickToSeeMoreHealingPanel (){
        healingClickToSeeMore.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
    }

    public void SeeSwapWeaponPanel()
    {
        swapWeaponPanel.SetActive(true);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponSwap1.sprite = gameManager.weaponsManager.weapons[gameManager.weaponSlot1].artWork;
        weaponSwap2.sprite = gameManager.weaponsManager.weapons[gameManager.weaponSlot2].artWork;

    }
    public void ExitSeeSwapWeaponPanel()
    {
        swapWeaponPanel.SetActive(false);
        AudioSource.PlayClipAtPoint(gameManager.pressButtonSound, FindObjectOfType<Camera>().transform.position, 0.5f);
        weaponsDetails.swapButton1.onClick.RemoveAllListeners();
        gameManager.listOfSounds[3].gameObject.SetActive(false);
    }
    public void SwapWeaponSlot1001()
    {
        gameManager.cash -= weaponsManager.weapons[1].cost;
        weaponsManager.weapons[gameManager.weaponSlot1].playerHasWeapon = false;
        weaponsManager.weapons[1].playerHasWeapon = true;
        gameManager.weaponSlot1 = 1; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 1;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot2001()
    {
        gameManager.cash -= weaponsManager.weapons[1].cost;
        weaponsManager.weapons[gameManager.weaponSlot2].playerHasWeapon = false;
        weaponsManager.weapons[1].playerHasWeapon = true;
        gameManager.weaponSlot2 = 1; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 1;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }

    }
    public void SwapWeaponSlot101()
    {
        gameManager.cash -= weaponsManager.weapons[2].cost;
        weaponsManager.weapons[gameManager.weaponSlot1].playerHasWeapon = false;
        weaponsManager.weapons[2].playerHasWeapon = true;
        gameManager.weaponSlot1 = 2; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 2;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot201()
    {
        gameManager.cash -= weaponsManager.weapons[2].cost;
        weaponsManager.weapons[gameManager.weaponSlot2].playerHasWeapon = false;
        weaponsManager.weapons[2].playerHasWeapon = true;
        gameManager.weaponSlot2 = 2; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 2;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot102()
    {
        gameManager.cash -= weaponsManager.weapons[3].cost;
        weaponsManager.weapons[gameManager.weaponSlot1].playerHasWeapon = false;
        weaponsManager.weapons[3].playerHasWeapon = true;
        gameManager.weaponSlot1 = 3; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 3;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot202()
    {
        gameManager.cash -= weaponsManager.weapons[3].cost;
        weaponsManager.weapons[gameManager.weaponSlot2].playerHasWeapon = false;
        weaponsManager.weapons[3].playerHasWeapon = true;
        gameManager.weaponSlot2 = 3; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 3;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot103()
    {
        gameManager.cash -= weaponsManager.weapons[4].cost;
        weaponsManager.weapons[gameManager.weaponSlot1].playerHasWeapon = false;
        weaponsManager.weapons[4].playerHasWeapon = true;
        gameManager.weaponSlot1 = 4; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 4;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot203()
    {
        gameManager.cash -= weaponsManager.weapons[4].cost;
        weaponsManager.weapons[gameManager.weaponSlot2].playerHasWeapon = false;
        weaponsManager.weapons[4].playerHasWeapon = true;
        gameManager.weaponSlot2 = 4; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 4;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot104()
    {
        gameManager.cash -= weaponsManager.weapons[5].cost;
        weaponsManager.weapons[gameManager.weaponSlot1].playerHasWeapon = false;
        weaponsManager.weapons[5].playerHasWeapon = true;
        gameManager.weaponSlot1 = 5; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 5;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void SwapWeaponSlot204()
    {
        gameManager.cash -= weaponsManager.weapons[5].cost;
        weaponsManager.weapons[gameManager.weaponSlot2].playerHasWeapon = false;
        weaponsManager.weapons[5].playerHasWeapon = true;
        gameManager.weaponSlot2 = 5; //weapon slot 2 gets weapon index of 3
        gameManager.selectedWeaponIndex = 5;
        gameManager.listOfSounds[3].gameObject.SetActive(true);
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
        gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
        gameManager.justBought = true;

        if (gameManager.playerWeaponAmount != 2)
        {
            gameManager.playerWeaponAmount++;
        }
    }
    public void BuyAmmo1()
    {
        if (gameManager.cash >= weaponsManager.weapons[1].ammoPrice && gameManager.weaponsManager.weapons[1].playerHasWeapon == true && 
                                                                                     gameManager.weaponsManager.weapons[1].reserveAmmoSize != gameManager.weaponsManager.weapons[1].fullReserveAmmoSize)
        {
            gameManager.cash -= weaponsManager.weapons[1].ammoPrice;
            gameManager.weaponsManager.weapons[1].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
            gameManager.weaponsManager.weapons[1].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
            gameManager.weaponsManager.weapons[1].ammoPrice += 250;
            gameManager.listOfSounds[3].gameObject.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }

    }
    public void BuyAmmo2()
    {
        if (gameManager.cash >= weaponsManager.weapons[2].ammoPrice && gameManager.weaponsManager.weapons[2].playerHasWeapon == true &&
                                                                                     gameManager.weaponsManager.weapons[2].reserveAmmoSize != gameManager.weaponsManager.weapons[2].fullReserveAmmoSize)
        {
            gameManager.cash -= weaponsManager.weapons[2].ammoPrice;
            gameManager.weaponsManager.weapons[2].clipSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullClipSize;
            gameManager.weaponsManager.weapons[2].reserveAmmoSize = gameManager.weaponsManager.weapons[gameManager.selectedWeaponIndex].fullReserveAmmoSize;
            gameManager.weaponsManager.weapons[2].ammoPrice += 500;
            gameManager.listOfSounds[3].gameObject.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }

    }
    public void BuyAmmo3()
    {        if (gameManager.cash >= weaponsManager.weapons[3].ammoPrice && gameManager.weaponsManager.weapons[3].playerHasWeapon == true  &&
                                                                                     gameManager.weaponsManager.weapons[3].reserveAmmoSize != gameManager.weaponsManager.weapons[3].fullReserveAmmoSize)
        {
            gameManager.cash -= weaponsManager.weapons[3].ammoPrice;
            gameManager.weaponsManager.weapons[3].clipSize = gameManager.weaponsManager.weapons[3].fullClipSize;
            gameManager.weaponsManager.weapons[3].reserveAmmoSize = gameManager.weaponsManager.weapons[3].fullReserveAmmoSize;
            gameManager.weaponsManager.weapons[3].ammoPrice += 750;
            gameManager.listOfSounds[3].gameObject.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }


    }
    public void BuyAmmo4()
    {
        if (gameManager.cash >= weaponsManager.weapons[4].ammoPrice && gameManager.weaponsManager.weapons[4].playerHasWeapon == true &&
                                                                                     gameManager.weaponsManager.weapons[4].reserveAmmoSize != gameManager.weaponsManager.weapons[4].fullReserveAmmoSize)
        {
            gameManager.cash -= weaponsManager.weapons[4].ammoPrice;
            gameManager.weaponsManager.weapons[4].clipSize = gameManager.weaponsManager.weapons[4].fullClipSize;
            gameManager.weaponsManager.weapons[4].reserveAmmoSize = gameManager.weaponsManager.weapons[4].fullReserveAmmoSize;
            gameManager.weaponsManager.weapons[4].ammoPrice += 750;
            gameManager.listOfSounds[3].gameObject.SetActive(true);
        }
        else
        {
            AudioSource.PlayClipAtPoint(errorBuySound, FindObjectOfType<Camera>().transform.position, 0.7f);
        }

    }



}
