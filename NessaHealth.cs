using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NessaHealth : MonoBehaviour
{
    public float nessaHealth = 150;
    public float startNessaHealth = 150f;
    public Image healthBar;
    public AudioClip nessaInPain;
    public bool isDead;

    private void Start()
    {
        nessaHealth = startNessaHealth;
    }
    private void Update()
    {
        healthBar.fillAmount = nessaHealth / startNessaHealth;
        if (nessaHealth <= 0)
        {
            isDead = true;
        }
    }

    public IEnumerator TakeDamage(int damage)
    {

        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }
        healthBar.fillAmount = nessaHealth / startNessaHealth;
        yield return new WaitForSeconds(2f);
        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }
        yield return new WaitForSeconds(2f);
        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }
        yield return new WaitForSeconds(2f);
        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }
        yield return new WaitForSeconds(2f);
        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }
        yield return new WaitForSeconds(2f);
        nessaHealth -= damage;
        if (!isDead)
        {
            AudioSource.PlayClipAtPoint(nessaInPain, FindObjectOfType<Camera>().transform.position, 0.35f);
        }

    }

}
