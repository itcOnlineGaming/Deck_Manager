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
    public List<QuestionCard> questionCardList = new List<QuestionCard>();
    public QuestionCard defaultQuestionCard;

    public List<PlayingCard> playingCardList = new List<PlayingCard>();
    public PlayingCard defaultPlayingCard;

    public List<int> shuffledCardLocations = new List<int>();//a list of randomly assigned index locations
    int currentShuffledCard = 0;//the current shuffled card we are on in the list
    bool deckShuffled = false;//does the deck need to be shuffled

    private void Start()
    {
        defaultQuestionCard.Type = "Default Type";
        defaultQuestionCard.Value = 0;
        defaultQuestionCard.Question = "Default Question";
        defaultQuestionCard.Answer = "Default Answer";
        defaultQuestionCard.FalseAnswer = "Default Incorrect";
        defaultQuestionCard.FalseAnswer2 = "Default Incorrect2";

        defaultPlayingCard.Type = "Default Type";
        defaultPlayingCard.Value = 0;
    }

    public struct QuestionCard
    {
        public string Type;//face card subject type etc
        public int Value;//card num or just a number to easily find

        public string Question;//the question associated with this card
        public string Answer;//the correct answer

        public string FalseAnswer;//false answer
        public string FalseAnswer2;//extra false answers if needed
    }

    public struct PlayingCard//standard cards
    {
        public string Type;//face card subject type etc
        public int Value;
    }

    public void shufflePlayingCards()
    {
        //place the index of every card in order into the list
        for(int index = 0;index < playingCardList.Count;index++)
        {
            shuffledCardLocations.Add(index);
        }

        //shuffle the cards locations around then just call on this list and step through to pull cards
        for (int index = 0; index < playingCardList.Count; index++)
        {
            //store it so it can be swapped
            int storedIndex = shuffledCardLocations[index];
            int randomIndex = UnityEngine.Random.Range(0, playingCardList.Count);

            //place random index in this one
            shuffledCardLocations[index] = shuffledCardLocations[randomIndex];
            //give our index to the randomly found one
            shuffledCardLocations[randomIndex] = storedIndex;
        }
    }
    public PlayingCard getShuffledCard()
    {
        if(playingCardList.Count == 0)
        {//error checking
            return defaultPlayingCard;
        }

        if(deckShuffled == false)
        {
            shufflePlayingCards();
            deckShuffled = true;
        }
        PlayingCard shuffledCard = playingCardList[shuffledCardLocations[currentShuffledCard]];
        currentShuffledCard++;//move on to the next shuffled card

        if(currentShuffledCard == playingCardList.Count)
        {//reshuffle the deck once the end is reached
            deckShuffled = false;
        }

        return shuffledCard;
    }

    //Question card logic
    public void addNewQuestionCards(string t_cardType, int t_value, string t_question, string t_answer, string t_wrongAnswer, string t_wrongAnswer2)
    {
        QuestionCard tempCard = new QuestionCard();
        tempCard.Type = t_cardType;
        tempCard.Value = t_value;
        tempCard.Question = t_question;
        tempCard.Answer = t_answer;
        tempCard.FalseAnswer = t_wrongAnswer;
        tempCard.FalseAnswer2 = t_wrongAnswer2;

        //make sure the data is valid before adding it to the list
        if (t_cardType != null || t_question != null || t_answer != null || t_wrongAnswer != null || t_wrongAnswer2 != null)
        {
            questionCardList.Add(tempCard);
        }
    }

    public void generateBulkQuestionCards(List<string> t_cardType, List<int> t_value, List<string> t_question, List<string> t_answer, List<string> t_wrongAnswer, List<string> t_wrongAnswer2)
    {
        for (int index = 0; index < t_cardType.Count; index++)
        {
            addNewQuestionCards(t_cardType[index], t_value[index], t_question[index], t_answer[index], t_wrongAnswer[index], t_wrongAnswer2[index]);
        }
    }

    public QuestionCard getSpecifiedQuestionCard(string t_cardType, int t_number)
    {
        List<string> questions = new List<string>();
        List<string> correctAnswers = new List<string>();
        List<string> falseAnswer = new List<string>();
        List<string> falseAnswer2 = new List<string>();

        for (int index = 0; index < questionCardList.Count; index++)
        {
            if (questionCardList[index].Type == t_cardType)
            {
                questions.Add(questionCardList[index].Question);
                correctAnswers.Add(questionCardList[index].Answer);
                falseAnswer.Add(questionCardList[index].FalseAnswer);
                falseAnswer2.Add(questionCardList[index].FalseAnswer2);
            }
        }

        if (t_number > questions.Count || questions.Count == 0)
        {
            return defaultQuestionCard;
        }

        QuestionCard temp = new QuestionCard();
        temp.Type = t_cardType;
        temp.Value = t_number;
        temp.Question = questions[t_number];
        temp.Answer = correctAnswers[t_number];
        temp.FalseAnswer = falseAnswer[t_number];
        temp.FalseAnswer2 = falseAnswer2[t_number];

        return temp;
    }

    public QuestionCard getRandomQuestionCardOfType(string t_cardType)
    {
        List<string> questions = new List<string>();//store all of the requested types data
        List<string> correctAnswers = new List<string>();
        List<string> falseAnswer = new List<string>();
        List<string> falseAnswer2 = new List<string>();

        for (int index = 0; index < questionCardList.Count; index++)
        {
            if (questionCardList[index].Type == t_cardType)
            {
                questions.Add(questionCardList[index].Question);
                correctAnswers.Add(questionCardList[index].Answer);
                falseAnswer.Add(questionCardList[index].FalseAnswer);
                falseAnswer2.Add(questionCardList[index].FalseAnswer2);
            }
        }

        if (questions.Count == 0)
        {
            return defaultQuestionCard;
        }

        int randomIndex = UnityEngine.Random.Range(0, questions.Count);

        QuestionCard temp = new QuestionCard();
        temp.Type = t_cardType;
        temp.Value = randomIndex;
        temp.Question = questions[randomIndex];
        temp.Answer = correctAnswers[randomIndex];
        temp.FalseAnswer = falseAnswer[randomIndex];
        temp.FalseAnswer2 = falseAnswer2[randomIndex];

        return temp;
    }

    public QuestionCard getRandomQuestionCard()
    {
        if (questionCardList.Count == 0)
        {
            return defaultQuestionCard;
        }
        int randomCard = UnityEngine.Random.Range(0, questionCardList.Count);
        return questionCardList[randomCard];
    }

    //Playing card logic
    public void addNewPlayingCards(string t_cardType, int t_value)
    {
        PlayingCard tempCard = new PlayingCard();
        tempCard.Type = t_cardType;
        tempCard.Value = t_value;

        //make sure the data is valid before adding it to the list
        if (t_cardType != null)
        {
            playingCardList.Add(tempCard);
        }
    }

    public void generateBulkPlayingCards(List<string> t_cardType, List<int> t_value)
    {
        for (int index = 0; index < t_cardType.Count; index++)
        {
            addNewPlayingCards(t_cardType[index], t_value[index]);
        }
    }

    public PlayingCard getSpecifiedPlayingCard(string t_cardType, int t_number)
    {
        for (int index = 0; index < playingCardList.Count; index++)
        {
            if (playingCardList[index].Type == t_cardType & playingCardList[index].Value == t_number)
            {
                return playingCardList[index];
            }
        }

        return defaultPlayingCard;
    }

    public PlayingCard getRandomPlayingCardOfType(string t_cardType)
    {
        List<int> selectedTypePositions = new List<int>();

        for (int index = 0; index < playingCardList.Count; index++)
        {
            if (playingCardList[index].Type == t_cardType)
            {
                selectedTypePositions.Add(index);
            }
        }

        if(selectedTypePositions.Count == 0)
        {
            return defaultPlayingCard;
        }

        int randomIndex = UnityEngine.Random.Range(0, selectedTypePositions.Count);

        return playingCardList[selectedTypePositions[randomIndex]];
    }

    public PlayingCard getRandomPlayingCard()
    {
        if (playingCardList.Count == 0)
        {
            return defaultPlayingCard;
        }
        int randomCard = UnityEngine.Random.Range(0, playingCardList.Count);
        return playingCardList[randomCard];
    }
}
