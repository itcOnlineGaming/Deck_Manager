using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGenerator : MonoBehaviour
{
    public struct Card
    {
        public string Type;//face card subject type etc
        public int Value;//card num or just a number to easily find
        
        public string Question;//the question associated with this card
        public string Answer;//the correct answer

        public string FalseAnswer;//false answer
        public string FalseAnswer1;//extra false answers if needed
        public string FalseAnswer2;
        public string FalseAnswer3;
        public string FalseAnswer4;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addNewCards(string t_type)
    {

    }
}
