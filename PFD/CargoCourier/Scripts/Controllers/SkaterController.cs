using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkaterController : MonoBehaviour
{
    public enum hostageEnum {SkaterA, SkaterB}
    public hostageEnum HostageEnum;
    private GameObject helper;

    Animator skaterAnimator;
    Renderer hostageRenderer;
    Texture texture;

    float scaleStart;

    private Route routeScript;
    private int CurrentWayPointID;

    public float transitionSpeedBase;
    public float transitionSpeed;
    public float transitionGoalSpeed;

    public float rotationSpeedBase;
    public float rotationSpeed;
    public float rotationGoalSpeed;

    public float animationSpeed;
    public float animationGoalSpeed;

    public float reachDistance;
    string routeName = "_skateRouteA";

    Vector3 current_position;
    Vector3 homePosition;

    GameObject healthBarBase;
    Image healthBar;
    float fillAmount;

    bool isSpeeded;

    string trick = "null";
    string[] tricks = { "trickA", "trickB", "trickC" };

    //string[] tricks = { "trickA" };
    //string[] tricks = { "trickB" };
    //string[] tricks = { "trickC" };




    private void Awake()
    {
        //Application.targetFrameRate = 60;
    }


    void Start()
    {
        routeScript = GameObject.Find(routeName).GetComponent<Route>();

        skaterAnimator = GetComponentInChildren<Animator>();
        hostageRenderer = GetComponentInChildren<Renderer>();
        texture = hostageRenderer.material.mainTexture;

        float[] _scales = { 0.6f };
        scaleStart = _scales[Random.Range(0, _scales.Length)];
        gameObject.transform.localScale = new Vector3(scaleStart, scaleStart, scaleStart);

        SetAnimationSpeed(animationSpeed);
    }

    void SetAnimationSpeed(float _animationSpeed)
    {
        skaterAnimator.SetFloat("speed", _animationSpeed);
        //Debug.Log(_boolState);
    }

    void SetAnimationBool(bool _boolState)
    {
        skaterAnimator.SetBool(trick, _boolState);
        //Debug.Log(_boolState);
    }

    void Material(float _sat, float _val, Texture _texture)
    {
        Color _color = Color.HSVToRGB(0f, _sat, _val);
        hostageRenderer.material.color = _color;
        hostageRenderer.material.mainTexture = _texture;
    }

    void Update()
    {
        float distance = Vector3.Distance(routeScript.path_objs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, routeScript.path_objs[CurrentWayPointID].position, Time.deltaTime * transitionSpeedBase * transitionSpeed);

        var rotation = Quaternion.LookRotation(routeScript.path_objs[CurrentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeedBase * rotationSpeed);

        if (distance <= reachDistance)
        {
            if (routeScript.path_objs[CurrentWayPointID].name == "trick")
            {
                trick = tricks[Random.Range(0, tricks.Length)];
                SetAnimationBool(true);
                Debug.Log(routeScript.path_objs[CurrentWayPointID].name);
                Debug.Log("Current trick: " + trick);
            }
            else if (routeScript.path_objs[CurrentWayPointID].name == "skate")
            {
                SetAnimationBool(false);
                Debug.Log(routeScript.path_objs[CurrentWayPointID].name);
                Debug.Log("Current trick: " + trick);
            }
            CurrentWayPointID++;
        }

        if (CurrentWayPointID >= routeScript.path_objs.Count)
        {
            CurrentWayPointID = 0;
        }

    }

    private void OnMouseUp()
    {
        if (!isSpeeded)
        {
            StartCoroutine(CoroutineAnimateSpeed());
            StartCoroutine(CoroutineAnimateScale());
            isSpeeded = true;
        }
    }

    IEnumerator CoroutineAnimateSpeed ()
    {
        float currentTime = 0;
        float time = 0.25f;
        float increaser;
        float _startTransitionSpeed = transitionSpeed;
        float _startRotationSpeed = rotationSpeed;
        float _startAnimationSpeed = animationSpeed;

        while (currentTime <= time)
        {
            currentTime += Time.deltaTime;
            increaser = currentTime / time;

            transitionSpeed = Mathf.Lerp(_startTransitionSpeed, transitionGoalSpeed, increaser);
            rotationSpeed = Mathf.Lerp(_startRotationSpeed, rotationGoalSpeed, increaser);
            animationSpeed = Mathf.Lerp(_startAnimationSpeed, animationGoalSpeed, increaser);

            SetAnimationSpeed(animationSpeed);

            Debug.Log(transitionSpeed);
            Debug.Log(rotationSpeed);
            Debug.Log(animationSpeed);

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        currentTime = 0;
        time = 2f;

        while (currentTime <= time)
        {
            currentTime += Time.deltaTime;
            increaser = currentTime / time;

            transitionSpeed = Mathf.Lerp(transitionGoalSpeed, _startTransitionSpeed, increaser);
            rotationSpeed = Mathf.Lerp(rotationGoalSpeed, _startRotationSpeed, increaser);
            animationSpeed = Mathf.Lerp(animationGoalSpeed, _startAnimationSpeed, increaser);

            SetAnimationSpeed(animationSpeed);

            Debug.Log(transitionSpeed);
            Debug.Log(rotationSpeed);
            Debug.Log(animationSpeed);

            yield return null;
        }
        isSpeeded = false;
    }

    IEnumerator CoroutineAnimateScale ()
    {
        float currentTime = 0;
        float time = 0.08f;
        float increaser;
        float scaleUp = scaleStart * 1.4f;
        Vector3 _scaleStart = new Vector3(scaleStart, scaleStart, scaleStart);
        Vector3 scaleEnd = new Vector3(scaleUp, scaleUp, scaleUp);

        while (currentTime <= time)
        {
            currentTime += Time.deltaTime;
            increaser = currentTime / time;
            transform.localScale = Vector3.Lerp(_scaleStart, scaleEnd, increaser);
            yield return null;
        }

        transform.localScale = scaleEnd;
        currentTime = 0;

        while (currentTime <= time)
        {
            currentTime += Time.deltaTime;
            increaser = currentTime / time;
            transform.localScale = Vector3.Lerp(scaleEnd, _scaleStart, increaser);
            yield return null;
        }
        transform.localScale = _scaleStart;
    }
}
