using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

    public float speed = 10.0f;
    public int timeLife = 50;//lifetime for the bullet
	// Use this for initialization

	void Start () {
        rigidbody2D.velocity = transform.up * Time.deltaTime * speed * 125;
	}
	// Update is called once per frame
    int x = 0;
	void Update () {
        DestroyObject();
	}
    void DestroyObject()
    {
        x++;
        if (x >= timeLife)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)//object specific gameobject destruction
    {
        if (!(col.gameObject.tag == "Bullet") && !(col.gameObject.name == "frigateBulletRed(Clone)") || (col.gameObject.name == this.gameObject.name))//Destroy when it encounters a non-bullet and non-frigate bullet
        {
            Destroy(gameObject);
        }
    }
}
