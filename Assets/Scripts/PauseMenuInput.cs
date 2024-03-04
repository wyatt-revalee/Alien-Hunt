using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PauseMenuInput : MonoBehaviour
{
    private EventSystem eventSystem;
    private int currentButton = 0;
    private bool settingButton = false;
    PlayerInput playerInput;

    void Start()
    {
        eventSystem = EventSystem.current;
        GetComponent<PauseMenu>().playerController.OnPrimaryButtonClick += OnMainButtonClick;
        GetComponent<PauseMenu>().playerController.OnMovement += MoveCursor;
    }

    void MoveCursor(Vector2 input)
    {
        if(settingButton)
        {
            return;
        }
        if(gameObject.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(gameObject);
            if (input.y > 0)
            {
                settingButton = true;
                if(currentButton == 1)
                {
                    StartCoroutine(SelectButton(3));
                }
                else
                {
                    StartCoroutine(SelectButton(currentButton - 1));
                }

                
            }
            else if (input.y < 0)
            {
                settingButton = true;
                if (currentButton == 3)
                {
                    StartCoroutine(SelectButton(1));
                }
                else
                {
                    StartCoroutine(SelectButton(currentButton + 1));
                }
            }
        }
    }

    public IEnumerator SelectButton(int button)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log("Selecting button " + button);
        eventSystem.currentSelectedGameObject.transform.GetChild(currentButton).GetComponent<TextMeshProUGUI>().color = Color.white;
        currentButton = button;
        eventSystem.currentSelectedGameObject.transform.GetChild(currentButton).GetComponent<TextMeshProUGUI>().color = Color.red;
        settingButton = false;
    }

    public void OnMainButtonClick()
    {
        if(!gameObject.activeInHierarchy)
        {
            return;
        }
        eventSystem.currentSelectedGameObject.transform.GetChild(currentButton).GetComponent<Button>().onClick.Invoke();
    }
}
