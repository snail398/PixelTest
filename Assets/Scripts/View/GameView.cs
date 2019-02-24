using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    [SerializeField] UnitView unitViewPrefab;
    [SerializeField] BombView bombViewPrefab;
    [SerializeField] public Transform areaTransform;
    [SerializeField] public MeshRenderer[] walls;
    GameController gameController;

    readonly List<UnitView> units = new List<UnitView>();
    readonly List<BombView> bombs = new List<BombView>();
    Pool<UnitView> unitPool;
    Pool<BombView> bombPool;

    public override void Render()
    {
        foreach (var unit in units)
        {
            unit.Render();
        }

        foreach (var bomb in bombs)
        {
            bomb.Render();
        }
    }
    
    public void Init()
    {
        unitPool = new Pool<UnitView>(() => Instantiate(unitViewPrefab), 20);
        bombPool = new Pool<BombView>(() => Instantiate(bombViewPrefab), 20);
    }

    public void SetUp(GameController gameController)
    {
        this.gameController = gameController;
        this.gameController.OnUnitSpawned += SpawnUnitView;
        this.gameController.OnBombSpawned += SpawnBombView;

        this.gameController.OnUnitDestroyed += DestroyUnitView;
        this.gameController.OnBombDestroyed += DestroyBombView;
    }

    void SpawnUnitView(UnitController controller)
    {
        var unitView = unitPool.Pick();
        unitView.SetUp(controller);
        units.Add(unitView);
    }
    void SpawnBombView(BombController controller)
    {
        var bombView = bombPool.Pick();
        bombView.SetUp(controller);
        bombs.Add(bombView);
    }

    void DestroyUnitView(UnitController controller)
    {
        var unitView = units.Find(u => u.Controller == controller);
        if (unitView != null)
            unitView.Return();
    }

    void DestroyBombView(BombController controller)
    {
        var bombView = bombs.Find(b => b.Controller == controller);
        if (bombView != null)
            bombView.Return();
    }
}
