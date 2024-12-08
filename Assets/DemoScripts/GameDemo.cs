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
    public DeckManager DeckManagerNormal;

    public List<string> demoType;
    public List<int> demoValue;
    public List<string> demoQuestions;
    public List<string> demoAnswer;
    public List<string> demoIncorrect;
    public List<string> demoIncorrect2;

    public struct Card
    {
        public string Type;//face card subject type etc
        public int Value;//card num or just a number to easily find//must be a unique number per type

        public string Question;//the question associated with this card
        public string Answer;//the correct answer

        public string FalseAnswer;//false answer
        public string FalseAnswer2;//extra false answers if needed
    }

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
        DeckManagerQuestions.generateBulkCards(demoType, demoValue, demoQuestions, demoAnswer, demoIncorrect, demoIncorrect2);

        demoType.Clear();//make sure if you re use list that you clear them to avoid unwanted data
        demoValue.Clear();
        demoQuestions.Clear();
        demoAnswer.Clear();
        demoIncorrect.Clear();
        demoIncorrect2.Clear();

        for (int index = 0; index < 13; index++)//deck of cards demo
        {
            demoType.Add("Spades");
            demoValue.Add(index+1);//add 1 as decks of cards start with 1 - ace and not 0
            demoQuestions.Add("Not Used");//need to assign a value but dont need to use it
            demoAnswer.Add("Not Used");
            demoIncorrect.Add("Not Used");
            demoIncorrect2.Add("Not Used");
        }
        DeckManagerNormal.generateBulkCards(demoType, demoValue, demoQuestions, demoAnswer, demoIncorrect, demoIncorrect2);
    }

    public void getNewQCard()
    {//will return any random card
        var tempCard = DeckManagerQuestions.getRandomCard();

        Type.text = tempCard.Type;
        Value.text = "Value "+ tempCard.Value;
        Question.text = tempCard.Question;
        Answer.text = tempCard.Answer;
        Incorrect.text = tempCard.FalseAnswer;
        Incorrect2.text = tempCard.FalseAnswer2;
    }

    public void getBadCard()
    {//will return the default card if a requested card doesnt exist
        var tempCard = DeckManagerQuestions.getRandomCardOfType("Any Junk Value");

        Type.text = tempCard.Type;
        Value.text = "Value " + tempCard.Value;
        Question.text = tempCard.Question;
        Answer.text = tempCard.Answer;
        Incorrect.text = tempCard.FalseAnswer;
        Incorrect2.text = tempCard.FalseAnswer2;
    }

    public void getNormalCard()
    {
        var tempCard = DeckManagerNormal.getRandomCard();

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

        Question.text = " ";
        Answer.text = " ";
        Incorrect.text = " ";
        Incorrect2.text = " ";
    }
}
