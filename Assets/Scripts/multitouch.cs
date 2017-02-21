using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multitouch : MonoBehaviour {
    public GameObject prefab2;
    public int vel;
    GameObject[] objetos = new GameObject[15];
    private int[] ids = new int[4];
    private Vector3[] startPosition = new Vector3[4];

    bool flagLeftTop, flagRightTop, flagLeftBot, flagRightBot;
    float sizeWidth, sizeHeight;
    // Use this for initialization
    void Start () {
        flagLeftTop = false; //id0
        flagRightTop = false; //id1
        flagLeftBot = false; //id2
        flagRightBot = false; //id3
        ids[0] = 20;
        ids[1] = 20;
        ids[2] = 20;
        ids[3] = 20;

        Ray rightTop;
        rightTop = Camera.main.ScreenPointToRay(new Vector2(Screen.width, Screen.height));

        Debug.Log(rightTop.origin.x);
        Debug.Log(rightTop.origin.y);
        sizeWidth = (rightTop.origin.x / 4);
        sizeHeight = (rightTop.origin.y / 4);

    }
    void Update()
    {
        Touch[] myTouches = Input.touches;

        for (int i = 0; i < Input.touchCount; i++)
        {
            //Do something with the touches
            if (myTouches[i].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(myTouches[i].position);
                Debug.Log("Begen");
                //right
                if (ray.origin.x > (3*sizeWidth))
                {
                    Debug.Log("x>0");

                    if (ray.origin.y > (3*sizeHeight))//top
                    {
                        if (!flagRightTop)
                        {
                            objetos[myTouches[i].fingerId] = Instantiate(prefab2, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
                            objetos[myTouches[i].fingerId].SetActive(false);
                            ids[0] = myTouches[i].fingerId;
                            flagRightTop = true;
                            startPosition[0] = new Vector3(ray.origin.x, ray.origin.y, 0);
                        }
                    }
                    else if (ray.origin.y < -(3 * sizeHeight))//bot
                    {
                        if (!flagRightBot)
                        {
                            objetos[myTouches[i].fingerId] = Instantiate(prefab2, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
                            objetos[myTouches[i].fingerId].SetActive(false);
                            flagRightBot = true;
                            ids[1] = myTouches[i].fingerId;
                            startPosition[1] = new Vector3(ray.origin.x, ray.origin.y, 0);

                        }
                    }
                }else if(ray.origin.x < -(3 * sizeWidth))//left
                {
                    Debug.Log("x<0");

                    if (ray.origin.y > (3 * sizeHeight))//top
                    {
                        if (!flagLeftTop)
                        {

                            objetos[myTouches[i].fingerId] = Instantiate(prefab2, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
                            ids[2] = myTouches[i].fingerId;
                            objetos[myTouches[i].fingerId].SetActive(false);
                            flagLeftTop = true;
                            startPosition[2] = new Vector3(ray.origin.x, ray.origin.y, 0);

                        }
                    }
                    else if (ray.origin.y < -(3 * sizeHeight))//bot
                    {
                        if (!flagLeftBot)
                        {
                            ids[3] = myTouches[i].fingerId;
                            objetos[myTouches[i].fingerId] = Instantiate(prefab2, new Vector3(ray.origin.x, ray.origin.y, 0), Quaternion.identity);
                            objetos[myTouches[i].fingerId].SetActive(false);
                            flagLeftBot = true;
                            startPosition[3] = new Vector3(ray.origin.x, ray.origin.y, 0);
                        }
                    }
                }
                
            }
            if (myTouches[i].phase == TouchPhase.Moved)
            {
                if (objetos[myTouches[i].fingerId])
                {
                    Ray ray = Camera.main.ScreenPointToRay(myTouches[i].position);
                    if (ids[0] == myTouches[i].fingerId)
                    {
                        if(ray.origin.x < (3 * sizeWidth) || ray.origin.y < (3 * sizeWidth))
                        {
                            flagRightTop = false;
                            objetos[myTouches[i].fingerId].SetActive(true);
                            ids[0] = 20;
                            Vector3 lu = (new Vector3(ray.origin.x, ray.origin.y, 0) - startPosition[0]).normalized * vel;
                            objetos[myTouches[i].fingerId].GetComponent<Rigidbody>().AddForce(lu, ForceMode.Impulse);
                            float angulo = (float)(System.Math.Atan2(lu.y, lu.x));
                            angulo = angulo * 180.0f / 3.1416f + 90;
                            objetos[myTouches[i].fingerId].transform.rotation = Quaternion.AngleAxis(angulo, new Vector3(0, 0, 1));
                        }
                    }
                    if (ids[1] == myTouches[i].fingerId)
                    {
                        if (ray.origin.x < (3 * sizeWidth) || ray.origin.y > -(3 * sizeWidth))
                        {
                            objetos[myTouches[i].fingerId].SetActive(true);
                            flagRightBot = false;
                            ids[1] = 20;
                            Vector3 lu = (new Vector3(ray.origin.x, ray.origin.y, 0) - startPosition[1]).normalized * vel;
                            objetos[myTouches[i].fingerId].GetComponent<Rigidbody>().AddForce(lu, ForceMode.Impulse);
                            float angulo = (float)(System.Math.Atan2(lu.y, lu.x));
                            angulo = angulo * 180.0f / 3.1416f + 90;
                            objetos[myTouches[i].fingerId].transform.rotation = Quaternion.AngleAxis(angulo, new Vector3(0, 0, 1));
                        }

                    }
                    if (ids[2] == myTouches[i].fingerId)
                    {
                        if (ray.origin.x > -(3 * sizeWidth) || ray.origin.y < (3 * sizeWidth))
                        {
                            objetos[myTouches[i].fingerId].SetActive(true);
                            flagLeftTop = false;
                            ids[2] = 20;
                            Vector3 lu = (new Vector3(ray.origin.x, ray.origin.y, 0) - startPosition[2]).normalized * vel;
                            objetos[myTouches[i].fingerId].GetComponent<Rigidbody>().AddForce(lu, ForceMode.Impulse);
                            float angulo = (float)(System.Math.Atan2(lu.y, lu.x));
                            angulo = angulo * 180.0f / 3.1416f + 90;
                            objetos[myTouches[i].fingerId].transform.rotation = Quaternion.AngleAxis(angulo, new Vector3(0, 0, 1));
                        }
                            

                    }
                    if (ids[3] == myTouches[i].fingerId)
                    {
                        if (ray.origin.x > -(3 * sizeWidth) || ray.origin.y > -(3 * sizeWidth))
                        {
                            objetos[myTouches[i].fingerId].SetActive(true);
                            flagLeftBot = false;
                            ids[3] = 20;
                            Vector3 lu = (new Vector3(ray.origin.x, ray.origin.y, 0) - startPosition[3]).normalized * vel;
                            objetos[myTouches[i].fingerId].GetComponent<Rigidbody>().AddForce(lu, ForceMode.Impulse);
                            float angulo = (float)(System.Math.Atan2(lu.y, lu.x));
                            angulo = angulo * 180.0f / 3.1416f + 90;
                            objetos[myTouches[i].fingerId].transform.rotation = Quaternion.AngleAxis(angulo, new Vector3(0,0,1));  
                        }

                    }
                }
            }
        }
    }
}
