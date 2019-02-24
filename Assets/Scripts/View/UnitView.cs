using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : View, IPoolable
{
    UnitController unitController;

    public UnitController Controller
    {
        get { return unitController; }
    }

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material[] materials;
    
    public override void Render()
    {
        if (unitController == null)
            return;
        transform.position = new Vector3(unitController.unit.position.X, unitController.unit.position.Y);
        if (unitController.unit.health <= 100)
        {
            meshRenderer.sharedMaterial = materials[0];
        }
        if (unitController.unit.health <= 60)
        {
            meshRenderer.sharedMaterial = materials[1];
        }
        if (unitController.unit.health <= 20)
        {
            meshRenderer.sharedMaterial = materials[2];
        }
    }

    public void SetUp(UnitController unitController)
    {
        this.unitController = unitController;
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
