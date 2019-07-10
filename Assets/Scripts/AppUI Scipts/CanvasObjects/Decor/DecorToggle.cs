using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecorToggle : MonoBehaviour, IClickable {
    public void ChangeVisibility()
    {
        switch (DenealManager.Instance.UiState)
        {
            case DenealManager.UIStates.AutoPlace:
                gameObject.SetActive(false);
                break;
            case DenealManager.UIStates.Idle:
                gameObject.SetActive(true);
                break;
            case DenealManager.UIStates.Loading:
                gameObject.SetActive(false);
                break;
        }
    }

    public void ClickHandler()
    {
        DenealManager.Instance.ToggleScene();
    }

    void Start () {
        gameObject.GetComponent<Button>().onClick.AddListener(ClickHandler);
        DenealManager.Instance.OnUIStateChange.AddListener(ChangeVisibility);
        ChangeVisibility();
     }
	
	void Update () {}
}
