using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeAlpha : MonoBehaviour {
	public float alpha =1;
	float speed = 0;
	public bool IsImage = false;
	public bool blink =false;
	public float blinktimer = 1;
	float blinkTime = 0;
	int fadeInOut =0;
	Renderer ThisRenderer;
	Color colour;
	// Use this for initialization
	void Awake()
	{
		if (IsImage) {
			if(blink)
			{
				colour = gameObject.GetComponent<Image>().color;
				colour.a = alpha;
				gameObject.GetComponent<Image>().color = colour;
			}
		}
		else
		{
			ThisRenderer = gameObject.GetComponent<Renderer> ();
			colour = ThisRenderer.material.color;
			colour.a = alpha;
			ThisRenderer.material.color = colour;
		}
	}
	void Start () {
		if (IsImage) {
			if(blink==false)
			{

				colour = gameObject.GetComponent<Image>().color;
			}

		}
		else
		{
			ThisRenderer = gameObject.GetComponent<Renderer> ();
			colour = ThisRenderer.material.color;
		}//gameObject.GetComponent<GUIText>().material.color = new Color(255,255,255,alpha);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (IsImage) 
		{
			colour = gameObject.GetComponent<Image>().color;
			if (fadeInOut == 1) 
			{
				colour.a += speed * Time.deltaTime;
				gameObject.GetComponent<Image>().color = colour;
			}
			else if (fadeInOut == -1)
			{
				colour = gameObject.GetComponent<Image>().color;
				colour.a -= speed * Time.deltaTime;
				gameObject.GetComponent<Image>().color = colour;
				if ( alpha >=1 && colour.a<=0)
				{
					gameObject.SetActive (false);
				}
			}
			if(blink)
			{
				if(fadeInOut == -1)
				{
					colour = gameObject.GetComponent<Image>().color;
					colour.a =0;
					gameObject.GetComponent<Image>().color = colour;
				}
				else if(blinkTime>blinktimer)
				{
					if(gameObject.GetComponent<Image>().color.a ==0)
					{
						colour = gameObject.GetComponent<Image>().color;
						colour.a =1;
						gameObject.GetComponent<Image>().color = colour;
					}
					else
					{
						colour = gameObject.GetComponent<Image>().color;
						colour.a =0;
						gameObject.GetComponent<Image>().color = colour;
					}
					blinkTime = 0;
				}
				else if (blinkTime<blinktimer)
				{
					blinkTime+=Time.deltaTime;
				}

			}
		} 
		else 
		{

			if (fadeInOut == 1) 
			{

				foreach (Material FadeMaterial in ThisRenderer.materials) 
				{
					Debug.Log (FadeMaterial.name);
					colour = FadeMaterial.color;
					colour.a += speed * Time.deltaTime;
					FadeMaterial.color = colour;
				}
			} else if (fadeInOut == -1) {
				//alpha -= speed;
				//gameObject.GetComponent<GUIText>().material.color = new Color(255,255,255,alpha);
				foreach (Material FadeMaterial in ThisRenderer.materials) {
					Debug.Log (FadeMaterial.name);
					colour = FadeMaterial.color;
					colour.a -= speed * Time.deltaTime;
					FadeMaterial.color = colour;
				}
				//Debug.Log (ThisRenderer.material.name +" " + ThisRenderer.material.color.a);
			}

		}
		if(blink==false)
		{
			if ( alpha >=1 && colour.a<=0)
			{
				gameObject.SetActive (false);
			}
		}

	}
	public void StartFade(Vector2 fadeInput)
	{


			this.fadeInOut =(int)fadeInput[1];
		
			speed = 1 / fadeInput[0];
		Debug.Log (speed + " " + fadeInput[0]);
		

	}
	public void SetAlpha(float alpha)
	{
		if(IsImage)
		{
			colour = gameObject.GetComponent<Image>().color;
			colour.a =alpha;
			gameObject.GetComponent<Image>().color = colour;
		}
		else
		{
			foreach (Material FadeMaterial in ThisRenderer.materials) 
			{
				Debug.Log (FadeMaterial.name);
				colour = FadeMaterial.color;
				colour.a = alpha;
				FadeMaterial.color = colour;
			}
		}
	}
	 
}
