using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        SnakeVariables snakeVariables = snake.GetComponent<SnakeVariables>();

        snakeVariables.IncreasePoints(10);
        snakeVariables.IncreaseLenght();
        snakeVariables.IncreaseSpeed(0.0375f);

        Vector2Int newPosition = GetComponent<GridElement>().grid.GetRandomEmptySpace();
        this.transform.position = SnakeUtils.ConvertToRealPosition(newPosition);
    }
}
