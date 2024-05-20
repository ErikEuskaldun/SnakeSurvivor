using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour, IInteractable
{
    int points;
    int tailLength;
    float speed;
    float despawnTime;

    public UnityEvent<Vector3> OnDespawnEvent;
    public UnityEvent OnInteractEvent;

    public void Instantiate(FoodScriptable scriptable)
    {
        this.points = scriptable.points;
        this.tailLength = scriptable.tailLength;
        this.speed = scriptable.speed;
        this.despawnTime = scriptable.despawnTime;

        StartCoroutine(Despawn());
    }
    public void SetRandomPosition()
    {
        Vector2Int newPosition = GetComponent<GridElement>().grid.GetRandomEmptySpace();
        this.transform.position = SnakeUtils.ConvertToRealPosition(newPosition);
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(despawnTime);

        OnDespawnEvent.Invoke(transform.position);

        Destroy(this.gameObject);
    }

    public void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        SnakeVariables snakeVariables = snake.GetComponent<SnakeVariables>();

        snakeVariables.IncreasePoints(points);
        snakeVariables.IncreaseLenght(tailLength);
        snakeVariables.IncreaseSpeed(speed);

        OnInteractEvent.Invoke();

        GameObject.Destroy(this.gameObject);
    }
}
