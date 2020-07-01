using System.Collections;
using System.Collections.Generic;
using PlayerBounding;
using UnityEngine;


namespace PlayerBounding
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

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float fireRate;
    private float nextFire;

    public Boundary boundary;
    public Rigidbody2D rg;


    public GameObject shot;
    public GameObject shotSpawn;
    public GameObject enemyPos;

   
   


    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    /*CalculateAngleTurn() and Cross() are on hold.
     * Doesn't quite work and it's only a added feature. */
   
    private void CalculateAngleTurn()
    {
        Vector3 playerForward = this.transform.up;
        Vector3 enemyDirection = enemyPos.transform.position - this.transform.position;

        float dot = playerForward.x * enemyDirection.x + playerForward.y * enemyDirection.y;
        float angle = Mathf.Acos(dot / (playerForward.magnitude * enemyDirection.magnitude));

        int clockwise = 1;
        if (Cross(playerForward, enemyDirection).z < 0)
        {
            clockwise = -1;
        }

        angle *= Mathf.Rad2Deg;
        this.transform.Rotate(0.0f, 0.0f, angle * clockwise);
    }

    private Vector3 Cross(Vector3 a, Vector3 b)
    {
        float aMult = a.y * b.z - a.z * b.y;
        float yMult = a.z * b.x - a.x * b.z;
        float zMult = a.x * b.y - a.y * b.x;

        return new Vector3(aMult, yMult, zMult);
    }

    private void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space)) && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
            GetComponent<AudioSource>().Play();

        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {

            CalculateAngleTurn();
        }
    }

    private void FixedUpdate()
    {
        // Extract and store player's input every frame
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        rg.velocity = movement * speed;

        // Limit player's boundary within game
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
                                         Mathf.Clamp(transform.position.y, boundary.yMin, boundary.yMax), transform.position.z);


        // Tilting mechanism for gunship wheile moving on the x-axis
        var angle = Mathf.Atan2(rg.position.y, rg.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, rg.velocity.x * -tilt, 0.0f);
    }
}
