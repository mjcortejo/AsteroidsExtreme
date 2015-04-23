using UnityEngine;
using System.Collections;

public class HeavyCruiserScript : MonoBehaviour {

    //objects
    public GameObject player;
    public GameObject missile;
    //firing points
    public Transform leftFiringPoint;
    public Transform rightFiringPoint;
    //instance vars
    public int health = 2500;
    public int rotationSpeed = 10;
    public float velocity = 40.0f;
    public int firingInterval;
	// Use this for initialization
	void Start () {
	
	}
    bool TargetIsClose()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance <= 30)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void ChaseTarget()
    {
        if (TargetIsClose())//If target is close, initiate side firing
        {
            velocity = 0.0f;
            rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 20.0f;
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.1f);
            FireAtTarget();
        }
        else//follow player in a straight line with haste but do not attack
        {
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.7f);
            velocity = 20.0f;
            rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 20.0f;
            //must be able to rotate in a random direction
        }
    }
    int x = 0;
    void FireAtTarget()
    {
        x++;
        if (x == firingInterval)//Aternating fire
        {
            Instantiate(missile, leftFiringPoint.position, leftFiringPoint.rotation);
        }
        if (x >= firingInterval * 2)
        {
            Instantiate(missile, rightFiringPoint.position, rightFiringPoint.rotation);
            x = 0;
        }
    }
	// Update is called once per frame
	void Update () 
    {
        ChaseTarget();
        CheckHealth();
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "bullet(Clone)")
        {
            health -= 10;
        }
        if (col.gameObject.name == "bulletGreen(Clone")
        {
            health -= 30;
        }
    }
    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
