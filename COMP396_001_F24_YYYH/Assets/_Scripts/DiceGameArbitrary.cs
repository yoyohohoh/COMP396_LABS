using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiceGameAmbitrary : MonoBehaviour
{
    public string inputValue = "1";
    public TMP_Text outputText;
    public TMP_InputField inputField;
    public Button button;

    int throwDice()
    {
        Debug.Log("Throwing dice...");
        int randomProbability = Random.Range(0, 100);
        int diceResult = 0;

        if (randomProbability < 12)
        {
            diceResult = 1;  // 12% chance
        }
        else if (randomProbability < 22)
        {
            diceResult = 2;  // 10% chance
        }
        else if (randomProbability < 29)
        {
            diceResult = 3;  // 7% chance
        }
        else if (randomProbability < 44)
        {
            diceResult = 4;  // 15% chance
        }
        else if (randomProbability < 64)
        {
            diceResult = 5;  // 20% chance
        }
        else
        {
            diceResult = 6;  // 36% chance
        }

        Debug.Log("Result: " + diceResult);
        return diceResult;
    }

    public void processGame()
    {
        inputValue = inputField.text;
        try
        {
            int inputInteger = int.Parse(inputValue);
            int totalSix = 0;
            for (var i = 0; i < 10; i++)
            {
                var diceResult = throwDice();
                if (diceResult == 6) { totalSix++; }

                if (diceResult == inputInteger)
                {
                    outputText.text = $"DICE RESULT: {diceResult} \r\nYOU WIN!";
                }
                else
                {
                    outputText.text = $"DICE RESULT: {diceResult} \r\nYOU LOSE!";
                }
            }
            Debug.Log($"Total of six: {totalSix}");
        }
        catch
        {
            outputText.text = "Input is not a number!";
            Debug.LogError("Input is not a number!");
        }
    }
}
