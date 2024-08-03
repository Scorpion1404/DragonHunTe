using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Fire Timers")]
    [SerializeField] private float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private Health playerHealth;


    [Header("SFX")]
    [SerializeField] private AudioClip FireSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if(playerHealth != null && active) {

            playerHealth.TakeDamage(damage);

            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {

            playerHealth = collision.GetComponent<Health>();
            if (!triggered)
            {
                StartCoroutine(ActiveFireTrap());
            }
            if (active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            playerHealth = null;

        }

    }

    private IEnumerator ActiveFireTrap()
    {
        triggered = true;
        spriteRend.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(FireSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated",true);


        //deactivate trap
        yield return new WaitForSeconds(activationDelay);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
