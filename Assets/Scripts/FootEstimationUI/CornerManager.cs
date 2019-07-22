using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CornerManager : MonoBehaviour {

    public GameObject line;
    private Camera mainCamera;
    public LayerMask planeLayerMask;
    public GameObject cornerLineAdjustUI;
    public Canvas canvas;
    public TMP_InputField tmPro;
    private int screenTouchHeight;
    private int screenTouchMarigin;
    private RectTransform canvasRect;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        screenTouchHeight = mainCamera.pixelHeight / 2;
        screenTouchMarigin = mainCamera.pixelHeight / 10;
        canvasRect = canvas.GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount == 1)
        {
            UpdateTouchScreenHeight();
            UpdateLinePosition();
            TouchRotate();
            UpdateCornerLineAdjustUIPosition();
        }

    }

    void UpdateTouchScreenHeight()
    {
        Touch touch = Input.GetTouch(0);
        if (Mathf.Abs(touch.position.y - screenTouchHeight)< screenTouchMarigin)
        {
            screenTouchHeight = (int)touch.position.y;
            tmPro.text = "screenTouchHeight: " + screenTouchHeight.ToString();
        }

    }

    void UpdateLinePosition()
    {
        Vector3 screenPoint = new Vector3(mainCamera.pixelWidth/2, screenTouchHeight, 0f);
        Vector3 newLinePosition = CalculatePositionFromScreenPoint(screenPoint);
        line.transform.position = newLinePosition;

    }

    void UpdateLineRotation()
    {
        line.transform.LookAt(mainCamera.transform.position);
        line.transform.eulerAngles = new Vector3(0, line.transform.eulerAngles.y + 180, 0);
    }

    void UpdateCornerLineAdjustUIPosition()
    {
        Vector2 ViewportPosition = mainCamera.WorldToViewportPoint(line.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        Debug.Log("ViewportPosition.x: " + ViewportPosition.x);
        Debug.Log("ViewportPosition.y: " + ViewportPosition.y);
        Debug.Log("canvasRect.sizeDelta.x: " + canvasRect.sizeDelta.x);
        Debug.Log("canvasRect.sizeDelta.y: " + canvasRect.sizeDelta.y);
        Debug.Log(WorldObject_ScreenPosition);
        cornerLineAdjustUI.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }

    Vector3 CalculatePositionFromScreenPoint(Vector3 screenPoint)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out hit, 500.0f, planeLayerMask))
        {
            return hit.point;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }

    }

    bool IsUITouch()
    {
        //if (Input.touchCount == 1)
        //{
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.GetTouch(0).position;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 4) //number of border panel elements
        {
            return true;
        }
        return false;
        //}
        //return false;
    }

    void TouchRotate()
    {
        if (line.activeSelf == true)
        {

            Quaternion desiredRotation = line.transform.rotation;

            TouchMovementCalculator.Calculate();

            //Debug.Log(TouchMovementCalculator.turnAngleDelta);
            if (Mathf.Abs(TouchMovementCalculator.turnAngleDelta) > 0)
            { // rotate
                Vector3 rotationDeg = Vector3.zero;
                rotationDeg.y = -TouchMovementCalculator.turnAngleDelta;
                desiredRotation *= Quaternion.Euler(rotationDeg);
            }

            desiredRotation.x = 0;
            desiredRotation.z = 0;

            line.transform.rotation = desiredRotation;
        }
    }
}
