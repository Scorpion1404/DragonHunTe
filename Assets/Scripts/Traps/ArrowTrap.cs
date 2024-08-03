using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float coolDownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        coolDownTimer = 0;
        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();


    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (coolDownTimer  >= attackCoolDown ) { 
            Attack();
        }
    }
}
