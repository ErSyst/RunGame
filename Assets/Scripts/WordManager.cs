using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    public Text wordText;

    private string currentWord;
    private string displayedWord;
    private bool isWordCorrect;

    private List<string> words = new List<string>()
    {
        "�����", "�������", "���������", "����������", "��������", "���������",
        "�����������", "�������", "������", "�������", "������������", "�������",
        "�������", "�����������", "����������", "�������", "���������", "���������",
        "��������", "��������", "�������", "�������", "����������", "������",
        "�����", "��������", "���������", "�������", "���������", "�����������",
        "����������", "���������", "�����������", "����������", "����������",
        "�����������", "��������", "�����������", "�������������", "�������",
        "����������", "����������������", "�����������", "����", "������", "������",
        "����������", "������", "������", "���� ������", "���������", "��������",
        "������", "���������", "�����������", "�����������", "�����������", "����������"
    };

    private Dictionary<char, char> commonMistakes = new Dictionary<char, char>()
    {
        { '�', '�' }, { '�', '�' },
        { '�', '�' }, { '�', '�' },
        { '�', '�' }, { '�', '�' },
        { '�', '�' }, { '�', '�' },
        { '�', '�' }, { '�', '�' },
        { '�', '�' }, { '�', '�' }
    };

    public void GenerateRandomWord()
    {
        currentWord = words[Random.Range(0, words.Count)];
        isWordCorrect = Random.Range(0, 2) == 0;

        if (isWordCorrect)
        {
            displayedWord = currentWord;
        }
        else
        {
            displayedWord = MakeMistake(currentWord);
        }

        Debug.Log($"Generated word: {currentWord}, Displayed word: {displayedWord}, Is correct: {isWordCorrect}");

        wordText.text = displayedWord;
    }

    private string MakeMistake(string word)
    {
        char[] wordChars = word.ToCharArray();
        int mistakeIndex = Random.Range(0, wordChars.Length);
        char originalChar = wordChars[mistakeIndex];

        if (commonMistakes.ContainsKey(originalChar))
        {
            wordChars[mistakeIndex] = commonMistakes[originalChar];
        }
        else
        {
            wordChars[mistakeIndex] = (char)('�' + Random.Range(0, 33));
        }

        return new string(wordChars);
    }

    public bool CheckWord(bool playerAnswer)
    {
        Debug.Log($"Player answered: {playerAnswer}, Correct answer: {isWordCorrect}");
        return playerAnswer == isWordCorrect;
    }
}
