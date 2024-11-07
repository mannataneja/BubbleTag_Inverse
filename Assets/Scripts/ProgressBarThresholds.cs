using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    public Slider progressBar;
    public GameObject starThresholdMarker;
    public float[] thresholds;

    public float minBarValue;
    public float maxBarValue;

    private void Start()
    {
        if (progressBar != null && starThresholdMarker != null)
        {
            PositionThresholdMarkers();
        }
    }

    private void PositionThresholdMarkers()
    {
        float sliderHeight = progressBar.GetComponent<RectTransform>().rect.height;

        for (int i = 0; i < thresholds.Length; i++)
        {
            float thresholdValue = Mathf.Clamp(thresholds[i], minBarValue, maxBarValue);  
            float markerPositionX = thresholdValue * sliderHeight - (sliderHeight / 2);

            // Instantiate the star marker prefab
            GameObject starMarker = Instantiate(starThresholdMarker, progressBar.transform);
            RectTransform starTransform = starMarker.GetComponent<RectTransform>();

            // Set the anchored position of the star marker
            starTransform.anchoredPosition = new Vector2(markerPositionX, starTransform.anchoredPosition.y);
        }
    }

}
