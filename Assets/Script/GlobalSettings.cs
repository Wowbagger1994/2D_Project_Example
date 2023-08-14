using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    public Text scoreTextDown;
    public Text scoreTextUp;
    public Transform upGroundCheck;
    public Transform downGroundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGroundDown;
    public LayerMask whatIsGroundUp;
    public float defaultGravity;
    public float acceleration;
    public float jumpForce;
    public Vector2 speed;
    public GameObject grass1;
    public GameObject grass2;
    public GameObject grass3;
    public GameObject grass4;
    public GameObject grassPreFab;
    public GameObject snow1;
    public GameObject snow2;
    public GameObject snow3;
    public GameObject snow4;
    public GameObject snowPrefab;
    public GameObject voidPrefab;
    public Camera cameraDown;
    public Camera cameraUp;
    public GameObject CheckIfThereIsATileUp;
    public GameObject CheckIfThereIsATileDown;
    private static Vector2 _screenSize;

    private float CalculateScaleTiles()
    {
        var tileX = StaticProperty.screenSize.x / 2;
        return tileX / 1.28f;
    }

    // Start is called before the first frame update
    void Start()
    {
        _screenSize = new Vector2
        (
        Vector2.Distance(cameraDown.ScreenToWorldPoint(new Vector2(0, 0)), cameraDown.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f,
        Vector2.Distance(cameraDown.ScreenToWorldPoint(new Vector2(0, 0)), cameraDown.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f
        );

        StaticProperty.screenSize = _screenSize;
        StaticProperty.defaultGravity = defaultGravity;
        StaticProperty.fallingGravity = defaultGravity * 1.1f;
        StaticProperty.jumpForce = jumpForce;
        StaticProperty.isVisible = true;
        StaticProperty.speed.x = speed.x;
        StaticProperty.sideTiles = 3;

        grass1.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), grass1.GetComponent<Transform>().localScale.y);
        grass2.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), grass2.GetComponent<Transform>().localScale.y);
        grass3.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), grass3.GetComponent<Transform>().localScale.y);
        grass4.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), grass4.GetComponent<Transform>().localScale.y);
        grassPreFab.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), grassPreFab.GetComponent<Transform>().localScale.y);
        snow1.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), snow1.GetComponent<Transform>().localScale.y);
        snow2.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), snow2.GetComponent<Transform>().localScale.y);
        snow3.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), snow3.GetComponent<Transform>().localScale.y);
        snow4.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), snow4.GetComponent<Transform>().localScale.y);
        snowPrefab.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), snowPrefab.GetComponent<Transform>().localScale.y);
        voidPrefab.GetComponent<Transform>().localScale = new Vector2(CalculateScaleTiles(), voidPrefab.GetComponent<Transform>().localScale.y);

        grass1.GetComponent<Transform>().position = new Vector3(-StaticProperty.screenSize.x + (StaticProperty.screenSize.x / 4), grass1.GetComponent<Transform>().position.y, 10);
        grass2.GetComponent<Transform>().position = new Vector3(grass1.GetComponent<Transform>().position.x + grass1.GetComponent<SpriteRenderer>().bounds.size.x, grass2.GetComponent<Transform>().position.y, 10);
        grass3.GetComponent<Transform>().position = new Vector3(grass2.GetComponent<Transform>().position.x + grass2.GetComponent<SpriteRenderer>().bounds.size.x, grass3.GetComponent<Transform>().position.y, 10);
        grass4.GetComponent<Transform>().position = new Vector3(grass3.GetComponent<Transform>().position.x + grass3.GetComponent<SpriteRenderer>().bounds.size.x, grass4.GetComponent<Transform>().position.y, 10);
        snow1.GetComponent<Transform>().position = new Vector3(-StaticProperty.screenSize.x + (StaticProperty.screenSize.x / 4), snow1.GetComponent<Transform>().position.y, -10);
        snow2.GetComponent<Transform>().position = new Vector3(snow1.GetComponent<Transform>().position.x + snow1.GetComponent<SpriteRenderer>().bounds.size.x, snow2.GetComponent<Transform>().position.y, -10);
        snow3.GetComponent<Transform>().position = new Vector3(snow2.GetComponent<Transform>().position.x + snow2.GetComponent<SpriteRenderer>().bounds.size.x, snow3.GetComponent<Transform>().position.y, -10);
        snow4.GetComponent<Transform>().position = new Vector3(snow3.GetComponent<Transform>().position.x + snow3.GetComponent<SpriteRenderer>().bounds.size.x, snow4.GetComponent<Transform>().position.y, -10);

        CheckIfThereIsATileDown.GetComponent<Transform>().position = new Vector3(grass4.GetComponent<Transform>().position.x, grass4.GetComponent<Transform>().position.y, 10);
        CheckIfThereIsATileUp.GetComponent<Transform>().position = new Vector3(snow4.GetComponent<Transform>().position.x, snow4.GetComponent<Transform>().position.y, -10);

        //Merge BoxColliders
        grass1.GetComponent<BoxCollider2D>().enabled = false;
        grass2.GetComponent<BoxCollider2D>().enabled = false;
        grass3.GetComponent<BoxCollider2D>().enabled = false;
        grass4.GetComponent<BoxCollider2D>().size = new Vector2(grass4.GetComponent<BoxCollider2D>().size.x * 4, grass4.GetComponent<BoxCollider2D>().size.y);
        grass4.GetComponent<BoxCollider2D>().offset = new Vector2(-(Vector2.Distance(grass4.GetComponent<Transform>().position, grass3.GetComponent<Transform>().position) / (2 * grass4.GetComponent<Transform>().localScale.x / StaticProperty.sideTiles)), grass4.GetComponent<BoxCollider2D>().offset.y);
        snow1.GetComponent<BoxCollider2D>().enabled = false;
        snow2.GetComponent<BoxCollider2D>().enabled = false;
        snow3.GetComponent<BoxCollider2D>().enabled = false;
        snow4.GetComponent<BoxCollider2D>().size = new Vector2(snow4.GetComponent<BoxCollider2D>().size.x * 4, snow4.GetComponent<BoxCollider2D>().size.y);
        snow4.GetComponent<BoxCollider2D>().offset = new Vector2(-(Vector2.Distance(snow4.GetComponent<Transform>().position, snow3.GetComponent<Transform>().position) / (2 * snow4.GetComponent<Transform>().localScale.x / StaticProperty.sideTiles)), snow4.GetComponent<BoxCollider2D>().offset.y);

        StaticProperty.sideTiles++;

        StaticProperty.score = 0;
        StaticProperty.deadlineDown = grass4.GetComponent<BoxCollider2D>().bounds.center.y + grass4.GetComponent<BoxCollider2D>().bounds.extents.y;
        StaticProperty.deadlineUp = snow4.GetComponent<BoxCollider2D>().bounds.center.y - snow4.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        StaticProperty.grounded = (!StaticProperty.isVisible && Physics2D.OverlapCircle(upGroundCheck.position, groundCheckRadius, whatIsGroundUp)) ||
            (StaticProperty.isVisible && Physics2D.OverlapCircle(downGroundCheck.position, groundCheckRadius, whatIsGroundDown));

        StaticProperty.thereIsATileDown = Physics2D.OverlapCircle(CheckIfThereIsATileDown.GetComponent<Transform>().position, 1, whatIsGroundDown);
        StaticProperty.thereIsATileUp = Physics2D.OverlapCircle(CheckIfThereIsATileUp.GetComponent<Transform>().position, 1, whatIsGroundUp);


        if (StaticProperty.grounded) StaticProperty.canDoubleJump = true;

        //On Tap Event
        if (Input.GetMouseButtonDown(0))
        {
            //Calculate Score
            if (StaticProperty.grounded)
            {
                StaticProperty.score++;
                scoreTextDown.text = $"SCORE: {StaticProperty.score}";
                scoreTextUp.text = $"SCORE: {StaticProperty.score}";
            }

            if (Input.mousePosition.y >= Screen.height / 2)
                StaticProperty.isVisible = false;
            else
                StaticProperty.isVisible = true;

            if (StaticProperty.isVisible)
            {
                cameraDown.backgroundColor = new Color32(49, 77, 255, 1);
                cameraDown.depth = 1;
                cameraUp.backgroundColor = new Color32(49, 77, 93, 1);
                cameraUp.depth = 0;
            }
            else
            {
                cameraDown.backgroundColor = new Color32(49, 77, 93, 1);
                cameraDown.depth = 0;
                cameraUp.backgroundColor = new Color32(49, 77, 255, 1);
                cameraUp.depth = 1;
            }
        }

        //Set Left Edge's position
        StaticProperty.camerasLeftEdgePos = cameraDown.transform.position.x - StaticProperty.screenSize.x;

        //Increase Velocity
        StaticProperty.speed.x += Time.deltaTime * acceleration;

        //Increase gravity
        StaticProperty.fallingGravity += Time.deltaTime / 6;
    }
}
