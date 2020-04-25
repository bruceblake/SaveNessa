using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform nessa;
    public GameManager gameManager;
    public GameObject floatingTextPrefab;
    public Spawner spawner;
    public Shoot shoot;
    public NessaHealth nessaHealth;
    public AudioClip enemyDeathSound;



    public float moveSpeed = 4f;
    public float enemyDamage;
    public float enemyHealth = 100f;
    public int attackDamageTowardsNessa = 34; // the attack damage of the enemy

    


    Rigidbody2D rb;
    Vector2 movement;


    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        enemyDamage = PlayerPrefs.GetFloat("EnemyDamage", 100);
        spawner = FindObjectOfType<Spawner>().GetComponent<Spawner>();
        shoot = FindObjectOfType<Shoot>().GetComponent<Shoot>();



    }
 
    private void Update()
    {
        Vector3 characterScale = transform.localScale;
        PlayerPrefs.SetFloat("EnemyDamage", enemyDamage);
        enemyDamage = gameManager.weapons.damage;
        Vector3 direction = nessa.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        MoveCharacter(direction);
        if (direction.x < 0)
        {
            characterScale.x = -2;
        }else if (direction.x > 0)
        {
            characterScale.x = 2;
        }
        transform.localScale = characterScale;
    }
    void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }



    private void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1") && gameManager.canShoot && !gameManager.isReloading)
        {
            shoot.ShootGun();
            if (gameManager.weapons.clipSize > 0)
            {
                TakeDamage(enemyDamage);
            }
        }

        if (Input.GetButton("Fire1") && gameManager.canShoot &&  gameManager.weapons.canBeAutomatic && !gameManager.isReloading)
        {

            if (shoot.waitTillNextFire <= 0)
            {
                shoot.ShootGun();
                if (gameManager.weapons.clipSize > 0)
                {
                    TakeDamage(enemyDamage * gameManager.weapons.shootSpeed);
                    shoot.waitTillNextFire = 1f / gameManager.weapons.shootSpeed;
                }
            }
        }
        shoot.waitTillNextFire -= Time.deltaTime;


    }


        


    
    public void TakeDamage(float damage)
    {
       
        enemyHealth -= damage;
        if (floatingTextPrefab)
        {
            ShowFloatingText();
        }
        if (enemyHealth <= 0)
        {
            Die();
        }
    }
    void ShowFloatingText()
    {
        
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = enemyHealth.ToString();
    }
    
    public void Die()
    {
        AudioSource.PlayClipAtPoint(enemyDeathSound, FindObjectOfType<Camera>().transform.position, 0.2f);
        Destroy(gameObject);
        gameManager.cash += 130;
        gameManager.finalEnemiesKilled++;
        gameManager.finalCash += 130;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Nessa")
        {
                StartCoroutine(gameManager.nessaHealth.TakeDamage(attackDamageTowardsNessa)); 
        }
    }
   
}
