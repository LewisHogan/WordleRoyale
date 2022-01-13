using Xunit;

using WordleRoyale.Core;
using WordleRoyale.Core.Models;

using System;

namespace WordleRoyale.Tests;

public class WordleSessionTest
{
    [Fact]
    public void LetterStates_AllCorrect_AllValidCharactersInGuess()
    {
        var letterStates = Wordle.CalculateLetterStates("ready", "ready");
        Assert.Equal(
            new LetterState[] { LetterState.CORRECT, LetterState.CORRECT, LetterState.CORRECT, LetterState.CORRECT, LetterState.CORRECT },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_AllWrongLetter_NoValidCharactersInGuess()
    {
        var letterStates = Wordle.CalculateLetterStates("sling", "ready");
        Assert.Equal(
            new LetterState[] { LetterState.WRONG_LETTER, LetterState.WRONG_LETTER, LetterState.WRONG_LETTER, LetterState.WRONG_LETTER, LetterState.WRONG_LETTER },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_AllWrongPosition_AllWrongPositionCharactersInGuess()
    {
        var letterStates = Wordle.CalculateLetterStates("alert", "later");
        Assert.Equal(
            new LetterState[] { LetterState.WRONG_POSITION, LetterState.WRONG_POSITION, LetterState.WRONG_POSITION, LetterState.WRONG_POSITION, LetterState.WRONG_POSITION },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_SomeCorrect_AllValidCharactersInGuessExceptSecondWrongLetter()
    {
        var letterStates = Wordle.CalculateLetterStates("dance", "dunce");
        Assert.Equal(
            new LetterState[] { LetterState.CORRECT, LetterState.WRONG_LETTER, LetterState.CORRECT, LetterState.CORRECT, LetterState.CORRECT },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_SomeCorrect_DuplicateLetterAndWrongLetter()
    {
        var letterStates = Wordle.CalculateLetterStates("cool", "cold");
        Assert.Equal(
            new LetterState[] { LetterState.CORRECT, LetterState.CORRECT, LetterState.WRONG_LETTER, LetterState.WRONG_POSITION },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_SomeCorrect_AllValidCharactersInGuessExceptWrongPosition()
    {
        var letterStates = Wordle.CalculateLetterStates("claps", "clasp");
        Assert.Equal(
            new LetterState[] { LetterState.CORRECT, LetterState.CORRECT, LetterState.CORRECT, LetterState.WRONG_POSITION, LetterState.WRONG_POSITION },
            letterStates
        );
    }

    [Fact]
    public void LetterStates_ThrowsException_InvalidGuessLength()
    {
        Assert.Throws<ArgumentException>(() => Wordle.CalculateLetterStates("test", "ready"));
    }

}