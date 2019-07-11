using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorPopupPanelBigController : MonoBehaviour, ICanvasPanel
{

    public bool isPanelOpen;
    private Animator cameraMaskAnimator;

    public void ClosePanel()
    {
        StartCoroutine(ClosePanelCoroutine());
    }

    public IEnumerator ClosePanelCoroutine()
    {
        cameraMaskAnimator = gameObject.GetComponent<Animator>();
        cameraMaskAnimator.Play("Popup Panel Big Out");
        // to set ispanelopen to true only after the opening animaiton is finished
        yield return new WaitForSeconds(0.01f);
        isPanelOpen = false;
    }

    public void OpenPanel()
    {
        StartCoroutine(OpenPanelCoroutine());
    }

    public IEnumerator OpenPanelCoroutine()
    {
        cameraMaskAnimator = gameObject.GetComponent<Animator>();
        cameraMaskAnimator.Play("Popup Panel Big In");
        // to set ispanelopen to false only after the closing animaiton is finished
        yield return new WaitForSeconds(0.01f);
        isPanelOpen = true;
    }

}
