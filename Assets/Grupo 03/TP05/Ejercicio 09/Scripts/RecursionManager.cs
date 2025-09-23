using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecursionManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text outputText;

    public void CalculateFibonacci()
    {
        if (!ValidarNumero(out int n)) return;
        if (n < 0)
        {
            outputText.text = "Error: El número debe ser mayor o igual a 0.";
            return;
        }

        string result = "";
        for (int i = 0; i < n; i++)
            result += Fibonacci(i) + (i < n - 1 ? ", " : "");
        outputText.text = result;
    }

    int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public void CalculateFactorial()
    {
        if (!ValidarNumero(out int n)) return;
        if (n < 0)
        {
            outputText.text = "Error: El número debe ser mayor o igual a 0.";
            return;
        }

        outputText.text = $"Factorial de {n} es: {Factorial(n)}";
    }

    int Factorial(int n)
    {
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    public void CalculateSum()
    {
        if (!ValidarNumero(out int n)) return;

        outputText.text = $"Suma hasta {n} es: {Sum(n)}";
    }

    int Sum(int n)
    {
        if (n <= 0) return 0;
        return n + Sum(n - 1);
    }

    public void GeneratePyramid()
    {
        if (!ValidarNumero(out int height)) return;
        if (height <= 0)
        {
            outputText.text = "Error: La altura debe ser mayor que 0.";
            return;
        }

        string pyramid = BuildPyramid(height, 1);
        outputText.text = pyramid;
    }

    string BuildPyramid(int height, int level)
    {
        if (level > height) return "";
        int spaces = height - level;
        int xCount = level * 2 - 1;
        string line = new string(' ', spaces) + new string('x', xCount) + "\n";
        return line + BuildPyramid(height, level + 1);
    }

    public void CheckPalindrome()
    {
        string text = inputField.text.ToLower().Replace(" ", "");
        if (string.IsNullOrEmpty(text))
        {
            outputText.text = "Error: Ingrese una frase para verificar.";
            return;
        }

        bool isPalindrome = IsPalindrome(text, 0, text.Length - 1);
        outputText.text = isPalindrome ? "Es palíndromo" : "No es palíndromo";
    }

    bool IsPalindrome(string text, int left, int right)
    {
        if (left >= right) return true;
        if (text[left] != text[right]) return false;
        return IsPalindrome(text, left + 1, right - 1);
    }

    // ? Función auxiliar para validar números
    bool ValidarNumero(out int numero)
    {
        string input = inputField.text;
        if (string.IsNullOrEmpty(input))
        {
            outputText.text = "Error: El campo está vacío.";
            numero = 0;
            return false;
        }

        if (!int.TryParse(input, out numero))
        {
            outputText.text = "Error: Ingrese un número válido.";
            return false;
        }

        return true;
    }
}

