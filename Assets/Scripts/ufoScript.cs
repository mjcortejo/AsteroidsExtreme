using UnityEngine;
using System.Collections;

public class ufoScript : MonoBehaviour {

	// Use this for initialization
    public int health = 300;
    public int velocity = 40;
    public float rotationSpeed = 2000.0f;
    public int firingInterval = 35;
    //firing point
    public GameObject bullet;
    public Transform firingPoint;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 3f;
        ChaseTarget();
	}

    void OnCollisionEnter2D(Collision2D col)//the entity should recieve damage from bullet
    {
        ChangeHealth(col);
    }

    void ChangeHealth(Collision2D col)//module for modifying health
    {
        switch (col.gameObject.name)
        {
            case "bullet(Clone)":
                health -= 10;
                break;
            case "ufoBulletRed(Clone)":
                health -= 3;
                break;
            case "bulletGreen(Clone)":
                health -= 15;
                break;
            case "frigateBulletRed(Clone)":
                health -= 30;
                break;
            default:
                health -= 5;
                break;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            //particle system here once destroyed
        }
    }
    public GameObject player;
    bool TargetIsClose()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        var distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance >= 3 && distance <= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int movementInterval = 1000;
    int mICtr = 0;//Movement interval coutner
    void ChaseTarget()
    {
        if (TargetIsClose())// When target is in sufficient distance
        {
            Vector3 dir = player.transform.position - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.AngleAxis(angle - 90.0f, Vector3.forward), Time.deltaTime * rotationSpeed);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
            FireAtTarget();
        }
        else//interval for when it changes its course
        {
            mICtr++;
            if (mICtr >= movementInterval)
            {
                transform.Rotate(0, 0, Random.rotation.z * 100);
                mICtr = 0;
            }
        }
    }
    int x = 0;
    void FireAtTarget()
    {
        x++;
        if (x >= firingInterval)
        {
            Instantiate(bullet, firingPoint.position, transform.rotation);
            x = 0;
        }
    }
}
    