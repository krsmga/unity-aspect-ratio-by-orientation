/**
 * @author Kleber Ribeiro da Silva
 * @email krsmga@gmail.com
 * @create date 2020-07-09 09:34:21
 * @modify date 2020-07-09 09:34:21
 * @desc You can use this class attached to a GUI element to keep it in its original proportions when rotating the device. (Portrait, LandscapeLeft or LandscapeRight).
 * @github https://github.com/krsmga/AspectRatioByOrientation
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// You can use this class attached to a GUI element to keep it in its original proportions when rotating the device. 
/// (Portrait, LandscapeLeft or LandscapeRight)
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class AspectRatioByOrientation : MonoBehaviour
{
    private Vector3 _rotation;
    private RectTransform _rectTransform;
    private ScreenOrientation _originalOrientation;

    private ScreenOrientation __orientation;
    private ScreenOrientation _orientation
    {
        get { return __orientation; }
        set 
        {
            if (__orientation != value)
            {
                if (value == _originalOrientation)
                {
                    gameObject.GetComponent<RectTransform>().eulerAngles = Vector3.zero;
                }
                else
                {
                    if (__orientation == ScreenOrientation.LandscapeLeft && value == ScreenOrientation.Portrait)
                    {
                        _rotation.z = 90;
                        gameObject.GetComponent<RectTransform>().eulerAngles = _rotation;
                    }
                    else if (__orientation == ScreenOrientation.LandscapeRight && value == ScreenOrientation.Portrait)
                    {
                        _rotation.z = -90;
                        gameObject.GetComponent<RectTransform>().eulerAngles = _rotation;
                    }
                    else if (value == ScreenOrientation.LandscapeLeft)
                    {
                        _rotation.z = 90;
                        gameObject.GetComponent<RectTransform>().eulerAngles = _rotation;                
                    }
                    else if (value == ScreenOrientation.LandscapeRight)
                    {
                        _rotation.z = -90;
                        gameObject.GetComponent<RectTransform>().eulerAngles = _rotation;
                    } 
                }
                __orientation = value;
                OnRectTransformDimensionsChange();
            }
        }
    }

    private void Awake()
    { 
        __orientation = Screen.orientation;
        _orientation = Screen.orientation;
        _originalOrientation = Screen.orientation;
        _rectTransform = GetComponent<RectTransform>();
        _rotation = _rectTransform.eulerAngles;
    }

    private void Update()
    {
        _orientation = Screen.orientation;
    }
 
    private void OnRectTransformDimensionsChange() 
    {
         if (!_rectTransform) {
             return;
         }
         if (!transform.parent) {
             return;
         }
         RectTransform parentTransform = transform.parent.GetComponent<RectTransform>();
         if (!parentTransform) {
             return;
         }

         if (Screen.orientation == _originalOrientation)
         {
             _rectTransform.anchorMin = Vector2.zero;
             _rectTransform.anchorMax = Vector2.one;
         }
         else
         {
            float aspectRatio = parentTransform.rect.size.x / parentTransform.rect.size.y;
            float halfAspectRatio = aspectRatio / 2.0f;
            float halfAspectRatioInvert = (1.0f / aspectRatio) / 2.0f;
            _rectTransform.anchorMin = new Vector2(0.5f - halfAspectRatioInvert, 0.5f - halfAspectRatio);
            _rectTransform.anchorMax = new Vector2(0.5f + halfAspectRatioInvert, 0.5f + halfAspectRatio);
         }
         _rectTransform.anchoredPosition = Vector3.zero;
         _rectTransform.offsetMin = Vector2.zero;
         _rectTransform.offsetMax = Vector2.zero;
    }
}
