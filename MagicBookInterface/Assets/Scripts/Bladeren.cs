using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bladeren : MonoBehaviour
{
    int startPage;
    public GameObject PageLeft;
    public GameObject PageRight;
    public GameObject PageFlip;
    Texture2D pageTexture;
	Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        startPage = 1;

		anim = this.GetComponent<Animator>();

		PageFlip.SetActive(false);

        pageTexture = Resources.Load("page" + startPage) as Texture2D;
        PageLeft.GetComponent<Renderer>().material.mainTexture = pageTexture;

		pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
		PageRight.GetComponent<Renderer>().material.mainTexture = pageTexture;

		pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
        PageFlip.GetComponent<Renderer>().materials[0].mainTexture = pageTexture;

        pageTexture = Resources.Load("page" + (startPage + 2)) as Texture2D;
        PageFlip.GetComponent<Renderer>().materials[1].mainTexture = pageTexture;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown("up"))
		{

			Debug.Log("Start1 = "+startPage);

			pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
			PageFlip.GetComponent<Renderer>().materials[0].mainTexture = pageTexture;

			pageTexture = Resources.Load("page" + (startPage + 2)) as Texture2D;
			PageFlip.GetComponent<Renderer>().materials[1].mainTexture = pageTexture;

			startPage++;

			Debug.Log("Start2 = "+startPage);

			pageTexture = Resources.Load("page" + startPage) as Texture2D;
			PageLeft.GetComponent<Renderer>().material.mainTexture = pageTexture;

			pageTexture = Resources.Load("page" + (startPage + 1)) as Texture2D;
			PageRight.GetComponent<Renderer>().material.mainTexture = pageTexture;

			PageFlip.SetActive(true);

			anim.Play("PageForward",-1,0f);

		}

		if (Input.GetKeyDown("down"))
		{
			startPage--;
		}

    }
}
