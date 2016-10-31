using UnityEngine;
using System.Collections;
using Prism;

public class SeasonalColorControl : MonoBehaviour {

    private GameController gControl;

    private PrismEffects pEffects;

    private Light sun;

    public Color summerMainColor, autumnMainColor, winterMainColor, springMainColor;
    // Use this for initialization
    void Start ()
    {
        gControl = GetComponent<GameController>();
        pEffects = FindObjectOfType<PrismEffects>();
        sun = FindObjectOfType<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch(gControl.currentSeason)
        {
            case GameController.Seasons.Autumn:
                SetColors(autumnMainColor);
                return;
            case GameController.Seasons.Spring:
                SetColors(springMainColor);
                return;
            case GameController.Seasons.Summer:
                SetColors(summerMainColor);
                return;
            case GameController.Seasons.Winter:
                SetColors(winterMainColor);
                return;
        }
	}
    void SetColors(Color mainColor)
    {
        sun.color = mainColor;
    }
}
