using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorLoadingHandler : MonoBehaviour {

	void Start () {
        DenealManager.Instance.OnUIStateChange.AddListener(ChangeVisibility);
        gameObject.SetActive(false);
    }

    void ChangeVisibility()
    {
        if (DenealManager.Instance.UiState == DenealManager.UIStates.Loading)
            gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
