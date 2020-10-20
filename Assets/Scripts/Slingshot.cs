using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private static Slingshot S;

    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    public LineRenderer band;
    public GameObject leftArm;
    public GameObject rightArm;

    private Rigidbody projectileRigidbody;

    public static Vector3 LAUNCH_POS
    {
        get
        {
            if (S == null)
                return Vector3.zero;

            return S.launchPos;
        }
    }

    void Awake()
    {
        S = this;
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;

        // Slingshot band GOs and components
        band = S.GetComponent<LineRenderer>();
        leftArm = GameObject.Find("LeftArm");
        rightArm = GameObject.Find("RightArm");
    }

    void Start()
    {
        ResetBand();
    }

    void OnMouseEnter()
    {
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        launchPoint.SetActive(false);
        ResetBand();
    }

    void OnMouseDown()
    {
        aimingMode = true;

        projectile = Instantiate(prefabProjectile);

        projectile.transform.position = launchPos;

        projectileRigidbody =  projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (launchPoint.activeSelf)
            band.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (!aimingMode)
            return;

        band.SetPosition(1, projectile.transform.position);

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 mouseDelta = mousePos3D - launchPos;

        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;

            MissionDemolition.ShotsFired();
            ProjectileLine.S.poi = projectile;
            ResetBand();
        }
    }

    void ResetBand()
    {
        Vector3[] restingBandPositions =
{
            leftArm.transform.position,
            new Vector3(-12,-8.5f,0),
            rightArm.transform.position
        };

        band.SetPositions(restingBandPositions);
    }
}
