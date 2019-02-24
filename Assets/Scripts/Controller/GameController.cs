using System;
using System.Collections.Generic;

public class GameController
{

    public event Action<UnitController> OnUnitSpawned;
    public event Action<BombController> OnBombSpawned;

    public event Action<UnitController> OnUnitDestroyed;
    public event Action<BombController> OnBombDestroyed;

    readonly GameState gameState;
    List<UnitController> activeUnits = new List<UnitController>();
    List<BombController> activeBomb = new List<BombController>();

    float unitSpawnTimer;
    float bombSpawnTimer;
    float widthSpawn;
    float heightSpawn;
    float modelOffset = 1;
    public List<Wall> walls;

    public GameController(GameState gameState, float widthSpawn, float heightSpawn, List<Wall> walls)
    {
        this.gameState = gameState;
        this.walls = walls;
        this.widthSpawn = widthSpawn/2-1;
        this.heightSpawn = heightSpawn/2-1;
    }

    public void Tick(float deltaTime)
    {
        ProcessSpawnSequence(deltaTime);
        UpdatePosition(deltaTime);
        UpdateBomb(deltaTime,activeUnits);
    }

    void ProcessSpawnSequence(float deltaTime)
    {
        unitSpawnTimer += deltaTime;
        bombSpawnTimer += deltaTime;
        if (unitSpawnTimer >= 2)
        {
            unitSpawnTimer = 0;
            Random rand = new Random();
            Vector pos;
            do {
                float x = rand.Next((int)-widthSpawn, (int)widthSpawn);
                float y = rand.Next((int)-heightSpawn, (int)heightSpawn);
                pos = new Vector(x, y);
            }
            while (IsOverlapping(pos));
            SpawnUnit(new Unit(pos, 100));
        }
        if (bombSpawnTimer >= 1)
        {
            bombSpawnTimer = 0;
            Random rand = new Random();
            Vector pos;
            do
            {
                float x = rand.Next((int)-widthSpawn, (int)widthSpawn);
                float y = rand.Next((int)-heightSpawn, (int)heightSpawn);
                pos = new Vector(x, y);
            }
            while (IsOverlapping(pos));
            SpawnBomb(new Bomb(pos, 40, 10, 3, 3));
        }
    }
    public void SpawnUnit(Unit unit)
    {
        var unitController = new UnitController(unit);
        gameState.unitList.Add(unit);
        activeUnits.Add(unitController);
        if (OnUnitSpawned != null)
            OnUnitSpawned(unitController);
    }

    public void SpawnBomb(Bomb bomb)
    {
        var bombController = new BombController(bomb,this);
        gameState.bombList.Add(bomb);
        activeBomb.Add(bombController);
        if (OnBombSpawned != null)
            OnBombSpawned(bombController);
    }

    public void UpdatePosition(float deltaTime)
    {
        for (int i = 0; i < activeUnits.Count; i++)
        {
            activeUnits[i].UpdatePosition(deltaTime);
        }
    }

    public void UpdateBomb(float deltaTime, List<UnitController> unitsList)
    {
        for (int i = 0; i < activeBomb.Count; i++)
        {
            activeBomb[i].UpdateTimeToDetonate(deltaTime, unitsList);
        }
    }

    public void DestroyBomb(BombController bombController)
    {
        gameState.bombList.RemoveAll(b=>b==bombController.bomb);
        activeBomb.RemoveAll(b=>b==bombController);
        if (OnBombDestroyed != null)
            OnBombDestroyed(bombController);
    }

    public void DestroyUnit(UnitController unitController)
    {
        gameState.unitList.RemoveAll(u => u == unitController.unit);
        activeUnits.RemoveAll(u => u == unitController);
        if (OnUnitDestroyed != null)
            OnUnitDestroyed(unitController);
    }

    bool IsOverlapping(Vector pos)
    {
        bool flag = false;
        foreach (var unit in activeUnits)
        {
            var distance = Vector.Distance(pos, unit.unit.position);
            if (distance < modelOffset) flag = true;
        }
        foreach (var bomb in activeBomb)
        {
            var distance = Vector.Distance(pos, bomb.bomb.position);
            if (distance < modelOffset) flag = true;
        }
        foreach (var wall in walls)
        {
            Vector leftBottomCorner = new Vector(wall.center.X-wall.extends.X, wall.center.Y - wall.extends.Y);
            Vector rightTopCorner = new Vector(wall.center.X + wall.extends.X, wall.center.Y + wall.extends.Y);
            if (pos.X+ modelOffset > leftBottomCorner.X && pos.X- modelOffset < rightTopCorner.X && pos.Y+ modelOffset > leftBottomCorner.Y && pos.Y- modelOffset < rightTopCorner.Y)
                flag = true;
        }
        return flag;
    }
}
