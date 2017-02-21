using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour {
    public int hp = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
            Destroy(collision.gameObject);
        hp--;
        if (hp < 1)
        {
            Destroy(gameObject);
        }
    }
}
