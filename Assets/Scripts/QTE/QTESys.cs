
/*
    Startup example:

    bool qteOk;

    qteSys.Run(
        timeLimit: 1.2f,
        key: KeyCode.E,
        requiredPresses: 5,
        onDone: result => { qteOk = result; }
    );
*/


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UI;

public class QTESys : MonoBehaviour
{
    GameObject uiRoot;
    bool running;
    Text uiText;

    private float timer = 0f;
    public float interval = 50.0f;
    void Update()
    {
        bool qteOk;

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            Run(
                timeLimit: 1.2f,
                key: Key.E,
                requiredPresses: 5,
                onDone: result => { qteOk = result; }
            );
        }
    }

    public void Run(float timeLimit, Key key, int requiredPresses, Action<bool> onDone)
    {
        if (running) return;
        StartCoroutine(RunRoutine(timeLimit, key, requiredPresses, onDone));

        // StartCoroutine() runs the method as a coroutine: it doesn’t execute the whole function in one frame.
        // It runs until it reaches `yield return null;`, then pauses and continues from the same place on the next frame.
        // if (running) return;` prevents starting the coroutine again while it’s already running.
    }

    IEnumerator RunRoutine(float timeLimit, Key key, int requiredPresses, Action<bool> onDone)
    {
        running = true;

        CreateUIIfNeeded();

        uiText.text = $"{key}";

        uiRoot.SetActive(true);

        int presses = 0;
        float t = timeLimit;

        while (t > 0f)
        {
            t -= Time.deltaTime;

            if (Keyboard.current != null && Keyboard.current[key].wasPressedThisFrame)
            {
                presses++;
                uiText.transform.localScale = Vector3.one * 1.3f;
                // You can add a visual effect here, e.g., change the color.
            }
            uiText.transform.localScale = Vector3.Lerp(uiText.transform.localScale, Vector3.one, Time.deltaTime * 10f);

            if (presses >= requiredPresses)
            {
                Finish(true, onDone);
                yield break;
            }

            yield return null;
        }
        Finish(false, onDone);
    }

    void Finish(bool result, Action<bool> onDone)
    {
        if (uiRoot) uiRoot.SetActive(false);
        running = false;
        onDone?.Invoke(result);
        Debug.Log($"QTE Finished. Result: {result}");
        // End of the QTE (execution finished)
    }

    void CreateUIIfNeeded()
    {
        if (uiRoot) return;

        // Canvas
        uiRoot = new GameObject("QTE_UI");
        var canvas = uiRoot.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        uiRoot.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        uiRoot.AddComponent<GraphicRaycaster>();

        // Panel
        var panelGO = new GameObject("Panel");
        panelGO.transform.SetParent(uiRoot.transform, false);
        var img = panelGO.AddComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0.55f);

        var rt = panelGO.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0.2f, 0.2f);
        rt.anchorMax = new Vector2(0.2f, 0.2f);
        rt.pivot = new Vector2(0.2f, 0.2f);
        rt.sizeDelta = new Vector2(80, 80);
        rt.anchoredPosition = Vector2.zero;

        // Text
        var textGO = new GameObject("Text");
        textGO.transform.SetParent(panelGO.transform, false);
        uiText = textGO.AddComponent<Text>();
        uiText.alignment = TextAnchor.MiddleCenter;
        uiText.fontSize = 26;
        uiText.color = Color.white;
        uiText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        var trt = textGO.GetComponent<RectTransform>();
        trt.anchorMin = Vector2.zero;
        trt.anchorMax = Vector2.one;
        trt.offsetMin = new Vector2(20, 20);
        trt.offsetMax = new Vector2(-20, -20);

        uiRoot.SetActive(false);
    }

    void OnDestroy()
    {
        if (uiRoot) Destroy(uiRoot);
    }
}


// Made by Mike Flint