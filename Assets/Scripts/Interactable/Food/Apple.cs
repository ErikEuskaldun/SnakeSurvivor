using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Apple : Food, IInteractable
{
    [SerializeField] GameObject rottenApple;

    public override void Instantiate()
    {
        base.Instantiate();
        OnDespawnEvent.AddListener(SpawnRottenApple);
    }

    private void SpawnRottenApple(Vector2Int position)
    {
        Food food = Instantiate(rottenApple, Vector3.up * 100, Quaternion.identity, this.transform.parent.transform).GetComponent<Food>();
        food.Instantiate();
        food.SetPosition(position);
    }
}
