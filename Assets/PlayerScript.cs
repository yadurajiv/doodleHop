using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    Rigidbody2D rb;

    public static Vector2 playerVelocity;
    public static Transform playerTransform;

    public static bool isGameOver;

    // Use this for initialization
    void Start () {

        isGameOver = false;

        rb = GetComponent<Rigidbody2D>();

        playerTransform = transform;
        playerVelocity = rb.velocity;

        rb.velocity = new Vector2(0f, 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
        // if game is over then don't do anything
        if(isGameOver) {
            return;
        }

        /* -- looping in x to the sides -- */

        // get the player's current position on the screen
        var objPos = Camera.allCameras[0].WorldToScreenPoint(transform.position);

        // if we are beyond the width of the camera, then set our x position to 0 (leftmost)
        if (objPos.x > Camera.allCameras[0].pixelWidth) {
            objPos.x = 0;
            transform.position = Camera.allCameras[0].ScreenToWorldPoint(objPos);

        // else if we are going left and off the screen then set it to the width of the screen (right most)
        } else if (objPos.x < 0) {
            objPos.x = Camera.allCameras[0].pixelWidth;
            transform.position = Camera.allCameras[0].ScreenToWorldPoint(objPos);
        }


        // if the player object hits the middle of the screen - y = 0
        if (transform.position.y >= 0f) {
            transform.position = new Vector2(transform.position.x, 0f);
        }


        /* -- movement code -- */

        // move left - clamped to a velocity
        if (Input.GetKey(KeyCode.LeftArrow)) {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - Time.deltaTime * 10.0f, -3, 3), rb.velocity.y);
        }

        // move right - clamped to a velocity
        if (Input.GetKey(KeyCode.RightArrow)) {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + Time.deltaTime * 10.0f, -3, 3), rb.velocity.y);
        }

        // stop moving in  x if any of the keys are up
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }


        /* -- save our position and velocity -- */

        playerTransform = transform;
        playerVelocity = rb.velocity;
    }

    // handling pass through platforms and death
    private void OnTriggerEnter2D(Collider2D collision) {
        // if we hit the bottom collider, then our platform is no longer solid!
        if (collision.name == "bottomHitBox") {
            var rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            var collider = rb.gameObject.GetComponent<BoxCollider2D>();
            collider.isTrigger = true;
        } else if (collision.tag == "Death") {
            isGameOver = true;
            //Destroy(gameObject);
            Invoke("GameRestart", 2f);
        }
    }


    // handling what happens after you exit our passthrough platform
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "bottomHitBox") {
            var rb = collision.gameObject.GetComponentInParent<Rigidbody2D>();
            var collider = rb.gameObject.GetComponent<BoxCollider2D>();
            collider.isTrigger = false;
        }
    }

    // handling floor collisions - jump again if you hit the floor
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            rb.velocity = new Vector2(0f, 8.5f);
        }
    }

    void GameRestart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
