using EnemyBounding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyBounding
{
    // Configuration parameters
    [System.Serializable]
    public class Boundary
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }
}
public class EnemyDodge : MonoBehaviour
{
    public float dodgeAmt;
    public float tilt;
    public float smooth;

    public Boundary boundary;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float currentSpeed;
    private float maneuverTarget;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentSpeed = rb.velocity.y;

        StartCoroutine(Evade());
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, maneuverTarget, Time.deltaTime * smooth);
        rb.velocity = new Vector2(newManeuver, currentSpeed);

        // Limit player's boundary 
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                                          Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax), transform.position.z);

        // Add tilting to Enemy when moving on the x axis
        var angle = Mathf.Atan2(rb.position.y, rb.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, rb.velocity.x * -tilt, 0.0f);
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)   // (Exits Internally)
        {
            maneuverTarget = Random.Range(1, dodgeAmt) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

            maneuverTarget = 0;

            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
}
