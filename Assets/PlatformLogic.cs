using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLogic : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (!PlayerScript.isGameOver) {
            if (PlayerScript.playerTransform.position.y >= 0) {
                transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * -6.2f);
            }
        }

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Death") {
            Destroy(gameObject);
        }
    }

}
