using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : Interactable
{
    public FoodScriptable scriptable;
    public UnityEvent<Vector2Int> OnDespawnEvent;
    public UnityEvent OnInteractEvent;

    public virtual void Instantiate()
    {
        StartCoroutine(Despawn());
    }

    public void SetRandomPosition()
    {
        Vector2Int newPosition = GetComponent<GridElement>().grid.GetRandomEmptySpace();
        SetPosition(newPosition);
    }

    public void SetPosition(Vector2Int position)
    {
        this.transform.position = SnakeUtils.ConvertToRealPosition(position);
        GetComponent<GridElement>().position = position;
    }

    protected virtual IEnumerator Despawn()
    {
        yield return new WaitForSeconds(scriptable.despawnTime * GameVariables.foodLifeTimeMultiplier);

        OnDespawnEvent.Invoke(GetComponent<GridElement>().position);

        Destroy(this.gameObject);
    }

    public override void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        SnakeVariables snakeVariables = snake.GetComponent<SnakeVariables>();
        snakeVariables.IncreasePointsAndTailWithFX(this.transform.position, scriptable.points, scriptable.tailLength);
        //snakeVariables.IncreaseSpeed(scriptable.speed);

        OnInteractEvent.Invoke();

        GameObject.Destroy(this.gameObject);
    }
}
