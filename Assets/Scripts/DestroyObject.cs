using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour 
{
	public GameObject explosion;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
		//explosionClone.GetComponent("Detonator").Explode();
		GameObject.Destroy(gameObject);
	}
}
