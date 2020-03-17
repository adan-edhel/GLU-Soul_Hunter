﻿using UnityEngine;

using SoulHunter.Player;
using SoulHunter.Gameplay;

namespace SoulHunter.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float attackDuration;
        Collider2D OverlapCircle;
        public GameObject Weapon;
        float attackTimer;
        bool attacking;

        void Start()
        {
            Weapon.SetActive(false);
            attackDuration = attackDuration / 10;
            OverlapCircle = Physics2D.OverlapCircle(transform.position, 2f, 11);
        }

        void Update()
        {
            if (attacking == true)
            {
                attackTimer += Time.deltaTime;
            }
            if (OverlapCircle.gameObject.CompareTag("Enemy"))
            {
                print("Enemy is in range");
            }

            ResetWeapon();
        }

        public void Attack()
        {
            if (GetComponent<PlayerAim>().aimDirection.x < 0)
            {
                Weapon.transform.localPosition = new Vector3(-1, 0, 0);
            }
            else
            {
                Weapon.transform.localPosition = new Vector3(1, 0, 0);
            }
            Weapon.SetActive(true);
            attacking = true;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                GetComponent<HealthSystem>().TakeDamage();
            }
        }
    }
}