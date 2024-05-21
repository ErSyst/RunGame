using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquationManager : MonoBehaviour
{
    public int currentAnswer;
    public int result;

    public Text equationText;
    
    public void GenerateRandomEquation()
    {
        int operand1 = Random.Range(1, 11);
        int operand2 = Random.Range(1, 11);
        char operatorChar = GetRandomOperator();

        if (operatorChar == '/')
        {
            operand2 = GetDivisor(operand1);
        }

        currentAnswer = CalculateAnswer(operand1, operand2, operatorChar);

        bool isIncorrect = Random.Range(0, 2) == 0;
        result = isIncorrect ? GetIncorrectResult() : currentAnswer;

        string equation = $"{operand1} {operatorChar} {operand2} = {result}";

        equationText.text = equation;
    }
    
    public bool CheckAnswer(int playerAnswer)
    {
        return playerAnswer == currentAnswer;
    }

    private int CalculateAnswer(int operand1, int operand2, char operatorChar)
    {
        switch (operatorChar)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                if (operand2 == 0)
                {
                    Debug.LogError("Division by zero");
                    return 0;
                }
                return operand1 / operand2;
            default:
                Debug.LogError("Invalid operator");
                return 0;
        }
    }

    private char GetRandomOperator()
    {
        char[] operators = { '+', '-', '*', '/' };
        int randomIndex = Random.Range(0, operators.Length);
        return operators[randomIndex];
    }

    private int GetIncorrectResult()
    {
        int offset = Random.Range(-1, 5);
        return currentAnswer + offset;
    }

    private int GetDivisor(int dividend)
    {
        List<int> factors = new List<int>();
        for (int i = 1; i < dividend; i++)
        {
            if (dividend % i == 0)
            {
                factors.Add(i);
            }
        }

        if (factors.Count == 0)
        {
            return 1;
        }

        int randomIndex = Random.Range(0, factors.Count);
        return factors[randomIndex];
    }
}
