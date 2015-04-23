using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class asteroidBehaviorScript : MonoBehaviour
{

    // Use this for initialization
    public int health = 100;
    Vector3 randDir;
    int velocity;
    prefabsScript pfScript;
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //rigidbody2D.velocity = transform.up * Time.deltaTime * velocity * 3f;
        rigidbody2D.AddForce(transform.up * Time.deltaTime * velocity * 5f);
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
                health -= 25;
                break;
            case "frigateBulletRed(Clone)":
                health -= 50;
                break;
            default:
                health -= 5;
                break;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            SplitAsteroids();
            //particle system here once destroyed
        }
    }
    float cltTime = 300;
    void CheckLifeTime()
    {
        if (cltTime >= 0)
        {
            cltTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Spawn()//random velocity, could be better if smaller asteroids are faster
    {
        //velocity = Random.Range(10, 30);
        switch (gameObject.tag)//Velocities vary on the size of the asteroids
        {
            case "Big Asteroids (Brown)":
            case "Big Asteroids (Grey)":
                velocity = Random.Range(200, 250);
                break;
            case "Medium Asteroids (Brown)":
            case "Medium Asteroids (Grey)":
                velocity = Random.Range(300, 350);
                break;
            case "Small Asteroids (Brown)":
            case "Small Asteroids (Grey)":
                velocity = Random.Range(400, 450);
                break;
            default:
                Debug.Log("Not Listed in the prefabs");
                break;
        }
    }
    /*Fix this part later, spawning of even smaller asteroids from its bigger counterparts
     */
    public List<GameObject> mediumAsteroidSplitPrefabs = new List<GameObject>();
    public List<GameObject> smallAsteroidSplitPrefabs = new List<GameObject>();
    public GameObject[] mediumBrownSplitAsteroids;
    public GameObject[] mediumGreySplitAsteroids;
    public GameObject[] smallBrownSplitAsteroids;
    public GameObject[] smallGreySplitAsteroids;
    void SplitAsteroids()//you should probably create different functions for both medium and small to avoid unnecessary assignments
    {
        switch (gameObject.tag)//If the detected tag is either a big or small, it spawns its smaller versions hehe.
        {
            case "Big Asteroids (Brown)":
                SpawnSplitAsteroids(mediumAsteroidSplitPrefabs, mediumBrownSplitAsteroids, 4);
                break;
            case "Big Asteroids (Grey)":
                SpawnSplitAsteroids(mediumAsteroidSplitPrefabs, mediumGreySplitAsteroids, 4);
                break;
            case "Medium Asteroids (Brown)":
                SpawnSplitAsteroids(smallAsteroidSplitPrefabs, smallBrownSplitAsteroids, 2);
                break;
            case "Medium Asteroids (Grey)":
                SpawnSplitAsteroids(smallAsteroidSplitPrefabs, smallGreySplitAsteroids, 2);
                break;
        }
    }
    void SpawnSplitAsteroids(List<GameObject> asteroidSplitPrefabs, GameObject[] SplitAsteroids, int _numberofSplits)//Spawning of those tiny idiots
    {
        asteroidSplitPrefabs = new List<GameObject>();//refreshes list for new prefabs to take place
        for (int i = 0; i < SplitAsteroids.Length; i++)
        {
            asteroidSplitPrefabs.Add(SplitAsteroids[i]);//add to list
        }
        int preFabIndex = Random.Range(0, SplitAsteroids.Length - 1);
        for (int i = 0; i < _numberofSplits; i++)//instantiates based on the number of splits
        {
            Instantiate(asteroidSplitPrefabs[preFabIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }
}
