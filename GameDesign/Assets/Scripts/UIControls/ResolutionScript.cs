using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ResolutionScript : MonoBehaviour
{
    [SerializeField] List<resolution> m_resolution;
    [SerializeField] TextMeshProUGUI ResolutionText;
    [SerializeField] private Toggle isFullScreen;
    private int selectedResolution;

    void Start()
    {
        isFullScreen.isOn = Screen.fullScreen;

        bool foundResolution = false;
        int i = 0;
        foreach (resolution r in m_resolution)
        {
            if (Screen.width == r.width && Screen.height == r.height)
            {
                foundResolution = true;
                selectedResolution = i;

            }
            i++;
        }

        if (!foundResolution)
        {
            resolution newRes = new resolution(Screen.width, Screen.height);
            m_resolution.Add(newRes);
        }

        m_resolution.Sort();

        i = 0;
        foreach (resolution r in m_resolution)
        {
            if (Screen.width == r.width && Screen.height == r.height)
            {
                selectedResolution = i;
                break;
            }
            i++;
        }

        updateResText();

    }


    private void updateResText()
    {
        ResolutionText.text = m_resolution[selectedResolution].width.ToString() + " x " + m_resolution[selectedResolution].height.ToString();
    }
    public void selectRightRes()
    {
        selectedResolution++;
        selectedResolution = selectedResolution % m_resolution.Count;
        updateResText();
    }
    public void selectLeftRes()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = m_resolution.Count - 1;
        updateResText();
    }

    public void applyResolutionChanges()
    {

        Screen.SetResolution(m_resolution[selectedResolution].width, m_resolution[selectedResolution].height, isFullScreen.isOn);


    }

    [System.Serializable]
    public class resolution : IComparable
    {
        public int m_width, m_height;
        public int width { get { return m_width; } }
        public int height { get { return m_height; } }

        public resolution(int width, int height)
        {
            m_width = width;
            m_height = height;
        }

        public int CompareTo(object obj)
        {
            if (obj.GetType() != typeof(resolution)) throw new Exception("Tried to compare a  non resolution with a resolution");
            resolution obj1 = (resolution)obj;

            return m_width.CompareTo(obj1.width);
        }
    }
}
