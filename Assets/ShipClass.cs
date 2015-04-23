using UnityEngine;
using System.Collections;

public class ShipClass : MonoBehaviour {

    //refering game objects
    public GameObject target;
    public GameObject bullet;
    //instance vars
    public int health;
    public int rotationSpeed;
    public float rotationSpeedMultiplier;
    public float velocity;
    //non blabla vars
    public int distanceRequired;
    public float attackVelocity;
    public float roamVelocity;
    public bool IsSideFiring;

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
        //rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 5f;
	}
    public virtual bool TargetIsClose()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(this.transform.position, target.transform.position);
        if (distance <= distanceRequired)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public virtual void ChaseTarget()
    {
        if (TargetIsClose() && IsSideFiring)//If target is close, initiate side firing
        {
            //velocity = attackVelocity;
            rigidbody2D.velocity = transform.up * Time.deltaTime * attackVelocity * 5.0f;
            rigidbody2D.drag = 0.002f;
            //rigidbody2D.AddForce(transform.up * Time.deltaTime * attackVelocity * 10.0f);
            //var direction = (target.transform.position - transform.position).normalized;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, transform.up), rotationSpeed * Time.deltaTime * 10.0f);
            //transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
            CheckTargetDirection();
        }
        else if (TargetIsClose())
        {
            //velocity = attackVelocity;
            rigidbody2D.drag = 1;
            //rigidbody2D.AddForce(transform.up * Time.deltaTime * attackVelocity * 0.0f);
            Vector3 dir = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * rotationSpeedMultiplier);
        }
        else//follow player in a straight line with haste but do not attack
        {
            //velocity = roamVelocity;
            rigidbody2D.drag = 0.05f;
            rigidbody2D.velocity = transform.up * Time.deltaTime * roamVelocity * 5.0f;
            //rigidbody2D.AddForce(transform.up * Time.deltaTime * roamVelocity * 10.0f);
            Vector3 dir = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed * 0.10f);
            //must be able to rotate in a random direction
        }
    }
    public virtual void CheckTargetDirection()
    {
        var relativePoint = transform.InverseTransformPoint(target.transform.position);
        if (relativePoint.x < 0.0)//left
        {
            Vector3 dir = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 180.0f, Vector3.forward), Time.deltaTime * rotationSpeed * rotationSpeedMultiplier);
        }
        else if (relativePoint.x > 0.0)//Right
        {
            Vector3 dir = target.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed * rotationSpeedMultiplier);
        }
    }
}
