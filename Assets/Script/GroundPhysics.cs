using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundPhysics : MonoBehaviour
{
    //Sostituirlo con Scriptable
    public GameObject downTile;
    //Sostituirlo con Scriptable
    public GameObject upTile;
    public bool isVoid;
    public GameObject voidGO;
    private Transform tr;
    private SpriteRenderer sr;
    private bool createdNewTiles;
    private float distFromEdge;
    private bool isOnCamera;

    void CreateDownTile()
    {
        var goClone = Instantiate(downTile, new Vector3(this.GetComponent<Transform>().position.x + StaticProperty.screenSize.x * 2, downTile.GetComponent<Transform>().position.y, 10), Quaternion.identity);
        if (StaticProperty.isVisible)
            goClone.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);
        else
            goClone.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);

        if (StaticProperty.thereIsATileDown != null)
        {
            goClone.GetComponent<BoxCollider2D>().size = new Vector2(goClone.GetComponent<BoxCollider2D>().size.x + StaticProperty.thereIsATileDown.gameObject.GetComponent<BoxCollider2D>().size.x, goClone.GetComponent<BoxCollider2D>().size.y);
            goClone.GetComponent<BoxCollider2D>().offset = new Vector2(-(Vector2.Distance(goClone.GetComponent<Transform>().position, StaticProperty.thereIsATileDown.gameObject.GetComponent<Transform>().position) / (2 * goClone.GetComponent<Transform>().localScale.x / StaticProperty.sideTiles)), goClone.GetComponent<BoxCollider2D>().offset.y);

            StaticProperty.thereIsATileDown.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StaticProperty.sideTiles++;
        }
        else
        {
            StaticProperty.sideTiles = 1;
        }
    }

    void CreateUpTile()
    {
        var goClone = Instantiate(upTile, new Vector3(this.GetComponent<Transform>().position.x + StaticProperty.screenSize.x * 2, upTile.GetComponent<Transform>().position.y, -10), Quaternion.identity);
        if (StaticProperty.isVisible)
            goClone.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);
        else
            goClone.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);

        if (StaticProperty.thereIsATileUp != null)
        {
            goClone.GetComponent<BoxCollider2D>().size = new Vector2(goClone.GetComponent<BoxCollider2D>().size.x + StaticProperty.thereIsATileUp.gameObject.GetComponent<BoxCollider2D>().size.x, goClone.GetComponent<BoxCollider2D>().size.y);
            goClone.GetComponent<BoxCollider2D>().offset = new Vector2(-(Vector2.Distance(goClone.GetComponent<Transform>().position, StaticProperty.thereIsATileUp.gameObject.GetComponent<Transform>().position) / (2 * goClone.GetComponent<Transform>().localScale.x / StaticProperty.sideTiles)), goClone.GetComponent<BoxCollider2D>().offset.y);

            StaticProperty.thereIsATileUp.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StaticProperty.sideTiles++;
        }
        else
        {
            StaticProperty.sideTiles = 1;
        }
    }

    void CreateVoidTile()
    {
        var goClone = Instantiate(voidGO, new Vector3(this.GetComponent<Transform>().position.x + StaticProperty.screenSize.x * 2, 0, 0), Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        //goPrincipal = GetComponent<GameObject>();
        tr = this.GetComponent<Transform>();
        sr = this.GetComponent<SpriteRenderer>();
        createdNewTiles = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (StaticProperty.isVisible)
                if (tr.position.y < 0)
                    this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);
                else
                    this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);

            else if (!StaticProperty.isVisible)
                if (tr.position.y > 0)
                    this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);
                else
                    this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);
        }

        distFromEdge = StaticProperty.camerasLeftEdgePos - tr.position.x + (sr.bounds.size.x / 2);
        isOnCamera = StaticProperty.camerasLeftEdgePos - tr.position.x - (sr.bounds.size.x / 2) < 0;

        if (isVoid && distFromEdge > 0 && !createdNewTiles)
        {
            int i = Random.Range(0, 11);
            if (i >= 0 && i <= 4)
            {
                CreateDownTile();
            }
            else if (i >= 5 && i <= 9)
            {
                CreateUpTile();
            }

            CreateVoidTile();

            createdNewTiles = true;
            //EditorApplication.isPaused = true;
        }

        if (!isOnCamera)
        {
            Destroy(gameObject);
        }
    }
}

