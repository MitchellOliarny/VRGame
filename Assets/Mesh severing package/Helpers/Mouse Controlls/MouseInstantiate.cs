using System.Collections.Generic;
using UnityEngine;

public class MouseInstantiate : MonoBehaviour
{
	public GameObject projectilePrefab;
	
	public float speed = 7.0f;
	
	public void Update()
	{
		if (Input.GetMouseButtonDown(0) && projectilePrefab != null)
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			GameObject newGameObject = (GameObject)Instantiate(projectilePrefab, mouseRay.origin, Quaternion.identity);
			
			if (newGameObject.GetComponent<Rigidbody>() != null)
			{
				newGameObject.GetComponent<Rigidbody>().velocity = mouseRay.direction * speed;
			}
		}
	}
}