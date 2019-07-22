using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorXRManager : MonoBehaviour {

    public GameObject infoPanel;
    public GameObject canvas;

    public void OnSurfaceAttach()
    {
        DecorAnimManager.Instance.Border2None();
        DecorAnimManager.Instance.CircularPlaneAnim();
        DenealManager.Instance.hasSurfaceFound = true;
        // Setting model visible after surface has found
        if (DenealManager.Instance.hasConnectionAndModel) {
            //float modelScale = 1f; //needs to be uncommented for model to be visible
            //DenealManager.Instance.DecorModelConnection.SetModelScale(modelScale);
        }
        else
        {
        }
        //canvas.SetActive(true); //needs to be uncommented for Canvas2 to be visible
        //infoPanel.SetActive(true);
    }

    public void OnSurfaceSwitch()
    {
        DecorAnimManager.Instance.CircularPlaneAnim();
    }
}
