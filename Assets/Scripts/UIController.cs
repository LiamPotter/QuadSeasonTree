using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    private Canvas canvas;

    public Text goalText, chiText,summerText,autumnText,winterText,springText,shouText;

    private Color summerColor, autumnColor, winterColor, springColor;

    public Image startMenu, loseMenu;

    private GameController gameController;
    private SeasonalColorControl ssControl;
    private float chi;
    private int shou;
    private string goal;
    public bool switchedSeasonUI;
    public bool doneDisplayingSeason;
    public bool doneRemovingSeason;
    private float lerpTimerDisplay;
    private float lerpTimerSwitch;
    private float lerpTimerSwitch2;
    // Use this for initialization
    void Start ()
    {
        gameController = GetComponent<GameController>();
        ssControl = GetComponent<SeasonalColorControl>();
        summerColor = summerText.color;
        autumnColor = autumnText.color;
        winterColor = winterText.color;
        springColor = springText.color;
        doneRemovingSeason = true;
        lerpTimerDisplay = 0;
	}
	
	void Update ()
    {
        shou = gameController.shou;
        chi = gameController.chi;
        goal = gameController.currentCritterPrefab.name;
        if (gameController.currentGameState == GameController.GameState.Playing)
        {
            chiText.gameObject.SetActive(true);
            shouText.gameObject.SetActive(true);
            goalText.gameObject.SetActive(true);
            chiText.text = "Chi - " + chi.ToString("F0");
            shouText.text = "Shou - " + shou.ToString();
            goalText.text = "Mùdì - " + goal;
            startMenu.gameObject.SetActive(false);
        }
        if(gameController.currentGameState == GameController.GameState.StartMenu)
        {
            startMenu.gameObject.SetActive(true);
            chiText.gameObject.SetActive(false);
            shouText.gameObject.SetActive(false);
            goalText.gameObject.SetActive(false);
        }

    }
    public void DisplaySeasonUI(Text toDisplay,Color oColor,float duration)
    {

        lerpTimerDisplay += Time.deltaTime; if (lerpTimerDisplay > 1) { lerpTimerDisplay = 1; }
        var perc = lerpTimerDisplay / duration;
        Color noAplha = new Color(oColor.r, oColor.g, oColor.b, 0);
        Color toApplyColor = Color.Lerp(noAplha, oColor, perc);
        if (toApplyColor.a == 1)
        {
            doneRemovingSeason = false;

            doneDisplayingSeason = true;
        }
        toDisplay.color = toApplyColor;
        //Debug.Log("Color stuff " + toApplyColor.a);
    }

    public void RemoveSeasonUI(Text toRemove, Color oColor,float duration)
    {
        lerpTimerDisplay += Time.deltaTime; if (lerpTimerDisplay > 1) { lerpTimerDisplay = 1; }
        var perc = lerpTimerDisplay / duration;
        Color noAplha = new Color(oColor.r, oColor.g, oColor.b, 0);
        Color toApplyColor = Color.Lerp(oColor, noAplha, perc);
        if (toApplyColor.a == 0)
        {
            doneRemovingSeason = true;
        }
        toRemove.color = toApplyColor;
    }

    public void SwitchSeasonUI(Text from, Text to,Color fromColor,Color toColor,float duration)
    {
        lerpTimerSwitch += Time.deltaTime; if (lerpTimerSwitch > 1) { lerpTimerSwitch = 1; }
        var perc = lerpTimerSwitch / duration;
        Color noAlphaFrom = new Color(fromColor.r, fromColor.g, fromColor.b,0);
        Color toApplyFrom = Color.Lerp(fromColor, noAlphaFrom, perc);
        Color noAlphaTo = new Color(toColor.r, toColor.g, toColor.b, 0);
        from.color = toApplyFrom;
        if (toApplyFrom.a == 0)
        {
            to.enabled = true;
            from.enabled = false;
            lerpTimerSwitch2 += Time.deltaTime; if (lerpTimerSwitch2 > 1) { lerpTimerSwitch2 = 1; }
            var perc2 = lerpTimerSwitch2 / duration;
            Color toApplyTo=Color.Lerp(noAlphaTo, toColor, perc2); ;
            to.color = toApplyTo;
            if (toApplyTo.a == 1)
                switchedSeasonUI = true;
        }
      
    }

    public void ResetLerpTimers()
    {
        lerpTimerDisplay = 0;
        lerpTimerSwitch = 0;
        lerpTimerSwitch2 = 0;
    }
}
