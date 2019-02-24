using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Bomb 
{
    public Vector position;
    public float damage;
    public float damageRadius;
    public float exploseTime;
    public float timeToDetonate;

    public Bomb(Vector position, float damage, float damageRadius, float exploseTime, float timeToDetonate)
    {
        this.position = position;
        this.damage = damage;
        this.damageRadius = damageRadius;
        this.exploseTime = exploseTime;
        this.timeToDetonate = timeToDetonate;
    }
}
