using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{
    // Use this for initialization
    #region variables
    //ship variables
    public int speed = 100;
    public float rotationSpeed = 5.0f;
    public float maxSpeed = 5.0f;
    public int fireInterval = 5;
    //
    //screenwrap variables
    Renderer[] renderers;
    bool isWrappingX = false;
    bool isWrappingY = false;
    //
    //Projectile
    public GameObject bullet;
    public GameObject bulletGreen;

    public Transform firingPoint;
    public Transform sideFiringPointLeft;
    public Transform sideFiringPointRight;
    #endregion
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }
    bool CheckRenderers()
    {
        foreach (var renderer in renderers)
        {
            //if at least one render is visible, return true
            if (renderer.isVisible)
            {
                return true;
            }
        }
        //OTherwise, the object is invisible
        return false;
    }
    void ScreenWrap()
    {
        var isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);

        var newPosition = transform.position;

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }
        transform.position = newPosition;
    }
    // Update is called once per frame
    void Update()
    {
        Controls();
        Fire();
        PowerUpCountDown();
        //ScreenWrap();
        //DebugLogs();
    }
    void Controls()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetButton("Vertical");

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        if (v)
        {
            rigidbody2D.AddForce(transform.up * Time.deltaTime * speed * Input.GetAxis("Vertical") * 0.5f);
        }
        rigidbody2D.AddForce(transform.right * Time.deltaTime * speed * h * 0.5f);
        //control maximum speed (Doesn't seem to be working)
        if (rigidbody2D.velocity.magnitude > maxSpeed)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
        }   
    }
    float cDownBullet;//countdown timer for the bullet
    float cDownSpeed;//countdown timer for the speed
    void PowerUpCountDown()//All countdown functions here
    {
        BulletPowerUpCountDown();
        SpeedPowerUpCountDown();
    }
    #region Powerup Specifics functions
    private void BulletPowerUpCountDown()
    {
        if (gotPowerUpBullet)
        {
            cDownBullet -= Time.deltaTime;
            Debug.Log(cDownBullet);
            fireInterval = 3;
        }
        if (cDownBullet <= 0)
        {
            cDownBullet = 15;
            gotPowerUpBullet = false;
            fireInterval = 5;
        }
    }
    private void SpeedPowerUpCountDown()
    {
        if (gotPowerUpSpeed)
        {
            cDownSpeed -= Time.deltaTime;
            speed = 500;
        }
        if (cDownSpeed <= 0)
        {
            cDownSpeed = 10;
            gotPowerUpSpeed = false;
            speed = 100;
        }
    }
    #endregion
    int x = 0;
    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            //fire bullet upwards relative to firingPoint position
            x++;
            if (x >= fireInterval)
            {
                if (!gotPowerUpBullet)//if no power up
                {
                    FireNormalBullet();
                }
                else if (gotPowerUpBullet)//else
                {
                    FireGreenBullet();
                }
                x = 0;//resets interval to 0 upon fired
            }
        }
    }
    private void FireNormalBullet()
    {
        Instantiate(bullet, firingPoint.position, transform.rotation);
    }
    private void FireGreenBullet()
    {
        //Instantiate(bulletGreen, firingPoint.position, transform.rotation);
        for (int i = 0; i < 3; i++)
        {
            switch (i)
            {
                case 0:
                    Instantiate(bulletGreen, firingPoint.position, firingPoint.rotation);
                    break;
                case 1:
                    Instantiate(bulletGreen, sideFiringPointLeft.position, sideFiringPointLeft.rotation);
                    break;
                case 2:
                    Instantiate(bulletGreen, sideFiringPointRight.position, sideFiringPointRight.rotation);
                    break;
                default:
                    break;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        RecievePowerUp(col);
    }
    bool gotPowerUpBullet = false;
    bool gotPowerUpSpeed = false;
    void RecievePowerUp(Collision2D col)
    {
        if (col.gameObject.name == "PowerUp Blue(Clone)")
        {
            Destroy(col.gameObject);
            gotPowerUpBullet = true;
            cDownBullet = 15;
            //Modify projectiles here
        }
        if (col.gameObject.name == "PowerUp Yellow(Clone)")
        {

            Destroy(col.gameObject);
            gotPowerUpSpeed = true;
            cDownSpeed = 10;
        }
    }
    void DebugLogs()
    {
        //Debug.Log(rigidbody2D.velocity);
    }
}
