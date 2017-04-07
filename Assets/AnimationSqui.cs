using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSqui : MonoBehaviour {
	public Texture[] Colors;
	public Mesh[] Squis;
	public float animationTime;
	private int count;
	private MeshFilter squi;
	private MeshRenderer SquiColor;

	public static bool isrunning= false;
	// Use this for initialization
	void Start () {
		squi = GetComponent<MeshFilter>();
		SquiColor = GetComponent<MeshRenderer>();
		count = 0;
		StartCoroutine(spriteRoutine());
	}
	IEnumerator spriteRoutine(){
		while (isrunning) {
//			AudioSource.Stop ();
			yield return new WaitForSeconds (animationTime);
			if (count > 2)
				count = 0;
			squi.mesh = Squis [count];
			SquiColor.material.mainTexture = Colors [count];
			count++;
		}
	}

	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate(){
		
	}
}
