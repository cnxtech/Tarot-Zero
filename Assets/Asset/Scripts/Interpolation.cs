using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Interpolation {

    public List<KeyFrame> KeyFrames = new List<KeyFrame>();

    public class KeyFrame
    {
        public KeyFrame(float time, float value)
        {
            this.time = time;
            this.value = value;
        }
        public float time;
        public float value;
    }

    public float Length { get; set; }

    /// <summary>
    /// Time elapsed in seconds
    /// </summary>
    public float TimeElapsed { get; private set; }
    public bool HasEnded { get; private set; }
    public int CurrentKey { get; private set; }

    /// <summary>
    /// </summary>
    /// <param name="length">Time in seconds it takes to finish the interpolation</param>
    /// <param name="keys">Stops of each</param>
    public Interpolation(float length)
    {
        this.Length = length;
        Reset();
    }

    public System.Action<KeyFrame> FrameCallback;
    
    public void AddKeyFrame(KeyFrame keyFrame)
    {
        KeyFrames.Add(keyFrame);
        KeyFrames.Sort((a, b) =>
        {
            return (a.time < b.time) ? -1 : 1;
        });
    }

    public void AddKeyFrame(float time, float value)
    {
        AddKeyFrame(new KeyFrame(time, value));
    }

    public void Reset()
    {
        TimeElapsed = 0;
        CurrentKey = 0;
        HasEnded = false;
    }

    /// <summary>
    /// Goes to the respective frame and calls FrameCallback
    /// </summary>
    public KeyFrame Next()
    {
        var kf = UpdateCurrent();
        if(FrameCallback != null) FrameCallback(kf);
        return kf;
    }

    public KeyFrame UpdateCurrent()
    {
        if (!HasEnded)
        {
            TimeElapsed += Time.deltaTime;
            HasEnded = TimeElapsed >= Length;

            if (KeyFrames.Count == 0)
            {
                CurrentKey = 0;
                return new KeyFrame(0, 0);
            }
            else
            {
                int i = 0;
                foreach (var kf in KeyFrames)
                {
                    if(kf.time > TimeElapsed)
                    {
                        break;
                    }
                    i++;
                }
                CurrentKey = Mathf.Clamp(i - 1, 0, KeyFrames.Count -1);
            }

            if(CurrentKey + 1 < KeyFrames.Count && KeyFrames[CurrentKey + 1].time != KeyFrames[CurrentKey].time)
            {
                var valueDistance = TimeElapsed - KeyFrames[CurrentKey].time;
                var maxValueDistance = KeyFrames[CurrentKey + 1].time - KeyFrames[CurrentKey].time;
                var currentValue = KeyFrames[CurrentKey].value + 
                    (KeyFrames[CurrentKey + 1].value - KeyFrames[CurrentKey].value)
                    * valueDistance / maxValueDistance;
                return new KeyFrame(TimeElapsed, currentValue);

            }
            else
            {
                return new KeyFrame(TimeElapsed, KeyFrames[CurrentKey].value);
            }

        }
        return KeyFrames.Count == 0 ? new KeyFrame(0,0) : KeyFrames[KeyFrames.Count - 1];
    }
}
