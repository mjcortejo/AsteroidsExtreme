using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class prefabsScript : MonoBehaviour {

	// Use this for initialization
    List<GameObject> bigAsteroidPrefabs = new List<GameObject>();
    List<GameObject> mediumAsteroidPrefabs = new List<GameObject>();
    List<GameObject> smallAsteroidPrefabs = new List<GameObject>();
    List<GameObject> ufoPrefabs = new List<GameObject>();
    List<GameObject> powerupPrefabs = new List<GameObject>();
    List<GameObject> lightfrigatePrefabs = new List<GameObject>();
    List<GameObject> frigatePrefabs = new List<GameObject>();
    List<GameObject> battlecruiserPrefabs = new List<GameObject>();
    public GameObject[] bigAsteroids;
    public GameObject[] mediumAsteroids;
    public GameObject[] smallAsteroids;
    public GameObject[] ufo;
    public GameObject[] powerups;
    public GameObject[] lightfrigate;
    public GameObject[] frigate;
    public GameObject[] battlecruiser;


	public void Start () {
        InitializeSpawnPositions();
        //Big Asteroids
        for (int i = 0; i < bigAsteroids.Length; i++)
        {
            bigAsteroidPrefabs.Add(bigAsteroids[i]);
        }
        //Medium Asteroids
        for (int i = 0; i < mediumAsteroids.Length; i++)
        {
            mediumAsteroidPrefabs.Add(mediumAsteroids[i]);
        }
        //Small Asteroids
        for (int i = 0; i < smallAsteroids.Length; i++)
        {
            smallAsteroidPrefabs.Add(smallAsteroids[i]);
        }
        //Ufos
        for (int i = 0; i < ufo.Length; i++)
        {
            ufoPrefabs.Add(ufo[i]);
        }
        //Powerups
        for (int i = 0; i < powerups.Length; i++)
        {
            powerupPrefabs.Add(powerups[i]);
        }
        //Light Frigate
        for (int i = 0; i < lightfrigate.Length; i++)
        {
            lightfrigatePrefabs.Add(lightfrigate[i]);
        }
        //Frigate
        for (int i = 0; i < frigate.Length; i++)
        {
            frigatePrefabs.Add(frigate[i]);
        }
        //BattleCruiser
        for (int i = 0; i < battlecruiser.Length; i++)
        {
            battlecruiserPrefabs.Add(battlecruiser[i]);
        }
	}
    int prefabIndex;
    #region Instantiate Game Objects
    public void InstantiateBigAsteroid(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, bigAsteroids.Length - 1);
        Instantiate(bigAsteroidPrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateMediumAsteroid(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, mediumAsteroids.Length - 1);
        Instantiate(mediumAsteroidPrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateSmallAsteroid(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, smallAsteroids.Length - 1);
        Instantiate(smallAsteroidPrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateUFO(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, ufo.Length - 1);
        Instantiate(ufoPrefabs[prefabIndex], position, rotation);
    }
    public void InstantiatePowerUps(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, powerups.Length - 1);
        Instantiate(powerupPrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateLightFrigates(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, lightfrigate.Length - 1);
        Instantiate(lightfrigatePrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateFrigates(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, frigate.Length - 1);
        Instantiate(frigatePrefabs[prefabIndex], position, rotation);
    }
    public void InstantiateBattleCruisers(Vector2 position, Quaternion rotation)
    {
        prefabIndex = Random.Range(0, battlecruiser.Length - 1);
        Instantiate(battlecruiserPrefabs[prefabIndex], position, rotation);
    }
    Vector2[] spawnPos = new Vector2[5];
    public void InitializeSpawnPositions()
    {
        //Spawn Objects outside of camera viewport but in 6 points only
        spawnPos[0].x = 1.7f;//Proportional values because yes
        spawnPos[0].y = 1.7f;
        spawnPos[1].x = -0.7f;
        spawnPos[1].y = 1.7f;
        spawnPos[2].x = 1.7f;
        spawnPos[2].y = -0.7f;
        spawnPos[3].x = -0.7f;
        spawnPos[3].y = -0.7f;
        spawnPos[4].x = 0.7f;
        spawnPos[4].y = 0.7f;
    }
    #endregion
    int selectedVectorX;
    int selectedVectorY;
    public void SpawnBigAsteroid()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateBigAsteroid(vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            //Instantiate(asteroid, vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }
    public void SpawnMediumAsteroid()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateMediumAsteroid(vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            //Instantiate(asteroid, vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }
    public void SpawnSmallAsteroid()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateSmallAsteroid(vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }
    public void SpawnUfo()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateUFO(vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            //Instantiate(asteroid, vpos, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        }
    }
    public void SpawnPowerUps()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiatePowerUps(vpos, Quaternion.Euler(new Vector3(0, 0, 0))); //original z value = 360
        }
    }
    public void SpawnLightFrigates()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateLightFrigates(vpos, Quaternion.Euler(new Vector3(0, 0, 0))); //original z value = 360
        }
    }
    public void SpawnFrigates()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateFrigates(vpos, Quaternion.Euler(new Vector3(0, 0, 0))); //original z value = 360
        }
    }
    public void SpawnBattleCruisers()
    {
        selectedVectorX = Random.Range(0, 5);
        selectedVectorY = Random.Range(0, 5);

        if (!(selectedVectorX == selectedVectorY))//if the randomrange both selected the middle coordinates to avoid spawning in the middle of the camera
        {
            Vector2 vpos = Camera.main.ViewportToWorldPoint(new Vector2(spawnPos[selectedVectorX].x, spawnPos[selectedVectorY].y));
            InstantiateBattleCruisers(vpos, Quaternion.Euler(new Vector3(0, 0, 0))); //original z value = 360
        }
    }
	// Update is called once per frame
    int[] timers = new int[8];
	void Update () {
        for (int i = 0; i < timers.Length; i++)
        {
            timers[i]++;
        }
        if (timers[0] >= 180)//Big Asteroids
        {
            SpawnBigAsteroid();
            timers[0] = 0;
        }
        if (timers[1] >= 100)//Medium Asteroids
        {
            SpawnMediumAsteroid();
            timers[1] = 0;
        }
        if (timers[2] >= 70)//Small Asteroids
        {
            SpawnSmallAsteroid();
            timers[2] = 0;
        }
        if (timers[3] >= 400)//UFOs
        {
            SpawnUfo();
            timers[3] = 0;
        }
        if (timers[4] >= 800)//Power Ups
        {
            SpawnPowerUps();
            timers[4] = 0;
        }
        if (timers[5] >= 1200)//Light Frigates
        {
            SpawnLightFrigates();
            timers[5] = 0;
        }
        if (timers[6] >= 1550)//Frigates
        {
            SpawnFrigates();
            timers[6] = 0;
        }
        if (timers[7] >= 1460)//BattleCruisers
        {
            SpawnBattleCruisers();
            timers[7] = 0;
        }
	}
}
