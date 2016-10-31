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
    public enum GameState
    {
        StartMenu,
        Playing,
        Lost,
        Restarting
    }
    public Seasons currentSeason;
    public GameState currentGameState;
    public GameObject currentCritterPrefab;
    public GameObject currentCritterInGame;
    public GameObject summerCritter, autumnCritter, winterCritter, springCritter;
    public GameObject[] summerCritterSpots, autumnCritterSpots, winterCritterSpots, springCritterSpots;
    public float chi;
    public int shou;
    public float seasonSwitchTime;
    private float pSeasonTime;
    public LayerMask onlyCritters;
    private UIController uiController;
    // Use this for initialization
    void Start ()
    {
        tree = GameObject.Find("Tree");
        uiController = GetComponent<UIController>();
        SelectCritter();
        pSeasonTime = seasonSwitchTime;
    }
	
	void Update()
    {
       
        switch (currentGameState)
        {
            case GameState.StartMenu:
                break;
            case GameState.Playing:
                if (Input.GetMouseButtonDown(0))
                {
                    ShootRay();
                }
                CountdownAndSwitchSeasons();
                if (currentCritterInGame != null)
                    HideCritter();
                break;
            case GameState.Lost:
                break;
            case GameState.Restarting:
                break;
            default:
                break;
        }
   
        
       
    }
	void FixedUpdate ()
    {
        chi -= Time.fixedDeltaTime;
	}
    void ChangeSeason(Seasons toSeason)
    {
        currentSeason = toSeason;
    }
    public void SelectCritter()
    {
        int selection = Random.Range(0, 4);
        switch(selection)
        {
            case 3:
                currentCritterPrefab = summerCritter;
                SpawnCritter(summerCritterSpots);
                return;
            case 2:
                currentCritterPrefab = autumnCritter;
                SpawnCritter(autumnCritterSpots);
                return;
            case 1:
                currentCritterPrefab = winterCritter;
                SpawnCritter(winterCritterSpots);
                return;              
            case 0:
                currentCritterPrefab = springCritter;
                SpawnCritter(springCritterSpots);
                return;
        }
    }
    void SpawnCritter(GameObject[] spots)
    {
        int spotSelect = Random.Range(0, spots.Length - 1);
        CritterSpotVariables cSpotVars = spots[spotSelect].GetComponent<CritterSpotVariables>();
        currentCritterInGame = (GameObject)Instantiate(currentCritterPrefab, spots[spotSelect].transform.position+cSpotVars.offset, cSpotVars.rot);
    }
    public void HideCritter()
    {
        switch (currentSeason)
        {
            case Seasons.Summer:
                if (currentCritterPrefab == summerCritter)
                    currentCritterInGame.SetActive(true);
                else currentCritterInGame.SetActive(false);
                break;
            case Seasons.Autumn:
                if (currentCritterPrefab == autumnCritter)
                    currentCritterInGame.SetActive(true);
                else currentCritterInGame.SetActive(false);
                break;
            case Seasons.Winter:
                if (currentCritterPrefab == winterCritter)
                    currentCritterInGame.SetActive(true);
                else currentCritterInGame.SetActive(false);
                break;
            case Seasons.Spring:
                if (currentCritterPrefab == springCritter)
                    currentCritterInGame.SetActive(true);
                else currentCritterInGame.SetActive(false);
                break;
            default:
                Debug.LogError("No seasons! How'd you manage that?");
                break;
        }
    }
    public void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if(Physics.Raycast(ray,out rayHit,100f, onlyCritters.value))
        {
            Debug.Log(rayHit.collider.tag);
            if(rayHit.collider.tag.Contains("Critter"))
            {
                ClickedCritter();
                SelectCritter();
            }
        }
    }
    public void ClickedCritter()
    {
        shou++;
        chi += 6;
        Destroy(currentCritterInGame);
    }
    void CountdownAndSwitchSeasons()
    {
        pSeasonTime -= Time.deltaTime;
        if(pSeasonTime<=0)
        {
            if (currentSeason == Seasons.Summer)
                currentSeason = Seasons.Autumn;
            else if (currentSeason == Seasons.Autumn)
                currentSeason = Seasons.Winter;
            else if (currentSeason == Seasons.Winter)
                currentSeason = Seasons.Spring;
            else if (currentSeason == Seasons.Spring)
                currentSeason = Seasons.Summer;
            ResetSeasonTime();
        }
    }
    void ResetSeasonTime()
    {
        pSeasonTime = seasonSwitchTime;
    }
    public void StartTheGame()
    {
        currentGameState = GameState.Playing;
    }
    public void EndTheGame()
    {
        Application.Quit();
    }
}
