using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenealModelConnection : MonoBehaviour
{
    public GameObject decorModel;
    public string assetBundleUrl;
    //[HideInInspector]
    public string prefabName = "model";
    [HideInInspector]
    public AssetBundle bundle;
    public AssetDownloader assetDownloader;
    public float height;
    public bool hasModelBeenPlaced = false;
    public string info;

    public GameObject DecorModel
    {
        get { return decorModel; }

        set
        {
            decorModel = value;
            SetModelScale(1f);
        }
    }

    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        if (button != null) button.onClick.AddListener(ButtonClicked);
    }

    void Update()
    {

    }

    public void SetModelScale(float modelScale)
    {
        if (DenealManager.Instance.is3DScene)
        {
            ModelScaleTransformer.CustomModelScale3D(decorModel, modelScale);
        }
        else
        {
            ModelScaleTransformer.ResetModelScaleAR(decorModel, height);
        }
    }

    public void ButtonClicked()
    {
        assetDownloader.ModelSelectHandler(this);
    }

    public void DestroyDecorModel()
    {
        OnDestroy();
        Destroy(DecorModel);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {

        if(DenealManager.Instance.allModelsDict.ContainsKey(decorModel))
            DenealManager.Instance.allModelsDict.Remove(decorModel);
        if (bundle != null)
        {
            bundle.Unload(false);
        }
    }
}
