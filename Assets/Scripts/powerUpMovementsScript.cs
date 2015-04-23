using UnityEngine;
using System.Collections;

public class powerUpMovementsScript : MonoBehaviour {

	// Use this for initialization
    public int velocity = 15;
	void Start () {
        rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 1.5f;
        rigidbody2D.fixedAngle = true;
	}
	
	// Update is called once per frame
    int x = 0;
	void Update () {
        x++;
        if (x >= 500)
        {
            //transform.Rotate(0, 0, Random.rotation.z * 100);
            /*I didn't use the rotation method
             * because I wanted the angle to stay the same
             * even when collided with other
             * game objects
             */
            Vector2 randomForce = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            rigidbody2D.AddForce(randomForce);
        }
	}
}
