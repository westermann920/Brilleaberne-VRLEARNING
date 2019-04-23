using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// AUTO INDENT IS : CTRL + K and then CTRL + F (YES BOTH ARE NEEDED, IN THAT ORDER)

/**
     
     
*/
public class NodeList : MonoBehaviour
{

    public GameObject[] snapZoneList;

    GameObject nextNode;
    GameObject beam1;
    GameObject beam2;


    GameObject root;
    GameObject snapped;

    public LinkedList<string> MoleculeList = new LinkedList<string>();

    public string childString;


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

    void setObjectReference(GameObject ObjE, LinkedList<string> list, GameObject prevObj) {
        // This is a reference to the beam connected to the Node (Atom)
        //beam1 = snapZoneList[E].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject;
        if (ObjE.GetComponent<Node>()) {
            list.AddLast(ObjE.GetComponent<Node>().molecule);
        }

        for (int i = 0; i < ObjE.GetComponents<NodeList>().Length; i++) {
            Debug.Log(i + " We inside the list");
            if (ObjE.GetComponent<NodeList>().snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject != null && 
                ObjE.GetComponent<NodeList>().snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject != prevObj) {
                setObjectReference(ObjE.GetComponent<NodeList>().snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject, list , ObjE);
            }
        }

        //nextNode = beam1.GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject;
    }

    public LinkedList<string> GetMoleculeList()
    {
       // string toBeAdded = this.GetComponent<Node>.mole


        //MoleculeList.AddFirst(this.GetComponent<Node>.);

        for (int i = 0; i < snapZoneList.Length; i++)
        {
            Debug.Log(i + " zone = " + snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject());
            if (snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject() != null) {
                Debug.Log("shit");
                setObjectReference(snapZoneList[i].GetComponent<VRTK.VRTK_SnapDropZone>().GetCurrentSnappedInteractableObject().gameObject, MoleculeList, this.gameObject);
            }
        }


        return MoleculeList;
    }
    
}
