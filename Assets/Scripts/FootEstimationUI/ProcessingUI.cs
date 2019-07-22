using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingUI : MonoBehaviour {

    public GameObject uiLine;
    public GameObject uiBackground;
    private Animator uiLineAnimator;
    private Animator uiBackgroundAnimator;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartProcessAnimation()
    {
        uiLineAnimator = uiLine.GetComponent<Animator>();
        uiBackgroundAnimator = uiBackground.GetComponent<Animator>();

        uiLineAnimator.Play("ProcessingLine");
        uiBackgroundAnimator.Play("ProcessingBackground");
    }

}
