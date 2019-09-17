using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Bladeren : MonoBehaviour {

    int startPage;

	public SteamVR_Action_Boolean flipB;
	public SteamVR_Action_Boolean flipF;

	public SteamVR_Input_Sources handType;

    public GameObject PageLeft;
    public GameObject PageRight;
    public GameObject PageFlip;
    Texture2D pageTexture;
	public Animator anim;
	bool OnTheLeft;

	public Material AirMaterial;
	public Material EarthMaterial;
	public Material FireMaterial;
	public Material WaterMaterial;
	
	
	public GameObject ForceSphere;

	private void OnEnable() {

		if (flipB == null || flipF == null)	{
			Debug.LogError("<b>[SteamVR Interaction]</b> No flip action assigned");
			return;
		}

		flipB.AddOnChangeListener(OnFlipBActionChange, handType);
		flipF.AddOnChangeListener(OnFlipFActionChange, handType);
	}

	private void OnDisable() {

			if (flipB != null)
				flipB.RemoveOnChangeListener(OnFlipBActionChange, handType);
			if (flipF != null)
				flipF.RemoveOnChangeListener(OnFlipFActionChange, handType);
			
	}
	
	private void ChangeForce() {
		
		if (startPage==1)
			ForceSphere.GetComponent<Renderer>().material = EarthMaterial;
		else if (startPage==3)
			ForceSphere.GetComponent<Renderer>().material = WaterMaterial;
		else if (startPage==5)
			ForceSphere.GetComponent<Renderer>().material = AirMaterial;
		else if (startPage==7)
			ForceSphere.GetComponent<Renderer>().material = FireMaterial;

	}
	
	private void OnFlipBActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue) {
		
		if (newValue) {
			
			if (anim.GetBool ("Flipped") && startPage>2) {
				
				startPage -= 2;

				anim.SetBool("Flipped", false);

				ForceSphere.SetActive(false);

				ChangeForce();
				
				pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
				PageFlip.GetComponent<Renderer>().materials[0].mainTexture = pageTexture;

				pageTexture = Resources.Load("page" + (startPage + 2)) as Texture2D;
				PageFlip.GetComponent<Renderer>().materials[1].mainTexture = pageTexture;

				PageFlip.SetActive(true);

				anim.Play("FlipBack",-1,0f);

				OnTheLeft = false;

				pageTexture = Resources.Load("page" + startPage) as Texture2D;
				PageLeft.GetComponent<Renderer>().material.mainTexture = pageTexture;

			}
		}
	}

	private void OnFlipFActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue) {
		
		if (newValue) {
			
			if (anim.GetBool ("Flipped") && startPage<6) {
				
				startPage += 2;

				anim.SetBool("Flipped", false);

				ForceSphere.SetActive(false);

				ChangeForce();
				
				pageTexture = Resources.Load("page" + startPage) as Texture2D;
				PageFlip.GetComponent<Renderer>().materials[1].mainTexture = pageTexture;

				pageTexture = Resources.Load("page" + (startPage - 1)) as Texture2D;
				PageFlip.GetComponent<Renderer>().materials[0].mainTexture = pageTexture;

				PageFlip.SetActive(true);

				anim.Play("FlipForward",-1,0f);

				OnTheLeft = true;

				pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
				PageRight.GetComponent<Renderer>().material.mainTexture = pageTexture;

			}

		}

	}


    // Start is called before the first frame update
    void Start()
    {
        startPage = 1;

		OnTheLeft = false;

		PageFlip.SetActive(false);

		//ForceSphere.SetActive(false);

		anim.Play("Flipped",-1,0f);

        pageTexture = Resources.Load("page1") as Texture2D;
        PageLeft.GetComponent<Renderer>().material.mainTexture = pageTexture;

		pageTexture = Resources.Load("page2") as Texture2D;
		PageRight.GetComponent<Renderer>().material.mainTexture = pageTexture;

		pageTexture = Resources.Load("page2") as Texture2D;
		PageFlip.GetComponent<Renderer>().materials[0].mainTexture = pageTexture;

		pageTexture = Resources.Load("page3") as Texture2D;
		PageFlip.GetComponent<Renderer>().materials[1].mainTexture = pageTexture;


    }

    // Update is called once per frame
    void Update() {

		if (anim.GetBool ("Flipped") && PageFlip.activeSelf) { //Finished flipping? Load page texture beneath and remove flip page.

			if (OnTheLeft) {
				pageTexture = Resources.Load("page" + startPage) as Texture2D;
				PageLeft.GetComponent<Renderer>().material.mainTexture = pageTexture;
			} else {
				pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
				PageRight.GetComponent<Renderer>().material.mainTexture = pageTexture;
			}

			PageFlip.SetActive(false);

			ForceSphere.SetActive(true);

		}

    }

}
