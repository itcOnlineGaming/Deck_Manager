using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
//read from a csv 
//make playing card struct
//have ability to just use one or the other card type
//could also make a class and pass it in and just get what is needed rather than the whole card
//need to have trello card ID in a commit to the package
//update the demo to show the new card type
//maybe add shuffling and grab from top of deck for the 2nd task
//take the csv parser from TOTC and mock up a small demo using a csv file
//yutokun is the call for the CSV parser
public class DeckManager : MonoBehaviour
{
    public List<Card> cardList = new List<Card>();
    public Card defaultCard;

    private void Start()
    {
        defaultCard.Type = "Type";
        defaultCard.Value = 0;
        defaultCard.Question = "Question";
        defaultCard.Answer = "Answer";
        defaultCard.FalseAnswer = "Incorrect";
        defaultCard.FalseAnswer2 = "Incorrect2";
    }

    public struct Card
    {
        public string Type;//face card subject type etc
        public int Value;//card num or just a number to easily find//must be a unique number per type

        public string Question;//the question associated with this card
        public string Answer;//the correct answer

        public string FalseAnswer;//false answer
        public string FalseAnswer2;//extra false answers if needed
    }

    public void addNewCards(string t_cardType, int t_value,string t_question,string t_answer,string t_wrongAnswer, string t_wrongAnswer2)
    {
        Card tempCard = new Card();
        tempCard.Type = t_cardType;
        tempCard.Value = t_value;
        tempCard.Question = t_question;
        tempCard.Answer = t_answer;
        tempCard.FalseAnswer = t_wrongAnswer;
        tempCard.FalseAnswer2 = t_wrongAnswer2;

        //make sure the data is valid before adding it to the list
        if (t_cardType != null || t_question != null || t_answer != null || t_wrongAnswer != null || t_wrongAnswer2 != null)
        {
            cardList.Add(tempCard);
        }
    }

    public void generateBulkCards(List<string> t_cardType, List<int> t_value, List<string> t_question, List<string> t_answer, List<string> t_wrongAnswer, List<string> t_wrongAnswer2)
    {
        for(int index = 0;index < t_cardType.Count;index++)
        {
            addNewCards(t_cardType[index], t_value[index], t_question[index], t_answer[index],t_wrongAnswer[index], t_wrongAnswer2[index]);
        }
    }

    public Card getSpecifiedCard(string t_cardType, int t_number)
    {
        List<string> questions = new List<string>();
        List<string> correctAnswers = new List<string>();
        List<string> falseAnswer = new List<string>();
        List<string> falseAnswer2 = new List<string>();

        for (int index = 0; index < cardList.Count; index++)
        {
            if (cardList[index].Type == t_cardType)
            {
                questions.Add(cardList[index].Question);
                correctAnswers.Add(cardList[index].Answer);
                falseAnswer.Add(cardList[index].FalseAnswer);
                falseAnswer2.Add(cardList[index].FalseAnswer2);
            }
        }

        if(t_number > questions.Count || questions.Count == 0)
        {
            return defaultCard;
        }

        Card temp = new Card();
        temp.Type = t_cardType;
        temp.Value = t_number;
        temp.Question = questions[t_number];
        temp.Answer = correctAnswers[t_number];
        temp.FalseAnswer = falseAnswer[t_number];
        temp.FalseAnswer2 = falseAnswer2[t_number];

        return temp;
    }

    public Card getRandomCardOfType(string t_cardType)
    {
        List<string> questions = new List<string>();//store all of the requested types data
        List<string> correctAnswers = new List<string>();
        List<string> falseAnswer = new List<string>();
        List<string> falseAnswer2 = new List<string>();

        for (int index = 0; index < cardList.Count; index++)
        {
            if (cardList[index].Type == t_cardType)
            {
                questions.Add(cardList[index].Question);
                correctAnswers.Add(cardList[index].Answer);
                falseAnswer.Add(cardList[index].FalseAnswer);
                falseAnswer2.Add(cardList[index].FalseAnswer2);
            }
        }

        if(questions.Count == 0) 
        {
            return defaultCard;
        }

        int randomIndex = UnityEngine.Random.Range(0, questions.Count);

        Card temp = new Card();
        temp.Type = t_cardType;
        temp.Value = randomIndex;
        temp.Question = questions[randomIndex];
        temp.Answer = correctAnswers[randomIndex];
        temp.FalseAnswer = falseAnswer[randomIndex];
        temp.FalseAnswer2 = falseAnswer2[randomIndex];

        return temp;
    }

    public Card getRandomCard()
    {
        if(cardList.Count == 0)
        {
            return defaultCard;
        }
        int randomCard = UnityEngine.Random.Range(0, cardList.Count);
        return cardList[randomCard];
    }
}
