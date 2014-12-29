using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour
{
	public int speed = 200;

	private Gun.Types m_gun_id;
	private Projectile.Types m_projectile_id;

	GameObject m_gun_object;
	
	private Dictionary<Gun.Types, GameObject> m_guns;

	public static Object LoadObject(string name)
	{
		Object loaded_object = null;
		try
		{
			loaded_object = Resources.Load("Guns/" + name);
		}
		catch(UnityException ex)
		{

		}

		Debug.Log("[LoadObject] '" + name + "'");

		string status = (loaded_object != null ? "OK" : "NOT FOUND");

		Debug.Log("[LoadObject] '" + name + "': " + status);
		return loaded_object;
	}

	private void SetGun(Gun.Types gunType)
	{
		if(!m_gun_object)
		{
			Debug.LogError("[SetGun] Gun point not found!");
		}

			bool gunSelected = false;
			bool all_guns_tryed = false;
			Gun.Types i = (Gun.Types)0;
			Gun.Types gunType_tmp = gunType;
			GameObject selected_gun = null;

			while(!gunSelected && !all_guns_tryed)
			{
				m_gun_id = gunType_tmp >= Gun.Types.GUNS_COUNT ? (Gun.Types)0 : gunType_tmp;
				selected_gun = m_guns[m_gun_id];
				if(selected_gun && selected_gun != null)
					gunSelected = true;

				i++;

				if(i == Gun.Types.GUNS_COUNT)
					all_guns_tryed = true;
			}

			if(!gunSelected)
			{
				Debug.LogError("[SetGun] Can't select any gun. Guns list is emty or doesn't loaded correctly!");
			}

			Debug.Log("[SetGun] change gun to " + m_gun_id);

			MeshFilter meshFilter = m_gun_object.GetComponent<MeshFilter>();
			meshFilter.mesh = selected_gun.GetComponent<MeshFilter>().mesh;

			m_gun_object.transform.localScale = m_guns[m_gun_id].transform.localScale;
			m_gun_object.renderer.material = m_guns[m_gun_id].renderer.material;
	}

	private void LoadWeapons()
	{

	}

	private bool AddGun(Gun.Types gun_type, string gun_object_name)
	{
		Debug.Log("[AddGun] " + gun_type);
		Object gun_prefab = GunController.LoadObject(gun_object_name);
		
		if(gun_prefab == null)
		{
			return false;
		}
		else
		{
			GameObject gun_instance = (GameObject)Instantiate(gun_prefab);
			gun_instance.GetComponent<Gun>().SetGunType(gun_type);
			m_guns.Add(gun_type, gun_instance);
		}

		return true;
	}

	private void LoadGuns()
	{
		m_guns = new Dictionary<Gun.Types, GameObject>();

		AddGun(Gun.Types.MAGIC_L1, "gun_magic");
		AddGun(Gun.Types.SAW_WOOD, "gun_saw_wood");
		AddGun(Gun.Types.SAW_STONE, "gun_saw_wood");

		Gun magic = m_guns[Gun.Types.MAGIC_L1].GetComponent<Gun>();

		magic.AddProjectile(Projectile.Types.ENERGYBALL, "proj_energyball");
		magic.AddProjectile(Projectile.Types.FIREBALL, "proj_fireball");
		magic.AddProjectile(Projectile.Types.LIGHTNING, "proj_lightning");
		magic.AddProjectile(Projectile.Types.METALL, "proj_metall");
		magic.AddProjectile(Projectile.Types.SOLIDBALL, "proj_solidball");
		magic.AddProjectile(Projectile.Types.SOUNDWAVE, "proj_soundwave");
		magic.AddProjectile(Projectile.Types.WATERBALL, "proj_waterball");
		magic.AddProjectile(Projectile.Types.WINDWAVE, "proj_windwave");
		magic.AddProjectile(Projectile.Types.FANTOM, "proj_fantom");
	}

	void Awake()
	{
		m_gun_id = Gun.Types.MAGIC_L1;
		m_projectile_id = Projectile.Types.SOLIDBALL;

		m_gun_object = GameObject.Find("/First Person Controller/Main Camera/Gun");
		if(!m_gun_object)
		{
			Debug.LogError("[AWAKE] Can't find a Gun object");
			return;
		}
		LoadGuns();
		SetGun(m_gun_id);
	}

	void Start ()
	{

	}

	private void CheckProjectiles(Gun gun)
	{
		foreach (KeyValuePair<Projectile.Types, Projectile> pair in gun.GetProjectiles())
		{
			Debug.Log("[CheckProjectiles] for " + gun.GetType() + ": " + pair.Key + ": " + (pair.Value == null ? "NULL" : "OK"));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

		if(Input.GetButtonDown("Fire1"))
		{
			CheckProjectiles(m_guns[m_gun_id].GetComponent<Gun>());
			Transform gunTransform = m_gun_object.transform;
			Vector3 SpawnVector = new Vector3(gunTransform.position.x, gunTransform.position.y, gunTransform.position.z);
			Debug.Log("[Gun.Update] m_gun_id : " + m_gun_id);
			GameObject gun = m_guns[m_gun_id];

			if(gun.GetComponent<Gun>().GetProjectiles().Count <= 0)
			{
				Debug.LogError("projectiles list is empty!");
				return;
			}
			else
			{
				Debug.Log("projectiles list size is: " + gun.GetComponent<Gun>().GetProjectiles().Count);
			}

			Projectile projectile = gun.GetComponent<Gun>().GetProjectiles()[m_projectile_id];

			if(projectile == null)
			{
				Debug.LogError("projectile '" + m_projectile_id + "' in gun '" + m_gun_id + "' to fire is null!");
				CheckProjectiles(gun.GetComponent<Gun>());
				return;
			}

			GameObject clone = (GameObject)Instantiate(projectile.GetPrefab(), SpawnVector, gunTransform.rotation);
			Rigidbody clone_body = clone.GetComponent<Rigidbody>();
			clone_body.velocity = gunTransform.TransformDirection(Vector3.forward*speed);
		}

		if(Input.GetButtonDown("Fire2"))
		{
			SetGun(m_gun_id + 1);
		}
	}
}
