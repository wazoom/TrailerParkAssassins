using UnityEngine;
using System.Collections;

public class PlayerMoveTest : MonoBehaviour {

    public float speed = 5;
    public float moveForce = 10;
    private Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 f;
        f.x = x * moveForce * Time.deltaTime;
        f.y = y * moveForce * Time.deltaTime;

        rb2D.AddRelativeForce(f);
        Vector2.ClampMagnitude(rb2D.velocity, speed);
	}
}
