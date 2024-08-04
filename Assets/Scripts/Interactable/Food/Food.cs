using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
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
        yield return new WaitForSeconds(scriptable.despawnTime);

        OnDespawnEvent.Invoke(GetComponent<GridElement>().position);

        Destroy(this.gameObject);
    }

    public virtual void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        SnakeVariables snakeVariables = snake.GetComponent<SnakeVariables>();

        snakeVariables.IncreasePoints(scriptable.points);
        snakeVariables.IncreaseLenght(scriptable.tailLength);
        //snakeVariables.IncreaseSpeed(scriptable.speed);
        int realPoints = scriptable.points * FindObjectOfType<SnakeVariables>().ComboMultiplier;
        FindObjectOfType<GameUIController>().InstantiatePointFX(this.transform.position, realPoints);

        OnInteractEvent.Invoke();

        GameObject.Destroy(this.gameObject);
    }
}
