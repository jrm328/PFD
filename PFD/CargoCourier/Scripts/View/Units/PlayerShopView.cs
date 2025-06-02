using UnityEngine;
using System.Collections;

public class PlayerShopView : UnitView
{
    private void Start()
    {
        unitAnimation.IsInShop = true;
    }

    protected override void AddCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = true;
        boxCollider.center = new Vector3(0f, 0.5f, 0f);
    }

    protected override void AddRigidBody()
    {
    }
}
