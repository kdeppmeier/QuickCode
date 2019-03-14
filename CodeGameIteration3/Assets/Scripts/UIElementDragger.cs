using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour
{
    public const string DRAGGABLE_TAG = "UIDraggable";

    private bool dragging = false;

    private Vector2 orginalPosition;

    private Transform objectToDrag;
    private Text objectToDragText;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    #region MonoBehaviour API


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if(objectToDrag != null)
            {
                dragging = true;

                objectToDrag.SetAsLastSibling();

                orginalPosition = objectToDrag.position;
                objectToDragText = objectToDrag.GetComponent<Text>();
                objectToDragText.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                Transform objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = orginalPosition;
                }
                else
                {
                    objectToDrag.position = orginalPosition;
                }

                objectToDragText.raycastTarget = true;
                objectToDrag = null;
            }

            dragging = false;
        }
    }

    #endregion

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedObject = GetObjectUnderMouse();

        if(clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }
}
