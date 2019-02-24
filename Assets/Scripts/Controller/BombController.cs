using System;
using System.Collections;
using System.Collections.Generic;

public class BombController 
{
    public Bomb bomb;
    GameController gameController;
    bool detonated = false;

    public BombController(Bomb bomb, GameController gameController)
    {
        this.bomb = bomb;
        this.gameController = gameController;
    }

    public void UpdateTimeToDetonate(float deltaTime, List<UnitController> unitsList)
    {
        bomb.timeToDetonate -= deltaTime;
        
        if (bomb.timeToDetonate <= 0 && !detonated)
        {
            detonated = true;
            Detonate(unitsList);
        }
    }

    public void Detonate(List<UnitController> activeUnits)
    {
        for (var i = 0; i < activeUnits.Count; i++)
        {
            if (CanDamage(activeUnits[i]))
            {
                if (!CheckWall(activeUnits[i]))
                {
                    activeUnits[i].unit.health -= (int)bomb.damage;
                    if (activeUnits[i].unit.health <= 0)
                    gameController.DestroyUnit(activeUnits[i]);
                }
            }
        }
    }

    bool CanDamage(UnitController unitController)
    {
        var distance = Vector.Distance(bomb.position, unitController.unit.position);
        if (distance <= bomb.damageRadius)
        {
            return true;
        }
        else return false;
    }

    bool CheckWall(UnitController unitController)
    {
        var distance = Vector.Distance(bomb.position, unitController.unit.position);
        Vector minus = bomb.position - unitController.unit.position;
        float cos = (float)Math.Sqrt(1 / (1 + Math.Pow(Math.Abs(minus.Y) / Math.Abs(minus.X), 2)));
        float sin = (float)Math.Sqrt(1 / (1 + Math.Pow(Math.Abs(minus.X) / Math.Abs(minus.Y), 2)));
        foreach (var wall in gameController.walls)
        {
            Vector leftBottomCorner = new Vector(wall.center.X - wall.extends.X, wall.center.Y - wall.extends.Y);
            Vector rightTopCorner = new Vector(wall.center.X + wall.extends.X, wall.center.Y + wall.extends.Y);
            var step = Math.Min(wall.extends.X,wall.extends.Y)*2*0.8f;
            int count = (int)Math.Floor(distance / step);
            for (int i = 1; i < count; i++)
            {
                Vector point = new Vector(bomb.position.X - Math.Sign(minus.X)*i*step*cos,bomb.position.Y - Math.Sign(minus.X) * i * step * sin);
                if (point.X > leftBottomCorner.X && point.X < rightTopCorner.X && point.Y > leftBottomCorner.Y && point.Y < rightTopCorner.Y) return true;
            }
        }
        return false;
    }

    public void DestroyBomb()
    {
        gameController.DestroyBomb(this);
    }

}
