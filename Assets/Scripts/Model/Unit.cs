using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Unit
{
    public Vector position;
    public int health;

    public Unit(Vector position, int health)
    {
        this.position = position;
        this.health = health;
    }
}
