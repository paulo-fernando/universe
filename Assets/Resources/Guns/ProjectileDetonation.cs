using UnityEngine;
using System.Collections;

public class ProjectileDetonation : MonoBehaviour
{
	public GameObject explosion;
	public int damage = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter(Collision collision)
	{
		if (explosion)
		{
			Instantiate(explosion, this.transform.position, this.transform.rotation);
		}

		Debug.Log("OnTriggerEnter");
		
		TerrainDeformer deformer_component = collision.collider.GetComponent<TerrainDeformer>();
		
		if (deformer_component != null)
		{
			Debug.Log("TerrainDeformer exists");
			collision.collider.GetComponent<TerrainDeformer>().DestroyTerrain(this.transform.position,10);
		}
		
		Enemy enemy_component = collision.collider.GetComponent<Enemy>();
		
		if(enemy_component != null)
		{
			enemy_component.GetDamaged(this.damage);
		}

		Destroy(this.gameObject);
	}
}
