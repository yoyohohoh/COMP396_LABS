using UnityEngine;
using TMPro;
using System;
using System.Linq;
using static UnityEditor.PlayerSettings;
using UnityEngine.SocialPlatforms;

public class SlotMachineW : MonoBehaviour
{
    [Serializable]
    public enum SlotState
    {
        Cherry,
        Lemon,
        Orange,
        Plum,
        Bar,
        Seven
    }

    [Serializable]
    public struct SlotProbability
    {
        public SlotState state;
        public int weight;
    }

    public SlotProbability[] slotStates;
    public float spinDuration = 2.0f;
    public TMP_Text firstReel;
    public TMP_Text secondReel;
    public TMP_Text thirdReel;
    public TMP_Text resultText;
    public TMP_Text totalCredits;
    public TMP_InputField inputBet;
    public GameObject firstReelObj;
    public GameObject secondReelObj;
    public GameObject thirdReelObj;


    private bool startSpin = false;
    private bool firstReelSpinned = false;
    private bool secondReelSpinned = false;
    private bool thirdReelSpinned = false;

    private int betAmount;
    private int credits = 1000;
    private SlotState firstReelResult;
    private SlotState secondReelResult;
    private SlotState thirdReelResult;

    private float elapsedTime = 0.0f;

    void Start()
    {
        // Ensure slotStates is initialized with default values if not set in Inspector
        if (slotStates.Length == 0)
        {
            slotStates = new SlotProbability[]
            {
                new SlotProbability { state = SlotState.Cherry, weight = 10 },
                new SlotProbability { state = SlotState.Lemon, weight = 20 },
                new SlotProbability { state = SlotState.Orange, weight = 30 },
                new SlotProbability { state = SlotState.Plum, weight = 40 },
                new SlotProbability { state = SlotState.Bar, weight = 50 },
                new SlotProbability { state = SlotState.Seven, weight = 60 }
            };
        }

        Debug.Log($"Slot States Length: {slotStates.Length}");
        for (int i = 0; i < slotStates.Length; i++)
        {
            Debug.Log($"Slot State {i}: {slotStates[i].state}, Weight: {slotStates[i].weight}");
        }
    }

    public void Spin()
    {
        if (betAmount > 0)
        {
            startSpin = true;
        }
        else
        {
            resultText.text = "Insert a valid bet!";
        }
    }

    private void OnGUI()
    {
        try
        {
            betAmount = int.Parse(inputBet.text);
        }
        catch
        {
            betAmount = 0;
        }
        totalCredits.text = credits.ToString();
    }

    SlotState SelectSlotState()
    {
        if (slotStates.Length == 0)
        {
            throw new Exception("Slot states array is empty!");
        }

        // Sum the weights of every state.
        var weightSum = slotStates.Sum(state => state.weight);
        if (weightSum <= 0)
        {
            throw new Exception("Total weight must be greater than 0!");
        }

        Debug.Log($"Total weight: {weightSum}");

        var randomNumber = UnityEngine.Random.Range(0, weightSum);
        Debug.Log($"Random number: {randomNumber}");

        var i = 0;

        // Iterate through the states and subtract the weight until the right state is chosen.
        while (i < slotStates.Length)
        {
            var state = slotStates[i];
            Debug.Log($"Checking state: {state.state}, weight: {state.weight}");

            randomNumber -= state.weight;

            if (randomNumber < 0)
            {
                return state.state;
            }

            i++;
        }

        // It is not possible to reach this point unless something is wrong.
        throw new Exception("Something is wrong in the SelectSlotState algorithm!");
    }

