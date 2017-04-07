using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNanar : MonoBehaviour {
	public Texture[] Colors;
	public Mesh[] Nanars;
	public float animationTime;
	private int count;
	private MeshFilter nanar;
	private MeshRenderer NanarColor;
	private bool isrunning= true;
	// Use this for initialization
	void Start () {
		nanar = GetComponent<MeshFilter>();
		NanarColor = GetComponent<MeshRenderer>();
		count = 0;
		StartCoroutine(spriteRoutine());
	}
	IEnumerator spriteRoutine(){
		while (isrunning) {
			yield return new WaitForSeconds (animationTime);
			if (count > 2)
				count = 0;
			nanar.mesh = Nanars [count];
			NanarColor.material.mainTexture = Colors [count];
			count++;
		}
	}

	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate(){

	}
}
