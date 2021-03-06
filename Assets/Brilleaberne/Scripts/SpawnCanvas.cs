﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCanvas : MonoBehaviour
{
    public GameObject prefabMolMenu;
    public GameObject prefabMolText;
    public GameObject prefabMathBlock;
    public GameObject laser;
    public GameObject root;
   
    GameObject moleculeMenu;
    GameObject hitObj;
    LineRenderer line;
    int H, O;

    // Start is called before the first frame update
    void Start()
    {
        line = laser.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.collider)
            {
                hitObj = hit.collider.gameObject;
                line.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else
            {
                line.SetPosition(1, new Vector3(0, 0, 1000));
            }
        }
    }

    public void SpawnCanvasFunction()
    {
        if (hitObj.GetComponent<Node>())
        {
            H = 0;
            O = 0;

            //int letterCounter = 2;

            LinkedList<string> MoleculeList = hitObj.GetComponent<NodeList>().GetMoleculeList();
            LinkedList<string> letterCounter = new LinkedList<string>();
            //Debug.Log("Length of moleculeList " + MoleculeList.Count);

            //GameObject moleculeMenu = Instantiate(prefabMolMenu, new Vector3(hitObj.transform.position.x + 0.5f, hitObj.transform.position.y, hitObj.transform.position.z), Quaternion.identity);

            /*
            foreach (string str in MoleculeList)
            {
                if (!letterCounter.Contains(str))
                {

                    letterCounter.AddLast(str);
                   
                }
            }
            */

            foreach (string mol in MoleculeList)
            {
                if (mol == "H")
                {
                    H++;
                }
                else if (mol == "O")
                {
                    O++;
                }
            }
            
            Debug.Log("H" + H + " O" + O);

            moleculeMenu = Instantiate(prefabMolMenu, new Vector3(hitObj.transform.position.x + 0.5f, hitObj.transform.position.y, hitObj.transform.position.z), Quaternion.identity);

            float x = 0;

            if(H > 0)
            { 
                GameObject moleculeText = Instantiate(prefabMolText, new Vector3(moleculeMenu.transform.position.x+x, moleculeMenu.transform.position.y, moleculeMenu.transform.position.z), Quaternion.identity);

                moleculeText.GetComponent<MathBlockScript>().atom = "H";
                moleculeText.GetComponent<MathBlockScript>().atomCount = H;
                moleculeText.GetComponent<MathBlockScript>().targetMolecule = hitObj;
                moleculeText.GetComponent<MathBlockScript>().root = root;

                moleculeText.GetComponent<Text>().text ="H" + H;
                moleculeText.transform.SetParent(moleculeMenu.transform);

                x = x + 0.5f;
            }
            if (O > 0)
            {
                GameObject moleculeText = Instantiate(prefabMolText, new Vector3(moleculeMenu.transform.position.x+x, moleculeMenu.transform.position.y, moleculeMenu.transform.position.z), Quaternion.identity);
                moleculeText.GetComponent<MathBlockScript>().atom = "O";
                moleculeText.GetComponent<MathBlockScript>().atomCount = O;
                moleculeText.GetComponent<MathBlockScript>().targetMolecule = hitObj;
                moleculeText.GetComponent<MathBlockScript>().root = root;

                moleculeText.GetComponent<Text>().text = "O" + O;
                moleculeText.transform.SetParent(moleculeMenu.transform);

                x = x + 0.5f;
            }
           
            //moleculeMenu.GetComponentInChildren<Text>().text = "Molecule:" + '\n' + "H" + H + " O" + O;
        }

        Debug.Log(hitObj.tag);

        if (hitObj.tag == "MoleCuleTextBoard")
        {
            moleculeMenu = hitObj.transform.parent.gameObject;

            GameObject moleculeText1 = Instantiate(prefabMathBlock, new Vector3(hitObj.transform.position.x - 0.2f, hitObj.transform.position.y - 0.5f, hitObj.transform.position.z), Quaternion.identity);
            GameObject moleculeText2 = Instantiate(prefabMathBlock, new Vector3(hitObj.transform.position.x + 0.2f, hitObj.transform.position.y - 0.5f, hitObj.transform.position.z), Quaternion.identity);

            moleculeText1.GetComponent<Text>().text = "+1";
            moleculeText2.GetComponent<Text>().text = "-1";

            moleculeText1.transform.SetParent(moleculeMenu.transform);
            moleculeText1.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            moleculeText1.tag = "MathBlockPlus";

            moleculeText2.transform.SetParent(moleculeMenu.transform);
            moleculeText2.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            moleculeText2.tag = "MathBlockMinus";
        }
    }
}
