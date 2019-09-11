using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// Adding OnTouch3D here forces us to implement the 
// OnTouch function, but also allows us to reference this
// object through the OnTouch3D class.
public class ARButton1 : MonoBehaviour, OnTouch3D
{
    public GameObject x;
    public GameObject o;
    private GameManager gm;
    private GameObject symbol;
    int buttonNum;

    void Start()
    {
        gm = transform.parent.gameObject.GetComponent<GameManager>();
        buttonNum = int.Parse(name.Substring(name.Length - 1, 1));
    }

    void Update()
    {
        
    }

    public void OnTouch()
    {
       Debug.Log("this button was touched " + name);
       if (gm.playerTurn && gm.statusButton(buttonNum) == 0)
       {
            placeObject(1);
            gm.playerButton(buttonNum);
       }
    }

    public void placeObject(int player)
    {
        if (player == 1)
        {
            symbol = Instantiate(x, transform);

        } else
        {
            symbol = Instantiate(o, transform);
        }
        symbol.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        symbol.transform.position = symbol.transform.position + new Vector3(-0.295f, 0.025f, 0.133f);
        symbol.SetActive(true);
    }

    public void deleteSymbol()
    {
        Debug.Log("deactivated symbol for me: " + buttonNum);
        symbol.SetActive(false);
    }
}