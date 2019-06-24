using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// AUTO INDENT IS : CTRL + K and then CTRL + F (YES BOTH ARE NEEDED, IN THAT ORDER)

public class NodeList : MonoBehaviour
{

    public GameObject[] snapZoneList;

    public LinkedList<string> MoleculeList = new LinkedList<string>();

    // This will need to be revisited depending on the Unity rules. If the first prefab is the root, then the object on the piedestal will be the root at first and that wont do.
    // What needs to happen is that first element with a connector beam attached becomes the root node. It may be that we should call this script from the controller and save the list somewhere else
    // We may need to create several lists, properbly not since it should be possible to have different lists on different objects. 
    // The Molecule List will be empty at first for each object so an else statement cant occur. What we need to do is to check when the first beam connects wether or not there are other 
    // GameObjects connected at the end of the beam, and if that is true, join into their list. 


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // I need to check if there is a gameObject connected to the snapDropZone
        // For that i need to know if the variable int SnapDropZone "snappedObject" is null or not. If it is null, nothing needs to happen. But if its true then i need to retrieve that object
        // And check its Node script for its type. Add that element to the list and then perfom the check on that Node aswell. 

        // This call should retrieve the object in the first snapZone of the given "Node". (Node = molecule = represented like balls)

    }

    void setObjectReference(GameObject ObjE, LinkedList<string> list, GameObject prevObj, int ID)
    {
        // This is a reference to the beam connected to the Node (Atom)

        if (ObjE.GetComponent<Node>())
        {
            Debug.Log("Checked object: " + ID + " has the node script");
            list.AddLast(ObjE.GetComponent<Node>().molecule);
        }

        if (ObjE.GetComponent<NodeList>())
        {

            Debug.Log("Checked object: " + ID + " has the nodeList script");

            GameObject[] listTest = ObjE.GetComponent<NodeList>().GetSnapZoneList();

            for (int i = 0; i < listTest.Length; i++)
            {

                Debug.Log("For loop running on object: " + ID + " Checking zone: " + i + " of " + listTest.Length);

                if (listTest[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedObject() &&
                    listTest[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedObject() != prevObj)
                {
                    Debug.Log("Object: " + ID + " Zone: " + i + " holds an object");
                    setObjectReference(listTest[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedObject(), list, ObjE, ID++);
                }
                else
                {
                    Debug.Log("Object: " + ID + " Zone: " + i + " does NOT hold an object");
                }
            }
        }
    }

    public LinkedList<string> GetMoleculeList()
    {
        GameObject root = transform.root.gameObject;
        GameObject.FindGameObjectWithTag("RightController").GetComponent<SpawnCanvas>().root = root;
        GameObject.FindGameObjectWithTag("LeftController").GetComponent<SpawnCanvas>().root = root;

        int ID = 0;

        if (root.GetComponent<Node>())
        {
            MoleculeList.AddFirst(root.GetComponent<Node>().molecule);
            Debug.Log("Adding node for object with ID " + ID + " as first molecule in the list");
        }

        GameObject[] rootList = root.GetComponent<NodeList>().GetSnapZoneList();

        for (int i = 0; i < rootList.Length; i++)
        {
            Debug.Log("Running for loop to check for objects. Checking object with ID: " + ID + " snapZone: " + i + " of " + rootList.Length);

            if (rootList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedObject() != null)
            {

                Debug.Log("Object with ID: " + ID + " Holds an object on snapZone: " + i);

                setObjectReference(rootList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedObject(), MoleculeList, root, ID++);
            }
            else
            {
                Debug.Log("Object with ID: " + ID + " Does NOT hold an object on snapZone: " + i);
            }
        }

        return MoleculeList;
    }

    GameObject[] GetSnapZoneList()
    {
        GameObject[] list = this.snapZoneList;
        return list;
    }
}
