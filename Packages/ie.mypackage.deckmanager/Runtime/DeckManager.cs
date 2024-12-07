using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> cardList;
    public List<string> allTypes;
    int type1Number = 0;

    public struct Card
    {
        public string Type;//face card subject type etc
        public int Value;//card num or just a number to easily find//must be a unique number per type

        public string Question;//the question associated with this card
        public string Answer;//the correct answer

        public string FalseAnswer;//false answer
        public string FalseAnswer2;//extra false answers if needed
    }

    public void addNewCards(string t_type, int t_value,string t_question,string t_answer,string t_falseAnswer, string t_falseAnswer2)
    {
        Card tempCard = new Card();
        tempCard.Type = t_type;
        tempCard.Value = t_value;
        tempCard.Question = t_question;
        tempCard.Answer = t_answer;
        tempCard.FalseAnswer = t_falseAnswer;
        tempCard.FalseAnswer2 = t_falseAnswer2;

        cardList.Add(tempCard);
    }

    public void generateBulkCards(List<string> t_type, List<int> t_value, List<string> t_question, List<string> t_answer, List<string> t_falseAnswer, List<string> t_falseAnswer2)
    {
        for(int index = 0;index < t_type.Count;index++)
        {
            addNewCards(t_type[index], t_value[index], t_question[index], t_answer[index],t_falseAnswer[index], t_falseAnswer2[index]);
        }
    }

    public Card getSpecifiedCard(string t_type,int t_number)
    {
        int indexOfSpecifiedType = 0;

        for(int index = 0;index<cardList.Count;index++)
        {
            if (cardList[index].Type == t_type)//is it the correct type
            {
                if(indexOfSpecifiedType == t_number)//is it the correct number
                {
                    return cardList[index];
                }

                indexOfSpecifiedType++;//look for the next number 
            }
        }

        return cardList[0];//return a default card as none were found that matched
    }

    public Card getRandomCardOfType(string t_type)
    {
        int randomCard = 0;
        bool typeFound = false;
        int indexToCorrectType = 0;
        int typeSize = 0;

        for (int index = 0; index < cardList.Count; index++)
        {
            if (cardList[index].Type == t_type)//is it the correct type
            {
                if(typeFound == false)//only want to find the first one
                {
                    indexToCorrectType = index;//how far in until we find the type
                }
                typeFound = true;

                typeSize++;//track how many of this type there are
            }
        }

        randomCard = UnityEngine.Random.Range(0, typeSize);//get a random card from the range of found cards
        randomCard += indexToCorrectType;//add on how far into the list it is so we get the correct card

        return cardList[randomCard];
    }

    public Card getRandomCard()
    {
        int randomCard = UnityEngine.Random.Range(0, cardList.Count);
        return cardList[randomCard];
    }
}
