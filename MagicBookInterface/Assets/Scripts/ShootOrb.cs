using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System;

public class ShootOrb : MonoBehaviour
{

	public SteamVR_Action_Boolean Shoot;
	public SteamVR_Input_Sources handType;
	
	Material SphereMaterial;
	public GameObject bol;

	public GameObject bullet; 
	public Transform spawnPoint; 
	
	private void OnEnable() {
		if (Shoot == null)	{
			Debug.LogError("<b>[SteamVR Interaction]</b> No shoot action assigned");
			return;
		}
		Shoot.AddOnChangeListener(OnShootActionChange, handType);
	}

	private void OnDisable() {
			if (Shoot != null)
				Shoot.RemoveOnChangeListener(OnShootActionChange, handType);
	}
	
    // Start is called before the first frame update
    void Start() {
        
    }

	private void OnShootActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue) {
	
		if (newValue) {
			SphereMaterial = bol.GetComponent<Renderer>().material;
			GameObject bulletInstance;
			bulletInstance = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
			bulletInstance.GetComponent<Renderer>().material = SphereMaterial;
			bulletInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * 1000f);
			Destroy(bulletInstance,5);
		}

	}
		
    // Update is called once per frame
    void Update() {

    }

}
