using System;

[Serializable]
public class Wall
{
    public Vector center;
    public Vector extends;

    public Wall (Vector center, Vector extends)
    {
        this.center = center;
        this.extends = extends;
    }
}
