using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorPopupPanelBigController : MonoBehaviour, ICanvasPanel
{

    public bool isPanelOpen;
    private Animator cameraMaskAnimator;

    public void ClosePanel()
    {
        isPanelOpen = false;
        cameraMaskAnimator = gameObject.GetComponent<Animator>();
        cameraMaskAnimator.Play("Popup Panel Big Out");
    }

    public void OpenPanel()
    {
        isPanelOpen = true;
        cameraMaskAnimator = gameObject.GetComponent<Animator>();
        cameraMaskAnimator.Play("Popup Panel Big In");
    }

}
