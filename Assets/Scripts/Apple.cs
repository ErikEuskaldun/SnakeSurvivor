using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
            snake.GetComponent<SnakeVariables>().IncreasePoints(10);
            snake.GetComponent<SnakeVariables>().IncreaseLenght();
            this.transform.position = new Vector3(Random.Range(-17, 17), Random.Range(-9, 9));
        }
    }
}
