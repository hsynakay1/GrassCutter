using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermoving : MonoBehaviour
{
    public MeshRenderer mat;

    public float speedX;

    public float speedY;

    float curX;

    float curY;




    void Start()
    {
        curX = mat.material.mainTextureOffset.x;

        curY = mat.material.mainTextureOffset.y;

    }



    void Update()
    {
        curX += Time.deltaTime * speedX;

        curY += Time.deltaTime * speedY;

        mat.material.SetTextureOffset("_MainTex", new Vector2(curX, curY));

    }

}
