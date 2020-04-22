using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBackgroundLoop : MonoBehaviour
{
	public GameObject[] levels; //The Image used for the level
	private Camera mainCamera; //Reference
	private Vector2 screenBounds; //The Boundary size of the Camera
	public float choke;

	public float moveSpeed = 0f; //The speed of the background moving

	void Start()
	{
		mainCamera = gameObject.GetComponent<Camera>(); //Defines the Image/Object to the Main Camera
		screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)); //The dimension(Screen width x height) of the camera is put into an X and Y axis
		foreach (GameObject obj in levels) //Executes the function for each sprite
		{
			loadChildObjects(obj); //Checks through the sprite to see which objects to load
		}
	}
	void loadChildObjects(GameObject obj) //Loads the sprites onto the screen
	{
		float objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke; //Horizontal value of the Sprite
		int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objectWidth); //The amount of clones needed to fill horizontally through the camera
		GameObject clone = Instantiate(obj) as GameObject; //Clones the image
		for (int i = 0; i <= childsNeeded; i++) //Creates a loop for the child objects for the image
		{
			GameObject c = Instantiate(clone) as GameObject; //Clone of the image
			c.transform.SetParent(obj.transform); //The child object of our parent object(image)
			c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y, obj.transform.position.z); //Spaces it out a bit from each other
			c.name = obj.name + i; //Gives the object a name
		}
		Destroy(clone); //Deletes the clone object in order so once it gets to a certain part of the screen, it will delete on the left and return on the right 
		Destroy(obj.GetComponent<SpriteRenderer>()); //Prevents the game from lagging because it will constantly create child objects
	}
	void repositionChildObjects(GameObject obj) 
	{
		Transform[] children = obj.GetComponentsInChildren<Transform>(); //Creates the child objects in obj
        //If the camera moves to the middle, the first child on the left would move past the last child continuously
		if (children.Length > 1)
		{
			GameObject firstChild = children[1].gameObject;
			GameObject lastChild = children[children.Length - 1].gameObject;
			float halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
			if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth)
			{
				firstChild.transform.SetAsLastSibling();
				firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
			}
			else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth)
			{
				lastChild.transform.SetAsFirstSibling();
				lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);

			}
		}
	}
	void LateUpdate() //Repositions the children so it can constantly fill the screen
	{
		foreach (GameObject obj in levels) //Loops through our list called levels
		{
			repositionChildObjects(obj);
		}
	}
	//Constantly allows the backgroup to move through a certain direction
	private void Update()
	{
        var DayToNight = levels[0];
		float offset = moveSpeed * Time.deltaTime;
		DayToNight.transform.position = new Vector3(DayToNight.transform.position.x + offset, DayToNight.transform.position.y, DayToNight.transform.position.z);
        DayToNight.transform.position = new Vector3(DayToNight.transform.position.x + offset * 2, DayToNight.transform.position.y, DayToNight.transform.position.z);
    }
}
