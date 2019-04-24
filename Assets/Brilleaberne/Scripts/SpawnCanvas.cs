using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCanvas : MonoBehaviour
{
    public GameObject prefab;
    public GameObject laser;
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

    public void SpawnCanvasFunction() {
        //Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        H = 0;
        O = 0;
        LinkedList<string> MoleculeList = hitObj.GetComponent<NodeList>().GetMoleculeList();
        Debug.Log(MoleculeList.Count);
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
        Debug.Log("H" + H + "O" + O);
    }
}
