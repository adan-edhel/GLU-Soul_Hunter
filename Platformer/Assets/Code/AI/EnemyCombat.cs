﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter.Enemy
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] float attackDuration;
        [SerializeField] float attackCooldown;
        public GameObject Weapon;
        float attackTimer;
        float cooldown;
        bool attacking;
        void Start()
        {
            Weapon.SetActive(false);
            attackDuration = attackDuration / 10;
        }

        void Update()
        {
            if (attacking == true)
            {
                attackTimer += Time.deltaTime;
            }
            cooldown += Time.deltaTime;
            ResetWeapon();
        }

        public void Attack()
        {
            if (cooldown > attackCooldown)
            {
                if (GetComponent<EnemyScript>().MoveRight == false)
                {
                    Weapon.transform.localPosition = new Vector3(-.5f, 0, 0);
                }
                else
                {
                    Weapon.transform.localPosition = new Vector3(.5f, 0, 0);
                }
                Weapon.SetActive(true);
                attacking = true;
                cooldown = 0;
            }
        }

        void ResetWeapon()
        {
            if (attackTimer >= attackDuration)
            {
                Weapon.SetActive(false);
                attackTimer = 0;
                attacking = false;
            }
        }
    }
}