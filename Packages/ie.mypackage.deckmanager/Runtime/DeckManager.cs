using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> cardType1;
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addNewCards(string t_type, int t_value,string t_question,string t_answer,string t_falseAnswer, string t_falseAnswer2)
    {
        Card tempCard = new Card();
        tempCard.Type = t_type;
        tempCard.Value = t_value;
        tempCard.Question = t_question;
        tempCard.Answer = t_answer;
        tempCard.FalseAnswer = t_answer;
        tempCard.FalseAnswer2 = t_falseAnswer2;

        cardType1[type1Number] = tempCard;
        type1Number++;
    }

    public Card getSpecifiedCard(string t_type,int t_number)
    { 
        return cardType1[t_number];
    }
}
