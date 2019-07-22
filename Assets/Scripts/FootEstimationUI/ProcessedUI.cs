using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessedUI : MonoBehaviour {

    public ProcessingUI ProcessingUI;
    private Animator foodEstimationUI;
    // Use this for initialization
    void Start () {
        //ProcessingUI ProcessingUI = gameObject.transform.Find("ProcessingPanel").GetComponent<ProcessingUI>();
        Debug.Log(gameObject.transform.Find("ProcessingPanel").name);
        foodEstimationUI = gameObject.GetComponent<Animator>();

        StartCoroutine(DemoAnimation());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DemoAnimation()
    {
        ProcessingUI.StartProcessAnimation();
        yield return new WaitForSeconds(30f);
        foodEstimationUI.Play("ProcessedResult");
    }
}
