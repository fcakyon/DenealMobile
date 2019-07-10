using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AssetDownloader : MonoBehaviour {

    private IEnumerator coroutine;
    private UnityWebRequest request;
    public Text progressPercentage;

    void Start () {
        Caching.ClearCache();
    }
    
    void Update () {
    }

    public void ModelSelectHandler(FoodModelConnection foodModelConnection)
    {
        if(FoodManager.Instance.FoodModelConnection == null ||
            FoodManager.Instance.FoodModelConnection.assetBundleUrl != foodModelConnection.assetBundleUrl) 
        {
            if (coroutine != null) StopCoroutine(coroutine);
            if (request != null && !request.isDone) request.Abort();
            coroutine = DownloadAssetBundleAndSetFoodModel();
            FoodManager.Instance.RemoveConnection();
            FoodManager.Instance.FoodModelConnection = foodModelConnection;
            if (FoodManager.Instance.FoodModelConnection.enabled)
                FoodManager.Instance.isChanging = true;
            StartCoroutine(coroutine);
        }
    }

    public void ModelSelectHandler(DenealModelConnection decorModelConnection)
    {
        if (!DenealManager.Instance.is3DScene
            || DenealManager.Instance.DecorModelConnection == null
            || decorModelConnection.assetBundleUrl != DenealManager.Instance.DecorModelConnection.assetBundleUrl)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            if (request != null && !request.isDone) request.Abort();
            coroutine = DownloadAssetBundleAndSetDecorModel();
            if (DenealManager.Instance.is3DScene) DenealManager.Instance.RemoveConnection();
            DenealManager.Instance.DecorModelConnection = decorModelConnection;
            StartCoroutine(coroutine);
        }

    }

    IEnumerator DownloadAssetBundleAndSetFoodModel()
    {
        FoodManager.Instance.UiState = FoodManager.UIStates.Loading;
        request = UnityWebRequestAssetBundle.GetAssetBundle(FoodManager.Instance.FoodModelConnection.assetBundleUrl, 0, 0);
        request.SendWebRequest();
        while (!request.isDone)
        {
            float progressPercentageFloat = Mathf.Min(request.downloadProgress * 100 / 4 * 5, 100);
            progressPercentage.text = progressPercentageFloat.ToString("n0") + "%";
            yield return null;
        }
        FoodManager.Instance.FoodModelConnection.bundle = DownloadHandlerAssetBundle.GetContent(request);
        GameObject foodModelAsset = FoodManager.Instance.FoodModelConnection.bundle.LoadAsset<GameObject>(FoodManager.Instance.FoodModelConnection.prefabName);
        GameObject foodModel = Instantiate(foodModelAsset, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        FoodManager.Instance.FoodModelConnection.FoodModel = foodModel;
        FoodManager.Instance.ChangeStateAfterLoading();
    }

    IEnumerator DownloadAssetBundleAndSetDecorModel()
    {
        bool hasBundleLoadedBefore = false;
        DenealManager.Instance.UiState = DenealManager.UIStates.Loading;
        foreach (DenealModelConnection connection in DenealManager.Instance.allModelsDict.Values)
        {
            if (connection.assetBundleUrl == DenealManager.Instance.DecorModelConnection.assetBundleUrl)
            {
                hasBundleLoadedBefore = true;
                DenealManager.Instance.DecorModelConnection.DecorModel = Instantiate(connection.DecorModel);
                break;
            }
        }
        if (!hasBundleLoadedBefore)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle(DenealManager.Instance.DecorModelConnection.assetBundleUrl, 0, 0);
            request.SendWebRequest();
            while (!request.isDone)
            {
                float progressPercentageFloat = Mathf.Min(request.downloadProgress * 100 / 4 * 5, 100); 
                progressPercentage.text = progressPercentageFloat.ToString("n0") + "%";
                yield return null;
            }
            AssetBundle downloadedBundle = DownloadHandlerAssetBundle.GetContent(request);
            DenealManager.Instance.DecorModelConnection.bundle = downloadedBundle;
            GameObject decorModelAsset = DenealManager.Instance.DecorModelConnection.bundle.LoadAsset<GameObject>(DenealManager.Instance.DecorModelConnection.prefabName);
            DenealManager.Instance.DecorModelConnection.DecorModel = Instantiate(decorModelAsset, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        }

        DenealManager.Instance.AddModelToDict(DenealManager.Instance.DecorModelConnection.DecorModel, DenealManager.Instance.DecorModelConnection);
        DenealManager.Instance.ChangeStateAfterLoading();
    }
}
