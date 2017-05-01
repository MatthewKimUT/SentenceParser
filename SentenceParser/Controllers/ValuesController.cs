using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace SentenceParser.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        //GET api/values/sentence
        //note: using query string 'api/values?sentence=' is safer if the sentence contains a question mark 
        public string Get(string sentence)
        {   
            StringBuilder editedSentence = new StringBuilder();
            //splits this sentence, 123 like so:
            //{"splits ", "this ", "sentence,", " ", "1", "2", "3", " ", "like ", "so:", ""}
            //Regex lookbehind preserves non-alphabetic characters
            string[] words = Regex.Split(sentence, @"(?<=[^a-zA-Z])");
            foreach (string word in words)
            {
                int numChars = word.Length;
                //this check is required in case a non-alphabetic character is found at the end of the sentence
                //the lookbehind in the regex split will act on closing punctuation, appending an empty string to the 'words' array
                if(numChars != 0)
                {
                    bool isSingleChar = numChars == 1;
                    bool hasNonAlphabetic = Regex.IsMatch(word, @"[^a-zA-Z]");
                    //normally a word in the array will count an extra distinct character (the non-alphabetic delimiter)
                    //variable offset is required in case an alphabetic character is found at the end of the sentence
                    int offset = (hasNonAlphabetic) ? 1 : 0;
                    //numberAndFinalChar will be empty if element in word array is non-alphabetic single character
                    string numberAndFinalChar = (isSingleChar && hasNonAlphabetic) ? "" : (word.Distinct().Count() - offset).ToString() + word.Substring(word.Length - offset - 1);
                    editedSentence.Append(word.ElementAt(0) + numberAndFinalChar);
                }
            }
            return editedSentence.ToString();
        }
    }
}
