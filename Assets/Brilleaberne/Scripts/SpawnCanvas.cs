using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCanvas : MonoBehaviour
{
    public GameObject prefabMolMenu;
    public GameObject prefabMolText;
    public GameObject laser;
    public Text CanvasMoleculeText;
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

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit)) {
            if (hit.collider)
            {
                hitObj = hit.collider.gameObject;
                line.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else {
                line.SetPosition(1, new Vector3(0, 0, 1000));
            }
        }
    }

    public void SpawnCanvasFunction()
    {
        if (hitObj.GetComponent<Node>()) {
            H = 0;
            O = 0;

            //int letterCounter = 2;

            LinkedList<string> MoleculeList = hitObj.GetComponent<NodeList>().GetMoleculeList();
            Debug.Log("Length of moleculeList " + MoleculeList.Count);
            while (MoleculeList.Count > 0) {
                if (MoleculeList.Contains("H"))
                {
                    H++;
                    MoleculeList.Remove("H");
                }
                else if (MoleculeList.Contains("O"))
                {
                    O++;
                    MoleculeList.Remove("O");
                }
            }
            Debug.Log("H" + H + " O" + O);
            //CanvasMoleculeText.text = "Molecule:" + '\n' + "H" + H + " O" + O;

            //
            GameObject moleculeMenu = Instantiate(prefabMolMenu, new Vector3(hitObj.transform.position.x + 0.5f, hitObj.transform.position.y, hitObj.transform.position.z), Quaternion.identity);
            ArrayList letterCounter = new ArrayList();
            letterCounter.Add("H");
            letterCounter.Add("O");

            if(H > 0)
            { 
                GameObject moleculeText = Instantiate(prefabMolText, new Vector3(moleculeMenu.transform.position.x, moleculeMenu.transform.position.y, moleculeMenu.transform.position.z), Quaternion.identity);
                moleculeText.GetComponent<Text>().text = "H" + H;
                moleculeText.transform.SetParent(moleculeMenu.transform);
            }
            if (O > 0)
            {
                GameObject moleculeText = Instantiate(prefabMolText, new Vector3(moleculeMenu.transform.position.x + 1, moleculeMenu.transform.position.y, moleculeMenu.transform.position.z), Quaternion.identity);
                moleculeText.GetComponent<Text>().text = "O" + O;
                moleculeText.transform.SetParent(moleculeMenu.transform);
            }



            //moleculeMenu.GetComponentInChildren<Text>().text = "Molecule:" + '\n' + "H" + H + " O" + O;
        }
    }
}
