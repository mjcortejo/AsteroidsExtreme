using UnityEngine;
using System.Collections;

public class FrigateShipClass : ShipClass {

    public int firingInterval;
    //firing points
    public Transform[] frontFiringPoint;
    public Transform[] leftFiringPoint;
    public Transform[] rightFiringPoint;
	// Use this for initialization
    public override void Start () 
    {
        Debug.Log(health);
        Debug.Log(velocity);
        Debug.Log(rotationSpeed);
    }
	
    //// Update is called once per frame
    public override void Update () 
    {
        //rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 5f;
        ChaseTarget();
        FireAtTarget();
        CheckHealth();
    }

    void FireAtTarget()
    {
        if (TargetIsClose())
        {
            if (IsSideFiring)
            {
                FireAtSide();
            }
            else if (!IsSideFiring)
            {
                FireAtFront();
            }
        }
    }
    //Fire At Side
    int fasCtr = 0;
    void FireAtSide()
    {
        fasCtr++;
        if (fasCtr >= firingInterval)
        {
            var relativePoint = transform.InverseTransformPoint(target.transform.position);
            if (relativePoint.x < 0.0)
            {
                for (int i = 0; i < leftFiringPoint.Length; i++)
                {
                    Instantiate(bullet, leftFiringPoint[i].position, leftFiringPoint[i].rotation);
                }
            }
            else if (relativePoint.x > 0.0)
            {
                for (int i = 0; i < rightFiringPoint.Length; i++)
                {
                    Instantiate(bullet, rightFiringPoint[i].position, rightFiringPoint[i].rotation);
                }
            }
            fasCtr = 0;
        }
    }
    //FireAtFront
    int fatCtr = 0;
    void FireAtFront()
    {
        fatCtr++;
        if (fatCtr >= firingInterval)
        {
            for (int i = 0; i < frontFiringPoint.Length; i++)
            {
                Instantiate(bullet, frontFiringPoint[i].position, frontFiringPoint[i].rotation);
            }
            fatCtr = 0;
        }
    }
    //Collision with bullets
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
