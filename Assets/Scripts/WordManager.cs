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
        "книга", "учебник", "компьютер", "информация", "алгоритм", "программа",
        "университет", "студент", "лекция", "семинар", "исследование", "экзамен",
        "задание", "диссертация", "библиотека", "кафедра", "факультет", "профессор",
        "аспирант", "бакалавр", "магистр", "инженер", "математика", "физика",
        "химия", "биология", "география", "история", "философия", "лингвистика",
        "литература", "экономика", "политология", "социология", "психология",
        "архитектура", "механика", "электроника", "робототехника", "авиация",
        "автоматика", "программирование", "кодирование", "сеть", "сервер", "клиент",
        "разработка", "анализ", "синтез", "база данных", "интерфейс", "алгоритм",
        "модель", "симуляция", "оптимизация", "верификация", "диагностика", "мониторинг"
    };

    private Dictionary<char, char> commonMistakes = new Dictionary<char, char>()
    {
        { 'о', 'а' }, { 'а', 'о' },
        { 'и', 'ы' }, { 'ы', 'и' },
        { 'я', 'е' }, { 'е', 'я' },
        { 'ю', 'у' }, { 'у', 'ю' },
        { 'с', 'з' }, { 'з', 'с' },
        { 'д', 'т' }, { 'т', 'д' }
    };

    public void GenerateRandomWord()
    {
        currentWord = words[Random.Range(0, words.Count)];
        isWordCorrect = Random.Range(0, 2) == 0;

        displayedWord = isWordCorrect ? currentWord : MakeMistake(currentWord);
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

        return new string(wordChars);
    }

    public bool CheckWord(bool playerAnswer)
    {
        return playerAnswer == isWordCorrect;
    }
}
