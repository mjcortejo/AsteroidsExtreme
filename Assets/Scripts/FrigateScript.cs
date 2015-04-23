using UnityEngine;
using System.Collections;

public class FrigateScript : MonoBehaviour {

	// Use this for initialization
    public GameObject player;
    public GameObject bullet;
    //firing points
    public Transform frontFiringPoint;
    public Transform leftFiringPoint1;
    public Transform leftFiringPoint2;
    public Transform leftFiringPoint3;
    public Transform rightFiringPoint1;
    public Transform rightFiringPoint2;
    public Transform rightFiringPoint3;
    //instance variables
    public int health = 2000;
    public int rotationSpeed = 10;
    public float velocity = 40.0f;
    public int firingInterval;
    //privates
    bool isInContact = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 3f;
        ChaseTarget();
        OnCollisionFire();
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!(col.gameObject.tag == "Bullet") && col.gameObject.tag == "Player")
        {
            Rigidbody2DExtension.AddExplosionForce(col.rigidbody, 5000.0f, transform.position, 1000.0f);
        }
    }
    void OnCollisionFire()
    {
        if (isInContact)
        {
            FireBothSides();
        }
    }
    int fCtr = 0;
    void FireBothSides()
    {
        fCtr++;
        //if (fCtr >= firingInterval)
        if (fCtr >= 10)
        {
            Instantiate(bullet, leftFiringPoint1.position, leftFiringPoint1.rotation);
            Instantiate(bullet, leftFiringPoint2.position, leftFiringPoint2.rotation);
            Instantiate(bullet, leftFiringPoint3.position, leftFiringPoint3.rotation);
            Instantiate(bullet, rightFiringPoint1.position, rightFiringPoint1.rotation);
            Instantiate(bullet, rightFiringPoint2.position, rightFiringPoint2.rotation);
            Instantiate(bullet, rightFiringPoint3.position, rightFiringPoint3.rotation);
            Instantiate(bullet, frontFiringPoint.position, frontFiringPoint.rotation);
            fCtr = 0;
        }
    }
    bool TargetIsClose()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance <= 20)
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
            velocity = 10.0f;
            var direction = (player.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, transform.up), rotationSpeed * Time.deltaTime* 10.0f);
            transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            FireAtTarget();
        }
        else//follow player in a straight line with haste but do not attack
        {
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.05f);
            velocity = 75.0f;
            //must be able to rotate in a random direction
        }
    }
    int x = 0;
    void FireAtTarget()
    {
        x++;
        if (x >= firingInterval)
        {
            var relativePoint = transform.InverseTransformPoint(player.transform.position);
            if (relativePoint.x < 0.0)
            {
                Instantiate(bullet, leftFiringPoint1.position, leftFiringPoint1.rotation);
                Instantiate(bullet, leftFiringPoint2.position, leftFiringPoint2.rotation);
                Instantiate(bullet, leftFiringPoint3.position, leftFiringPoint3.rotation);
            }
            else if (relativePoint.x > 0.0)
            {
                Instantiate(bullet, rightFiringPoint1.position, rightFiringPoint1.rotation);
                Instantiate(bullet, rightFiringPoint2.position, rightFiringPoint2.rotation);
                Instantiate(bullet, rightFiringPoint3.position, rightFiringPoint3.rotation);
            }
            else
            {
                Instantiate(bullet, frontFiringPoint.position, frontFiringPoint.rotation);
            }
            x = 0;
        }
    }
}

public static class Rigidbody2DExtension //explosion force extension (src: Swamy site: http://forum.unity3d.com/threads/need-rigidbody2d-addexplosionforce.212173/)
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * explosionForce * wearoff);
    }

    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * explosionForce * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }
}

