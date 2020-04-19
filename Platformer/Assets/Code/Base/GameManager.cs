﻿using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Enemy;

namespace SoulHunter
{
    public class GameManager : MonoBehaviour // Mort
    {
        // Singleton Instance
        public static GameManager Instance;

        // Game Pause State
        public static bool gameIsPaused;

        // Player interact state
        public static bool interacting;

        // Dialogue state
        public static bool initiatedDialogue;

        // List of enemies currently alive
        public List<EnemyBase> Enemies = new List<EnemyBase>();

        // List of all portals
        public List<PortalScript> Portals = new List<PortalScript>();

        private void Awake()
        {
            Instance = this;
            name = "Managers";
            DontDestroyOnLoad(this);

            AudioManager.Initialize();
        }

        /// <summary>
        /// Adds or removes enemies to/from the list of currently alive enemies
        /// </summary>
        /// <param name="enemy"></param>
        public void EnemyListRegistry(EnemyBase enemy)
        {
            if (!Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
            }
            else
            {
                Enemies.Remove(enemy);

                // End Portal Conditions
                if (Enemies.Count >= 20)
                {
                    for (int i = 0; i < Portals.Count; i++)
                    {
                        Portals[i].isActivatable = true;
                    }
                }
            }
        }

        /// <summary>
        /// Adds or removes enemies to/from the list of currently alive enemies
        /// </summary>
        /// <param name="portal"></param>
        public void PortalListRegistry(PortalScript portal)
        {
            if (!Portals.Contains(portal))
            {
                Portals.Add(portal);
            }
            else
            {
                Portals.Remove(portal);
            }
        }
    }
}