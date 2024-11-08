using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationSpeedController : MonoBehaviour
{
    public enum AnimationMode { RealTime, SlowMotion, FastForward }
    public AnimationMode currentMode = AnimationMode.RealTime;

    [Header("UI Components")]
    public Button realTimeButton;
    public Button slowMotionButton;
    public Button fastForwardButton;
    public Slider slowMotionSlider;
    public Slider fastForwardSlider;
    public Text slowMotionValueText;
    public Text fastForwardValueText;

    [Header("Properties")]
    public float slowMotionSpeed = 2f;
    public float fastForwardSpeed = 2f;

    private void Start()
    {
        // Set slider range values
        slowMotionSlider.minValue = 1.5f;
        slowMotionSlider.maxValue = 5f;
        fastForwardSlider.minValue = 1.5f;
        fastForwardSlider.maxValue = 5f;

        // Set initial slider values and texts
        slowMotionSlider.value = slowMotionSpeed;
        fastForwardSlider.value = fastForwardSpeed;
        slowMotionValueText.text = slowMotionSpeed.ToString("0.0") + "x";
        fastForwardValueText.text = fastForwardSpeed.ToString("0.0") + "x";

        // Assign button click events
        realTimeButton.onClick.AddListener(SetRealTimeMode);
        slowMotionButton.onClick.AddListener(SetSlowMotionMode);
        fastForwardButton.onClick.AddListener(SetFastForwardMode);

        // Assign slider events
        slowMotionSlider.onValueChanged.AddListener(OnSlowMotionSpeedChanged);
        fastForwardSlider.onValueChanged.AddListener(OnFastForwardSpeedChanged);

        // Set the initial time scale based on the default mode
        UpdateTimeScale();
    }

    private void UpdateTimeScale()
    {
        // Adjust Time.timeScale based on the selected mode
        switch (currentMode)
        {
            case AnimationMode.SlowMotion:
                Time.timeScale = 1f / slowMotionSlider.value;
                break;
            case AnimationMode.RealTime:
                Time.timeScale = 1f;
                break;
            case AnimationMode.FastForward:
                Time.timeScale = fastForwardSlider.value;
                break;
        }
    }

    public void SetRealTimeMode()
    {
        currentMode = AnimationMode.RealTime;
        UpdateTimeScale();
    }

    public void SetSlowMotionMode()
    {
        currentMode = AnimationMode.SlowMotion;
        UpdateTimeScale();
    }

    public void SetFastForwardMode()
    {
        currentMode = AnimationMode.FastForward;
        UpdateTimeScale();
    }

    public void OnSlowMotionSpeedChanged(float value)
    {
        slowMotionSpeed = value;
        slowMotionValueText.text = value.ToString("0.0") + "x";
        if (currentMode == AnimationMode.SlowMotion)
        {
            UpdateTimeScale();
        }
    }

    public void OnFastForwardSpeedChanged(float value)
    {
        fastForwardSpeed = value;
        fastForwardValueText.text = value.ToString("0.0") + "x";
        if (currentMode == AnimationMode.FastForward)
        {
            UpdateTimeScale();
        }
    }

    private void OnDisable()
    {
        // Reset the time scale when the script is disabled
        Time.timeScale = 1f;
    }
}
