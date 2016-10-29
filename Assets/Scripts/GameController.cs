using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private GameObject tree;
    public enum Seasons
    {
        Summer,
        Autumn,
        Winter,
        Spring
    }

    public Seasons currentSeason;
    public GameObject summerCritter, autumnCritter, winterCritter, springCritter;
    // Use this for initialization
    void Start ()
    {
        tree = GameObject.Find("Tree");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
