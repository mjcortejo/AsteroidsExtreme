using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform character;
	// Use this for initialization
	void Start () 
    {
    }
	// Update is called once per frame
	void Update () 
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos2D = new Vector3((character.position.x + mousePos.x) / 2, (character.position.y + mousePos.y) / 2, -10);
        transform.position = mousePos2D;
	}
}
