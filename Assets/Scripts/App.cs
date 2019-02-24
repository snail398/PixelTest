using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField] GameView gameView;
    GameController gameController;

    void Awake()
    {
        StartGame(new GameState());
        gameView.Init();
    }

    public void StartGame(GameState gameState)
    {
        List<Wall> walls = new List<Wall>();
        foreach (var meshWall in gameView.walls)
        {
            Vector center = new Vector(meshWall.bounds.center.x, meshWall.bounds.center.y);
            Vector extends = new Vector(meshWall.bounds.extents.x, meshWall.bounds.extents.y);
            walls.Add(new Wall(center,extends));
        }
        gameController = new GameController(gameState,gameView.areaTransform.localScale.x, gameView.areaTransform.localScale.z,walls);
        gameView.SetUp(gameController);
    }

    private void Update()
    {
        gameController.Tick(Time.deltaTime);
        gameView.Render();
    }


}
