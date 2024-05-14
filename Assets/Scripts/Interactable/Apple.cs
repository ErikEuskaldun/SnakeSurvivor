using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        snake.GetComponent<SnakeVariables>().IncreasePoints(10);
        snake.GetComponent<SnakeVariables>().IncreaseLenght();

        Vector2Int newPosition = GetComponent<GridElement>().grid.GetRandomEmptySpace();
        this.transform.position = SnakeUtils.ConvertToRealPosition(newPosition);
    }
}
