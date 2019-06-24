using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class MathBlockScript : MonoBehaviour
{

    public String atom;
    public int atomCount;
    public GameObject targetMolecule;
    public GameObject[] prefabs;
    public GameObject root;
    GameObject objRemove;

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
            GameObject beamObj;
            GameObject atomObj;

            if (atom == "H")
            {
                beamObj = Instantiate(prefabs[0], new Vector3(targetMolecule.transform.position.x-1, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                atomObj = Instantiate(prefabs[1], new Vector3(targetMolecule.transform.position.x-2, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                //beamObj.transform.parent.GetComponent<VRTK.VRTK_SnapDropZone>().currentSnappedObject = atomObj.GetComponent<VRTK.VRTK_InteractableObject>();
                //Debug.Log("snapCurr is equals too: " + snapCurr.GetComponent<Node>().molecule);
            }
            else if (atom == "O")
            {
                beamObj = Instantiate(prefabs[0], new Vector3(targetMolecule.transform.position.x-1, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                atomObj = Instantiate(prefabs[2], new Vector3(targetMolecule.transform.position.x-2, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                //beamObj.transform.GetChild(0).GetComponent<VRTK.VRTK_SnapDropZone>().currentSnappedObject = atomObj.GetComponent<VRTK.VRTK_InteractableObject>();
                //Debug.Log("snapCurr is equals too: " + snapCurr.GetComponent<Node>().molecule);
            }

        }

       else if (collision.gameObject.tag == "MathBlockMinus")
       {
            /*
            string holder = gameObject.GetComponent<Text>().text;
            int valueOfHolder = Int32.Parse(holder.Substring(1, holder.Length - 1));

            valueOfHolder++;

            */

            if(atomCount > 0)
            {
                atomCount--;
                gameObject.GetComponent<Text>().text = atom + atomCount;
                findChildAtom(root);
            }
       }
    }

    private void findChildAtom(GameObject parentObj)
    {
        var children = new List<GameObject>();

        foreach (Transform childTransform in parentObj.transform)
        {
            var childGameObject = childTransform.gameObject;
            children.Add(childGameObject);
		}

	}
}
