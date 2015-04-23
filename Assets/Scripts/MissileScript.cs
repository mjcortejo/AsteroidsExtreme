using UnityEngine;
using System.Collections;

public class MissileScript : MonoBehaviour {

    public float speed = 1.0f;
    public int timeLife = 50;//lifetime for the bullet
    public int rotationSpeed = 10;
    public GameObject target;
    // Use this for initialization

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        DestroyObject();
        ChaseTarget();
    }
    int x = 0;
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
        if (!(col.gameObject.tag == "Bullet"))
        {
            Destroy(gameObject);
        }
    }
    void ChaseTarget()
    {
        Vector3 dir = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.20f);
        //rigidbody2D.AddForce(transform.up * Time.deltaTime * speed * 0.005f);
        rigidbody2D.velocity = transform.up * Time.deltaTime * speed * 50.0f;
    }
}
