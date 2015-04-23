using UnityEngine;
using System.Collections;

public class LightFrigateScript : MonoBehaviour {

    //referers
    public GameObject player;
    public GameObject bullet;
    //firing points
    public Transform leftFiringPoint1;
    public Transform leftFiringPoint2;
    public Transform leftFiringPoint3;
    public Transform rightFiringPoint1;
    public Transform rightFiringPoint2;
    public Transform rightFiringPoint3;
    //instance vars
    public int health = 1300;
    public int rotationSpeed = 10;
    public float velocity = 40.0f;
    public int firingInterval;
	// Use this for initialization
	void Start () 
    {
	
	}

    bool TargetIsClose()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance <= 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!(col.gameObject.tag == "Bullet") && col.gameObject.tag == "Player")
        {
            Rigidbody2DExtension.AddExplosionForce(col.rigidbody, 5000.0f, transform.position, 1000.0f);
        }
    }
    void ChaseTarget()
    {
        if (TargetIsClose())//If target is close, initiate side firing
        {
            velocity = 15.0f;
            var direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, transform.up), rotationSpeed * Time.deltaTime * 20.0f);
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            FireAtTarget();
        }
        else//follow player in a straight line with haste but do not attack
        {
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.1f);
            velocity = 60.0f;
            //must be able to rotate in a random direction
        }
    }
    int x = 0;//Change this to time
    void FireAtTarget()
    {
        x++;
        if (x >= firingInterval)
        {
            var relativePoint = transform.InverseTransformPoint(player.transform.position);
            if (relativePoint.x > 0.0)
            {
                Instantiate(bullet, leftFiringPoint1.position, leftFiringPoint1.rotation);
                Instantiate(bullet, leftFiringPoint2.position, leftFiringPoint2.rotation);
                Instantiate(bullet, leftFiringPoint3.position, leftFiringPoint3.rotation);
            }
            else if (relativePoint.x < 0.0)
            {
                Instantiate(bullet, rightFiringPoint1.position, rightFiringPoint1.rotation);
                Instantiate(bullet, rightFiringPoint2.position, rightFiringPoint2.rotation);
                Instantiate(bullet, rightFiringPoint3.position, rightFiringPoint3.rotation);
            }
            x = 0;
        }
    }
	// Update is called once per frame
	void Update () 
    {
        rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 3f;
        ChaseTarget();
	}
}
