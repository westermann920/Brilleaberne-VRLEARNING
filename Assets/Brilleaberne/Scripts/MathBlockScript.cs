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
    GameObject objFreeSnap;

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

                findFreeZone(beamObj.GetComponent<NodeList>().snapZoneList, atomObj);
                findFreeAtom(beamObj);

                //beamObj.transform.parent.GetComponent<VRTK.VRTK_SnapDropZone>().currentSnappedObject = atomObj.GetComponent<VRTK.VRTK_InteractableObject>();
                //Debug.Log("snapCurr is equals too: " + snapCurr.GetComponent<Node>().molecule);
            }
            else if (atom == "O")
            {
                beamObj = Instantiate(prefabs[0], new Vector3(targetMolecule.transform.position.x-1, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                atomObj = Instantiate(prefabs[2], new Vector3(targetMolecule.transform.position.x-2, targetMolecule.transform.position.y, targetMolecule.transform.position.z), Quaternion.identity);
                //beamObj.transform.GetChild(0).GetComponent<VRTK.VRTK_SnapDropZone>().currentSnappedObject = atomObj.GetComponent<VRTK.VRTK_InteractableObject>();
                //Debug.Log("snapCurr is equals too: " + snapCurr.GetComponent<Node>().molecule);

            
                findFreeZone(beamObj.GetComponent<NodeList>().snapZoneList, atomObj);
                findFreeAtom(beamObj);

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
                findAtomToDelete(atom);
            }
       }
    }

    private void findAtomToDelete(String atom)
    {
        GameObject delAtomObj = new GameObject();
        GameObject delConnectorObj = new GameObject();

        Queue<Transform> children = new Queue<Transform>();
        children.Enqueue(root.transform);

        while (children.Count > 0)
        {
            Transform parentTransform = children.Dequeue();

            foreach (Transform child1Transform in parentTransform)
            {
                foreach (Transform child2Transform in child1Transform)
                {
                    if (child2Transform.gameObject.tag == "Atom")
                    {
                        children.Enqueue(child2Transform);
                        if (child2Transform.gameObject.GetComponent<Node>().molecule == atom)
                        {
                            delAtomObj = child2Transform.gameObject;
                        }
                    }
                    else if (child2Transform.gameObject.tag == "Connector")
                    {
                        children.Enqueue(child2Transform);
                    }
                }
		    }
        }


        if (delAtomObj.transform.parent.parent != null)
        {
            delConnectorObj = delAtomObj.transform.parent.parent.gameObject;
        }
        Destroy(delAtomObj);
        Destroy(delConnectorObj);
    }

    void findFreeAtom(GameObject obj)
    {
        Queue<Transform> list = new Queue<Transform>();
        list.Enqueue(root.transform);
        
        while (list.Count > 0)
        {
            Transform parentTransform = list.Dequeue();

            if (parentTransform.gameObject.tag == "Atom")
            {
                if (checkFreeZone(parentTransform.gameObject))
                {
                    Debug.Log("Check freezone = true");
                    objFreeSnap.GetComponent<VRTK.VRTK_SnapDropZone>().ForceSnap(obj);
                    return;
                }
            }

            foreach (Transform child1Transform in list)
            {
                foreach (Transform child2Transform in child1Transform)
                {
                    if (child2Transform.gameObject.tag == "Atom" || child2Transform.gameObject.tag == "Connector")
                    {
                        list.Enqueue(child2Transform);
                    }
                
                }
            }
        }
    }

    bool checkFreeZone(GameObject parent)
    {
        bool result = false;
        int used = 0;

        for (int i = 0; i < parent.GetComponent<NodeList>().snapZoneList.Length-1; i++)
        {
            if (parent.GetComponent<NodeList>().snapZoneList[i].gameObject.transform.childCount <= 1)
            {
                objFreeSnap = parent.GetComponent<NodeList>().snapZoneList[i].gameObject;
                result = true;
            }
            else
            {
                used++;
            }
        }

        return result;
    }

    void findFreeZone(GameObject[] zoneList, GameObject objToZnap)
    {
        for (int i = 0; i <= zoneList.Length-1; i++)
        {
            if (zoneList[i].transform.childCount <= 1)
            {
                zoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().ForceSnap(objToZnap);
                return;
            }
        }
    }
}
