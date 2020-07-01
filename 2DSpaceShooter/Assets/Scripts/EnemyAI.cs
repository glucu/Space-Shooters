using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    const float enemyRange = 0.0f;  // <-- Change this when ready
    public GameObject player;
    
    void Start()
    {
        
    }

    private void CalculateDistance()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 enemyPosition = this.transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow(enemyPosition.x - playerPosition.x, 2) +
                         Mathf.Pow(enemyPosition.y - playerPosition.y, 2));

        Debug.Log("Distance: " + distance);

        if(distance >= enemyRange) { }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
        }
    }
}
