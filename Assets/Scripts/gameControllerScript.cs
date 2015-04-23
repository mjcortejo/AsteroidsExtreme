using UnityEngine;
using System.Collections;

public class gameControllerScript : MonoBehaviour {

    public Transform crossHair;

	void Start () {
        Screen.showCursor = false;//disables mouse cursor
	}
	
	// Update is called  once per frame
    //int x = 0;
	void Update () {
        DrawCameraViewBox();
        DrawCrossHair();
	}
    void DrawCameraViewBox()
    {
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(1, 1)), Camera.main.ViewportToWorldPoint(new Vector2(0, 1)));//top
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(0, 1)), Camera.main.ViewportToWorldPoint(new Vector2(0, 0)));//left
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)), Camera.main.ViewportToWorldPoint(new Vector2(1, 0)));//bottom
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)), Camera.main.ViewportToWorldPoint(new Vector2(1, 1)));//right
    }
    void DrawCrossHair()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crossHair.position = new Vector3(mousePos.x, mousePos.y, -1.0f);
    }
}
