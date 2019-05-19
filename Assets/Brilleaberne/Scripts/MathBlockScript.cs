using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class MathBlockScript : MonoBehaviour
{

    public String atom;
    public int atomCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    // Substring splits a string into substrings. It takes 2 numbers which is the start- and end index of the substring
    // Example: String test = Hello
    // test.Substring(0,4) = Hell

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "MathBlockPlus")
        {
            // First attempt at splitting the string, we ended up just having another string and integer passed over from the SpawnCanvas script
            /*
            string holder = gameObject.GetComponent<Text>().text;
            string subHolder = holder.Substring(0,1);

            int valueOfHolder = Int32.Parse(holder.Substring(2, holder.Length-1)); 
                        
            valueOfHolder++;

           
            Debug.Log("subHolder is now: " + subHolder);
            Debug.Log("Value of substring: " + valueOfHolder);
            Debug.Log("the holder string is:" + holder.Substring(1,1));
            */

            atomCount++;
            gameObject.GetComponent<Text>().text = atom + atomCount;
       
        }

       else if (collision.gameObject.tag == "MathBlockMinus")
       {
            /*
            string holder = gameObject.GetComponent<Text>().text;
            int valueOfHolder = Int32.Parse(holder.Substring(1, holder.Length - 1));

            valueOfHolder++;

            */

            if(atomCount > 0) { 
            atomCount--;
            gameObject.GetComponent<Text>().text = atom + atomCount;

            }
        }
    }
}
