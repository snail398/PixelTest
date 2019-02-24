using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameState
{
    public List<Unit> unitList;
    public List<Bomb> bombList;

    public GameState()
    {
        unitList = new List<Unit>();
        bombList = new List<Bomb>();
    }
}
