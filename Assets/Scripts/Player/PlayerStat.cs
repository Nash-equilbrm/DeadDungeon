using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStat
{
    // Current wave
    public int stage = 1; 

    // Player health
    public int startingHealth = 10;
    public int currentHealth = 0;
    public int medkitHeal = 5;
    public float hurtDelayTime = 0.5f;

    //Player movement
    public float speed = 10f;
    public float speedUp = 5f;
    public float speedUpCoolDown = 3f;

    //Player inventory
    public int numBullets = 0;
    public int numGrenades = 0;
    public int numMedkits = 0;
    public int numEnergyDrinks = 0;
    public int numResources = 0;
}
