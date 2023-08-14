using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public GameObject otherGO;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private bool isPlayerDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        cc = this.GetComponent<CapsuleCollider2D>();

        isPlayerDown = this.GetComponent<Transform>().position.y < 0;

        Physics2D.IgnoreCollision(cc, otherGO.GetComponent<CapsuleCollider2D>());
        if (isPlayerDown)
        {
            Physics2D.IgnoreLayerCollision(10, 9);
            rb.gravityScale = StaticProperty.defaultGravity;
        }
        else
        {
            Physics2D.IgnoreLayerCollision(11, 8);
            rb.gravityScale = -StaticProperty.defaultGravity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(StaticProperty.speed.x, rb.velocity.y);

        //Player Down
        if (StaticProperty.isVisible && isPlayerDown)
        {
            if (rb.velocity.y <= 0)
                rb.gravityScale = StaticProperty.fallingGravity;

            var bottomEdgeBC = cc.bounds.center.y - cc.bounds.extents.y;
            var bcTile = GameObject.FindGameObjectWithTag("GroundDown");
            if (bcTile != null)
            {
                var topEdgeBCTile = bcTile.GetComponent<BoxCollider2D>().bounds.center.y + bcTile.GetComponent<BoxCollider2D>().bounds.extents.y;
                if (bottomEdgeBC < topEdgeBCTile)
                {
                    if (!Physics2D.GetIgnoreLayerCollision(10, 8))
                        Physics2D.IgnoreLayerCollision(10, 8);
                }
                else
                {
                    if (Physics2D.GetIgnoreLayerCollision(10, 8))
                        Physics2D.IgnoreLayerCollision(10, 8, false);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                //this.GetComponent<Transform>().position = new Vector3(this.transform.position.x, -otherGO.GetComponent<Transform>().position.y, 10);
                otherGO.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);
                this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);


                if (StaticProperty.grounded)
                {
                    rb.gravityScale = StaticProperty.defaultGravity;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(0, StaticProperty.jumpForce), ForceMode2D.Impulse);
                    otherGO.GetComponent<Rigidbody2D>().gravityScale = -StaticProperty.defaultGravity;
                    otherGO.GetComponent<Rigidbody2D>().velocity = new Vector2(otherGO.GetComponent<Rigidbody2D>().velocity.x, 0);
                    otherGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -StaticProperty.jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    if (StaticProperty.canDoubleJump)
                    {
                        StaticProperty.canDoubleJump = false;

                        rb.gravityScale = StaticProperty.defaultGravity;
                        rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
                        rb.AddForce(new Vector2(0, StaticProperty.jumpForce), ForceMode2D.Impulse);
                        otherGO.GetComponent<Rigidbody2D>().gravityScale = -StaticProperty.defaultGravity;
                        otherGO.GetComponent<Rigidbody2D>().velocity = new Vector2(otherGO.GetComponent<Rigidbody2D>().velocity.x, 0);
                        otherGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -StaticProperty.jumpForce), ForceMode2D.Impulse);
                    }
                }
            }

            otherGO.GetComponent<Transform>().position = new Vector3(this.transform.position.x, -this.transform.position.y, -10);

            if (this.GetComponent<Transform>().position.y < -StaticProperty.screenSize.y)
            {
                //SceneManager.UnloadSceneAsync("Game");
                SceneManager.LoadScene("Menu");
            }
        }

        //Player Up
        if (!StaticProperty.isVisible && !isPlayerDown)
        {
            if (rb.velocity.y >= 0)
                rb.gravityScale = -StaticProperty.fallingGravity;

            var topEdgeCC = cc.bounds.center.y + cc.bounds.extents.y;
            var bcTile = GameObject.FindGameObjectWithTag("GroundUp");
            if (bcTile != null)
            {
                var bottomEdgeBCTile = bcTile.GetComponent<BoxCollider2D>().bounds.center.y - bcTile.GetComponent<BoxCollider2D>().bounds.extents.y;
                if (topEdgeCC > bottomEdgeBCTile)
                {
                    if (!Physics2D.GetIgnoreLayerCollision(11, 9))
                        Physics2D.IgnoreLayerCollision(11, 9);
                }
                else
                {
                    if (Physics2D.GetIgnoreLayerCollision(11, 9))
                        Physics2D.IgnoreLayerCollision(11, 9, false);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                //this.GetComponent<Transform>().position = new Vector3(this.transform.position.x, -otherGO.GetComponent<Transform>().position.y, -10);
                otherGO.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 0.5f);
                this.GetComponent<SpriteRenderer>().color = new Color(191, 191, 191, 1);

                if (StaticProperty.grounded)
                {
                    rb.gravityScale = -StaticProperty.defaultGravity;
                    rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
                    rb.AddForce(new Vector2(0, -StaticProperty.jumpForce), ForceMode2D.Impulse);
                    otherGO.GetComponent<Rigidbody2D>().gravityScale = StaticProperty.defaultGravity;
                    otherGO.GetComponent<Rigidbody2D>().velocity = new Vector2(otherGO.GetComponent<Rigidbody2D>().velocity.x, 0);
                    otherGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, StaticProperty.jumpForce), ForceMode2D.Impulse);
                }
                else
                {
                    if (StaticProperty.canDoubleJump)
                    {
                        StaticProperty.canDoubleJump = false;

                        rb.gravityScale = -StaticProperty.defaultGravity;
                        rb.velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
                        rb.AddForce(new Vector2(0, -StaticProperty.jumpForce), ForceMode2D.Impulse);
                        otherGO.GetComponent<Rigidbody2D>().gravityScale = StaticProperty.defaultGravity;
                        otherGO.GetComponent<Rigidbody2D>().velocity = new Vector2(otherGO.GetComponent<Rigidbody2D>().velocity.x, 0);
                        otherGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, StaticProperty.jumpForce), ForceMode2D.Impulse);
                    }
                }
            }

            otherGO.GetComponent<Transform>().position = new Vector3(this.transform.position.x, -this.transform.position.y, 10);

            if (!StaticProperty.isVisible && this.GetComponent<Transform>().position.y > StaticProperty.screenSize.y)
            {
                //SceneManager.UnloadSceneAsync("Game");
                SceneManager.LoadScene("Retry");
            }
        }
    }
}