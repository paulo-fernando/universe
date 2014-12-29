using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int hp_max = 50;
	private int hp;
	public AudioClip damageSound;
	// Use this for initialization
	void Start ()
	{
		InvokeRepeating("Regen", 3, 3);
	}

	void Awake()
	{
		hp = hp_max;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(hp <= 0)
		{
			audio.PlayOneShot (damageSound);
			Destroy(gameObject);
			//gameObject.GetComponent("Detonator").Explode();
		}
	}
	
	void Regen()
	{
		if(hp < hp_max)
			hp += 5;
		else if(hp > hp_max)
			hp = hp_max;
	}
	
	/*void OnCollisionEnter(Collision collision)
	{
		if(collision.rigidbody)
		{
			//Projectile projectile = (Projectile)collision.gameObject.GetComponent("Projectile");
			int damage = 5;//projectile.damage;
			hp -= damage;
			audio.PlayOneShot (damageSound);
			Debug.Log ("Enemy attaked by '" + collision.gameObject.tag + "' and damaged by " + damage);
		}
	}*/

	public void GetDamaged(int damage)
	{
		hp -= damage;
		audio.PlayOneShot(damageSound);
		Debug.Log ("Enemy attaked and damaged by " + damage);
	}
}
