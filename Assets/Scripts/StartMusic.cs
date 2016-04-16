using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMusic : MonoBehaviour {
	public AudioSource BackgroundMusic;
	public GameObject CutSceen;
	public float Speed = 0.01f; 
	bool StartedMusic = false;
	// Use this for initialization
	void Start () {
		BackgroundMusic.Pause();
		BackgroundMusic.volume = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if(CutSceen.GetComponent<Image>().color.a<1 || CutSceen.activeInHierarchy==false)
		{
			if(StartedMusic!=true)
			{
				BackgroundMusic.Play();
				StartedMusic = true;
			}
			if(BackgroundMusic.volume<1)
			{
				BackgroundMusic.volume+=Time.deltaTime*Speed;
			}
		}
	}
}
