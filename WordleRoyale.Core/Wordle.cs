using WordleRoyale.Core.Models;

namespace WordleRoyale.Core;

public class Wordle
{
    /// <summary>
    /// Calculates the state of each letter within the guess and returns an array of the results.
    /// <para>A letter within the guess can be considered to be in one of three states:
    /// <list type="number">
    /// <item>
    /// <description>The letter is not in the answer.</description>
    /// </item>
    /// <item>
    /// <description>The letter is in the answer, but the position of the letter is wrong.</description>
    /// </item>
    /// <item>
    /// <description>The letter is in the answer and in the correct location.</description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="guess">The word used as a guess.</param>
    /// <param name="answer">The correct answer.</param>
    public static LetterState[] CalculateLetterStates(string guess, string answer)
    {
        if (guess.Length != answer.Length) throw new ArgumentException($"'{guess}' and '{answer}' must be the same length.");

        var letterStates = new LetterState[answer.Length];
        // Every result is invalid unless proven to be otherwise
        Array.Fill(letterStates, LetterState.WRONG_LETTER);

        var answerLetterCounts = new Dictionary<char, int>();

        foreach (var letter in answer)
        {
            if (answerLetterCounts.ContainsKey(letter))
            {
                answerLetterCounts[letter]++;
            }
            else
            {
                answerLetterCounts[letter] = 1;
            }
        }
        // Check for correct letters first, since they should be prioritised if we have any double letters when we don't need any
        for (int answerIdx = 0; answerIdx < answer.Length; answerIdx++)
        {
            var answerLetter = answer[answerIdx];
            if (guess[answerIdx] == answer[answerIdx])
            {
                letterStates[answerIdx] = LetterState.CORRECT;
                answerLetterCounts[answerLetter]--;
            }
        }

        // Now we check for any letters which are out of order
        for (int guessIdx = 0; guessIdx < guess.Length; guessIdx++)
        {
            var guessLetter = guess[guessIdx];

            if (guessLetter != answer[guessIdx] && answerLetterCounts.ContainsKey(guessLetter) && answerLetterCounts[guessLetter] != 0)
            {
                letterStates[guessIdx] = LetterState.WRONG_POSITION;
                answerLetterCounts[guessLetter]--;
            }
        }


        return letterStates;
    }
}
