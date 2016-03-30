using UnityEngine;
using System;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

    public int hp = 1;

    // maximum linear speed
    public float speed = 5;
    public float moveForce = 1500;
    public Sprite deadSprite;

    // layers the enemy can't see through
    public LayerMask wallLayers;

    private bool playerInROI = false;

    private Vector2 heading;
    private Rigidbody2D rb2D;
    private SpriteRenderer spr;
    private Transform player;

	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        if(playerInROI)
        Debug.DrawRay(transform.position, -heading);
    }

    void FixedUpdate()
    {
        if (hp > 0 && playerInROI)
        {
            if (!Physics2D.Linecast(transform.position, player.position, wallLayers))
            {
                heading = transform.position - player.position;
                float angle = 0;
                if (heading.x != 0)
                    angle = Mathf.Atan2(heading.x, -heading.y) * Mathf.Rad2Deg;
                rb2D.rotation = angle;

                Mathf.Clamp(rb2D.angularVelocity, 0, 0);
                rb2D.AddRelativeForce(Vector2.up * moveForce * Time.deltaTime);
                Vector2.ClampMagnitude(rb2D.velocity, speed);
            }
        } else if (hp == 0) {
            rb2D.isKinematic = true;
            spr.sprite = deadSprite;
            Destroy(GetComponent<Collider2D>());
            GetComponent<EnemyBehavior>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!player) player = other.transform;
            playerInROI = true;
            Debug.Log(gameObject.name + "ROI: " +  playerInROI);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerInROI = false;
            Debug.Log(gameObject.name + "ROI: " +  playerInROI);
        }
    }
}