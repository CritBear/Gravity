#region Using
using UnityEngine;
using System;
using System.Collections;
#endregion // Using

public class RecordScreens : MonoBehaviour
{

	#region Variabels
	int m_nCount = 0;
	#endregion // Variabels

	// Use this for initialization
	void Start ()
	{
	
	}
	
	#region Functions
	public bool IsRecording()
	{
		if ( m_nCount > 0)
			return true;
		return false;
			
	}
	#endregion // Functions
	
	#region Updates
	void Update()
	{
		UpdateSnapshot();
		
		if ( m_nCount > 0 )
		{
			m_nCount--;
			string screenshotFilename;
            DateTime td = System.DateTime.Now;
			int number = 500 - m_nCount;
			screenshotFilename = "..//..//ScreenShots//08 - Nebula//Nebula - " + number.ToString("0000") + ".png";
            // screenshotFilename = "ScreenShots//RED - " + td.ToString("yyyy MM dd-HH-mm-ss-ffff") + ".png";
            ScreenCapture.CaptureScreenshot(screenshotFilename);
		}
	}
	#endregion Updates
	
	
	#region Debug extras
    void UpdateSnapshot()
    {
	
		if ( Input.GetKeyDown("r") )
        {
			m_nCount = 500;
		}
		
        if (Input.GetKeyDown("k"))
        {
            string screenshotFilename;
            DateTime td = System.DateTime.Now;
            screenshotFilename = "ScreenShots//SS - " + td.ToString("yyyy MM dd-HH-mm-ss-ffff") + ".png";
            ScreenCapture.CaptureScreenshot(screenshotFilename);
        }
    }
    #endregion // Debug extras
}
