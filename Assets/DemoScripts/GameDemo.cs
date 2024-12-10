using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDemo : MonoBehaviour
{
    public TMP_Text Type;
    public TMP_Text Value;
    public TMP_Text Question;
    public TMP_Text Answer;
    public TMP_Text Incorrect;
    public TMP_Text Incorrect2;

    public DeckManager DeckManagerQuestions;//multiple decks can be made per game
    public DeckManager DeckManagerPlayingCards;

    public List<string> demoType;
    public List<int> demoValue;
    public List<string> demoQuestions;
    public List<string> demoAnswer;
    public List<string> demoIncorrect;
    public List<string> demoIncorrect2;

    // Start is called before the first frame update
    void Start()
    {
        for(int index = 0;index < 10;index++)
        {//create lists of all of the data you want your cards to have
            demoType.Add("Type of Card: " + index);
            demoValue.Add(index);
            demoQuestions.Add("Question: " + index);
            demoAnswer.Add("Answer: " + index);
            demoIncorrect.Add("Incorrect: " + index);
            demoIncorrect2.Add("Incorrect 2: " + index);
        }
        DeckManagerQuestions.generateBulkQuestionCards(demoType, demoValue, demoQuestions, demoAnswer, demoIncorrect, demoIncorrect2);

        demoType.Clear();//make sure if you re use list that you clear them to avoid unwanted data
        demoValue.Clear();

        for (int index = 0; index < 13; index++)//deck of cards demo
        {
            demoType.Add("Spades");
            demoValue.Add(index+1);
        }
        for (int index = 0; index < 13; index++)
        {
            demoType.Add("Hearts");
            demoValue.Add(index + 1);
        }
        for (int index = 0; index < 13; index++)
        {
            demoType.Add("Clubs");
            demoValue.Add(index + 1);
        }
        for (int index = 0; index < 13; index++)
        {
            demoType.Add("Diamonds");
            demoValue.Add(index + 1);
        }
        DeckManagerPlayingCards.generateBulkPlayingCards(demoType, demoValue);
    }

    public void getNewQCard()
    {//will return any random card
        var tempCard = DeckManagerQuestions.getRandomQuestionCard();

        Type.text = tempCard.Type;
        Value.text = "Value "+ tempCard.Value;
        Question.text = tempCard.Question;
        Answer.text = tempCard.Answer;
        Incorrect.text = tempCard.FalseAnswer;
        Incorrect2.text = tempCard.FalseAnswer2;
    }

    public void getBadCard()
    {//will return the default card if a requested card doesnt exist
        var tempCard = DeckManagerQuestions.getRandomQuestionCardOfType("Any Junk Value");

        Type.text = tempCard.Type;
        Value.text = "Value " + tempCard.Value;
        Question.text = tempCard.Question;
        Answer.text = tempCard.Answer;
        Incorrect.text = tempCard.FalseAnswer;
        Incorrect2.text = tempCard.FalseAnswer2;
    }

    public void getNormalCard()
    {
        //return a completely random card
        var tempCard = DeckManagerPlayingCards.getRandomPlayingCard();

        Type.text = tempCard.Type;
        Value.text = "Card " + tempCard.Value;

        if(tempCard.Value == 1)
        {
            Value.text = "Ace";
        }
        if (tempCard.Value == 11)
        {
            Value.text = "Jack";
        }
        if (tempCard.Value == 12)
        {
            Value.text = "Queen";
        }
        if (tempCard.Value == 13)
        {
            Value.text = "King";
        }

        Question.text = " ";//hide the text used during the demo as its not needed for these
        Answer.text = " ";
        Incorrect.text = " ";
        Incorrect2.text = " ";
    }

    public void getShuffledCard()
    {
        //this will shuffle the deck if it hadnt alreay been shuffled and draw cards 1 at a tiem from the
        //top of the deck - it will reshuffle once all cards a have been drawn
        var tempCard = DeckManagerPlayingCards.getShuffledCard();

        Type.text = tempCard.Type;
        Value.text = "Card " + tempCard.Value;

        if (tempCard.Value == 1)
        {
            Value.text = "Ace";
        }
        if (tempCard.Value == 11)
        {
            Value.text = "Jack";
        }
        if (tempCard.Value == 12)
        {
            Value.text = "Queen";
        }
        if (tempCard.Value == 13)
        {
            Value.text = "King";
        }

        Question.text = " ";//hide the text used during the demo as its not needed for these
        Answer.text = " ";
        Incorrect.text = " ";
        Incorrect2.text = " ";
    }
}
