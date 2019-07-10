using UnityEngine;

public class 
DecorAdd : MonoBehaviour, IClickable {

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

    public void ClickHandler() {}

    void Start () {
        DenealManager.Instance.OnUIStateChange.AddListener(ChangeVisibility);
        ChangeVisibility();
    }
	
	void Update () {}
}