    void CheckResult()
    {
        // Check for exact match
        if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult)
        {
            resultText.text = "YOU WIN!";
            credits += 500 * betAmount;
        }
        else
        {
            // Check for near-miss condition
            if ((firstReelResult == secondReelResult || secondReelResult == thirdReelResult || firstReelResult == thirdReelResult) &&
                (firstReelResult == SlotState.Seven || secondReelResult == SlotState.Seven || thirdReelResult == SlotState.Seven))
            {
                resultText.text = "NEAR MISS!";
            }
            else
            {
                resultText.text = "YOU LOSE!";
                credits -= betAmount;
            }
        }
    }

    void FixedUpdate()
    {
        if (startSpin)
        {
            elapsedTime += Time.deltaTime;
            SlotState randomSpinResult = SelectSlotState();

            if (!firstReelSpinned)
            {
                //firstReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    firstReelResult = randomSpinResult;
                    firstReelSpinned = true;
                    elapsedTime = 0;
                }
                showReel(firstReelObj, randomSpinResult);

            }
            else if (!secondReelSpinned)
            {
                //secondReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    secondReelResult = randomSpinResult;
                    secondReelSpinned = true;
                    elapsedTime = 0;
                }
                showReel(secondReelObj, randomSpinResult);
            }
            else if (!thirdReelSpinned)
            {
                //thirdReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    thirdReelResult = randomSpinResult;
                    startSpin = false;
                    elapsedTime = 0;
                    firstReelSpinned = false;
                    secondReelSpinned = false;

                    CheckResult();  // Check the result of the spin after all reels have finished spinning
                }
                showReel(thirdReelObj, randomSpinResult);
            }
        }
    }

    void showReel(GameObject ReelObj, SlotState randomSpinResult)
    {
        Transform cherryTransform = ReelObj.transform.Find("Cherry");
        Transform lemonTransform = ReelObj.transform.Find("Lemon");
        Transform orangeTransform = ReelObj.transform.Find("Orange");
        Transform plumTransform = ReelObj.transform.Find("Plum");
        Transform barTransform = ReelObj.transform.Find("Bar");
        Transform sevenTransform = ReelObj.transform.Find("Seven");
        //Cherry,
        //Lemon,
        //Orange,
        //Plum,
        //Bar,
        //Seven
        GameObject cherry = cherryTransform.gameObject;
        GameObject lemon = lemonTransform.gameObject;
        GameObject orange = orangeTransform.gameObject;
        GameObject plum = plumTransform.gameObject;
        GameObject bar = barTransform.gameObject;
        GameObject seven = sevenTransform.gameObject;

        // Check if the random spin result matches the first slot state
        if (randomSpinResult == SlotState.Cherry)
        {
            // Enable the cherry GameObject
            cherry.SetActive(true);
            lemon.SetActive(false);
            orange.SetActive(false);
            plum.SetActive(false);
            bar.SetActive(false);
            seven.SetActive(false);
        }
        else if(randomSpinResult == SlotState.Lemon)
        {
            cherry.SetActive(false);
            lemon.SetActive(true);
            orange.SetActive(false);
            plum.SetActive(false);
            bar.SetActive(false);
            seven.SetActive(false);
        }
        else if (randomSpinResult == SlotState.Orange)
        {
            cherry.SetActive(false);
            lemon.SetActive(false);
            orange.SetActive(true);
            plum.SetActive(false);
            bar.SetActive(false);
            seven.SetActive(false);
        }
        else if (randomSpinResult == SlotState.Plum)
        {
            cherry.SetActive(false);
            lemon.SetActive(false);
            orange.SetActive(false);
            plum.SetActive(true);
            bar.SetActive(false);
            seven.SetActive(false);
        }
        else if (randomSpinResult == SlotState.Bar)
        {
            cherry.SetActive(false);
            lemon.SetActive(false);
            orange.SetActive(false);
            plum.SetActive(false);
            bar.SetActive(true);
            seven.SetActive(false);
        }
        else if (randomSpinResult == SlotState.Seven)
        {
            cherry.SetActive(false);
            lemon.SetActive(false);
            orange.SetActive(false);
            plum.SetActive(false);
            bar.SetActive(false);
            seven.SetActive(true);
        }

    }
}
