using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public RectTransform cursorPos;
    public List<Button> buttons;
    //public List<Button> mainButtons;
    public Button selectedButton;
    int index;
    bool canScroll;
    public float originalWaitTime = 0.425f;
    float currentWaitTime;
    public GameObject currentMenu;
    public GameObject previousMenu;
    bool canPause = true;
    public bool vertical = true;
    bool isSubmenu;
    public bool canHold = true;
    public bool closable = true;
    //public MenuSounds ms;
    // Start is called before the first frame update
    void Start()
    {
        isSubmenu = !(previousMenu == null);
    }

    void OnEnable()
    {
        InitiateButtonList();
    }

    void Update()
    {

        if (vertical)
        {
            if (Input.GetAxis("Vertical") < -0.5f)
            {
                if (canScroll)
                {
                    //ms.PlayNext();
                    SelectDownButton();
                    if (canHold)
                    {
                        StartCoroutine(ScrollDown(currentWaitTime));
                    }
                    else
                    {
                        canScroll = false;
                    }
                }
            }
            else if (Input.GetAxis("Vertical") > 0.5f)
            {
                if (canScroll)
                {
                    //ms.PlayNext();
                    SelectUpButton();
                    if (canHold)
                    {
                        StartCoroutine(ScrollDown(currentWaitTime));
                    }
                    else
                    {
                        canScroll = false;
                    }
                }
            }
            else
            {
                canScroll = true;
                if (canHold)
                {
                    StopAllCoroutines();
                    currentWaitTime = originalWaitTime;
                }
            }
        }
        else
        {
            if (Input.GetAxis("Horizontal") < -0.5f)
            {
                if (canScroll)
                {
                    //ms.PlayNext();
                    SelectLeftButton();
                    if (canHold)
                    {
                        StartCoroutine(ScrollDown(currentWaitTime));
                    }
                    else
                    {
                        canScroll = false;
                    }
                }
            }
            else if (Input.GetAxis("Horizontal") > 0.5f)
            {
                if (canScroll)
                {
                    //ms.PlayNext();
                    SelectRightButton();
                    if (canHold)
                    {
                        StartCoroutine(ScrollDown(currentWaitTime));
                    }
                    else
                    {
                        canScroll = false;
                    }
                }
            }
            else
            {
                canScroll = true;
                if (canHold)
                {
                    StopAllCoroutines();
                    currentWaitTime = originalWaitTime;
                }
            }
        }
        if (Input.GetButtonDown("ButtonB") && closable)
        {
            //ms.PlayBack();
            CloseMenu();
        }
        if (Input.GetButtonDown("Jump"))
        {
            //ms.PlaySelect();
            selectedButton.onClick.Invoke();
        }
    }

    IEnumerator ScrollDown(float waitTime)
    {
        canScroll = false;
        yield return new WaitForSecondsRealtime(waitTime);
        canScroll = true;
        if (currentWaitTime >= 0.1f)
        {
            currentWaitTime -= 0.025f;
        }

    }

    public void InitiateButtonList()
    {
        selectedButton = buttons[0];
        index = -1;
        if (vertical)
        {
            SelectDownButton();
        }
        else
        {
            SelectRightButton();
        }
        currentWaitTime = originalWaitTime;
        StartCoroutine(ScrollDown(currentWaitTime));
    }

    public void CloseMenu()
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        if (isSubmenu)
        {
            previousMenu.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void OpenMenu(GameObject newMenu)
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        newMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    void SelectDownButton()
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        index++;
        if (index >= buttons.Count)
        {
            index = 0;
        }
        selectedButton = buttons[index];
        cursorPos.localPosition = new Vector3(136f, selectedButton.GetComponent<RectTransform>().localPosition.y, cursorPos.localPosition.z);
        selectedButton.GetComponentInChildren<Text>().color = Color.red;
    }

    void SelectUpButton()
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        index--;
        if (index < 0)
        {
            index = buttons.Count - 1;
        }
        selectedButton = buttons[index];
        cursorPos.localPosition = new Vector3(136f, selectedButton.GetComponent<RectTransform>().localPosition.y, cursorPos.localPosition.z);
        selectedButton.GetComponentInChildren<Text>().color = Color.red;
    }

    void SelectRightButton()
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        index++;
        if (index >= buttons.Count)
        {
            index = 0;
        }
        selectedButton = buttons[index];
        cursorPos.localPosition = new Vector3(selectedButton.GetComponent<RectTransform>().localPosition.x + 95f, cursorPos.localPosition.y, cursorPos.localPosition.z);
        selectedButton.GetComponentInChildren<Text>().color = Color.red;
        canScroll = false;
    }

    void SelectLeftButton()
    {
        selectedButton.GetComponentInChildren<Text>().color = Color.white;
        index--;
        if (index < 0)
        {
            index = buttons.Count - 1;
        }
        selectedButton = buttons[index];
        cursorPos.localPosition = new Vector3(selectedButton.GetComponent<RectTransform>().localPosition.x + 95f, cursorPos.localPosition.y, cursorPos.localPosition.z);
        selectedButton.GetComponentInChildren<Text>().color = Color.red;
        canScroll = false;
    }
}
