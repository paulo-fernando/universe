using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class Projectile
{
	public enum Types
	{
		FIREBALL,
		WATERBALL,
		SOLIDBALL,
		ENERGYBALL,
		LIGHTNING,
		SOUNDWAVE,
		WINDWAVE,
		METALL,
		FANTOM,
		PROJECTILES_COUNT
	};

	public GameObject explosion;
	
	private bool disposed = false;
	private Object m_projectile_prefab;

	public Object GetPrefab()
	{
		return m_projectile_prefab;
	}

	private Projectile.Types m_projectile_type = Projectile.Types.PROJECTILES_COUNT;

	public void SetType(Projectile.Types projectile_type)
	{
		m_projectile_type = projectile_type;
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public Projectile(Projectile.Types projectile_type, Object projectile_prefab)
	{
		m_projectile_type = projectile_type;
		m_projectile_prefab = projectile_prefab;
	}

	void Awake()
	{
		Debug.Log("[Awake Projectile]");
	}

	~Projectile()
	{
		Debug.Log ("[Destroy Projectile] Projectile '" + m_projectile_type + "' destroyed");
	}

	/*public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}*/

/*	private void Dispose(bool disposing)
	{
		if(!this.disposed)
		{
			this.disposed = true;
		}
	}*/
}
