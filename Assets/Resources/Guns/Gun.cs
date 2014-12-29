using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public enum Types
	{
		SAW_WOOD,
		SAW_STONE,
		MAGIC_L1,
		GUNS_COUNT
	};

	private Types m_gun_type = Types.GUNS_COUNT;


	public Gun.Types GetType()
	{
		return m_gun_type;
	}

	public void SetGunType(Gun.Types gun_type)
	{
		m_gun_type = gun_type;
	}

	public int damage_factor = 1;

	private Dictionary<Projectile.Types, Projectile> m_projectiles;

	public void AddProjectile(Projectile.Types projectile_type, string pobject_name)
	{
		Object projectile_prefab = GunController.LoadObject(pobject_name);

		if(projectile_prefab != null)
		{
			Projectile projectile = new Projectile(projectile_type, projectile_prefab);

			if(projectile == null)
				Debug.LogError("Projectile is null");

			if(m_projectiles != null)
			{
				Debug.Log("[AddProjectile]: projectile '" + projectile_type + "' added to gun " + m_gun_type);
				m_projectiles.Add(projectile_type, projectile);
			}
			else
				Debug.LogError("[AddProjectile]: m_projectiles is undefined");
		}
		else
		{
			Debug.LogWarning("[AddProjectile]: Cannot add projectile '" + projectile_type + "' to gun '" + m_gun_type + "' cause it passed as null");
		}
	}

	public Gun(Gun.Types gun_type)
	{
		Debug.Log("Create Gun");
	}

	~Gun()
	{
		Debug.Log ("[Destroy Gun] Gun '" + m_gun_type + "' destroyed");
	}

	public Dictionary<Projectile.Types, Projectile> GetProjectiles()
	{
		return m_projectiles;
	}

	void Awake()
	{
		Debug.Log ("[Gun Awake] create projectiles for gun '" + m_gun_type + "'");
		m_projectiles = new Dictionary<Projectile.Types, Projectile>();
//		GC.SuppressFinalize(this);
	}

	void Start ()
	{

	}
	
	void Update ()
	{
	
	}
}
