using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debbugger : MonoBehaviour {

    public List<Item> Items;

	// Use this for initialization
	void Start () {
        foreach (var item in Items)
        {
            Instantiate(item.Graphics, transform.position, transform.rotation, null);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
