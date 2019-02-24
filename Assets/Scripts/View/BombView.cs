using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombView : View, IPoolable
{
    BombController bombController;
    [SerializeField] SpriteRenderer sprite;
    float alpha = 1;

    public BombController Controller
    {
        get { return bombController; }
    }
    public override void Render()
    {
        if (bombController == null)
            return;
        transform.position = new Vector3(bombController.bomb.position.X, bombController.bomb.position.Y);
        if (bombController.bomb.timeToDetonate <= 0) RenderDetonate();

    }

    void RenderDetonate()
    {
        sprite.transform.localScale = new Vector3(bombController.bomb.damageRadius/5.5f, bombController.bomb.damageRadius/5.5f);
        alpha -= 0.02f;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,alpha);
        sprite.enabled = true;
        if (alpha <= 0) bombController.DestroyBomb();
    }

    public void SetUp(BombController bombController)
    {
        alpha = 1;
        this.bombController = bombController;
        Render();
    }

    public void Init()
    {

    }

    public void Pick()
    {
        gameObject.SetActive(true);
    }

    public bool IsBeingUsed()
    {
        return gameObject.activeSelf;
    }

    public void Return()
    {
        gameObject.SetActive(false);
    }
}
