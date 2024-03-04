using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class ShopInput : MonoBehaviour
{
    private EventSystem eventSystem;
    public int currentButton = 0;
    public int selectedItem = 0;
    public int itemsBought = 0;
    private bool settingButton = false;
    private bool shopHasItems = true;


    void Start()
    {
        eventSystem = EventSystem.current;
        GetComponent<Shop>().playerController.OnPrimaryButtonClick += OnMainButtonClick;
        GetComponent<Shop>().playerController.OnMovement += MoveCursor;
        StartCoroutine(SelectButton(0));
        StartCoroutine(SelectItem(0));
    }

    void MoveCursor(Vector2 input)
    {
        if (settingButton)
        {
            return;
        }
        if (gameObject.activeInHierarchy)
        {
            eventSystem.SetSelectedGameObject(gameObject);
            if (input.y > 0)
            {
                settingButton = true;
                // If an item is selected and player moves up, set refresh button as selected
                if (currentButton == 0)
                {
                    transform.GetChild(currentButton).GetChild(selectedItem).GetComponent<ShopItem>().HideDescription();
                    selectedItem = 1;
                    StartCoroutine(SelectButton(2));
                }
                settingButton = false;
            }
            else if (input.y < 0)
            {
                settingButton = true;
                // Set shop items as selected, highlight the first item
                if (currentButton == 1 || currentButton == 2)
                {
                    if(!shopHasItems)
                    {
                        settingButton = false;
                        return;
                    }
                    Debug.Log("Setting shop items as selected");
                    selectedItem = 0;
                    StartCoroutine(SelectButton(0));
                    StartCoroutine(SelectItem(0));
                }
                settingButton = false;
            }

            // If player moves left or right and shop items are not selected, move controls between refresh and close
            if(currentButton != 0)
            {
                if (input.x != 0)
                {
                    settingButton = true;
                    if (input.x > 0 && currentButton < gameObject.transform.childCount - 1)
                    {
                        StartCoroutine(SelectButton(currentButton + 1));
                    }
                    else if (input.x < 0 && currentButton > 1)
                    {
                        StartCoroutine(SelectButton(currentButton - 1));
                    }
                    settingButton = false;
                }
            }
            
            // If shop items are selected, move controls between items
            if(currentButton == 0)
            {
                if (input.x != 0)
                {
                    settingButton = true;
                    if (input.x > 0 && selectedItem < transform.GetChild(0).childCount - 1)
                    {
                        StartCoroutine(SelectItem(selectedItem + 1));
                    }
                    else if (input.x < 0 && selectedItem > 0)
                    {
                        StartCoroutine(SelectItem(selectedItem - 1));
                    }
                    settingButton = false;
                }
            }
        }
    }

    public IEnumerator SelectButton(int button)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log("Selecting button " + button);
        transform.GetChild(currentButton).GetComponent<Image>().color = Color.white;
        currentButton = button;
        transform.GetChild(currentButton).GetComponent<Image>().color = Color.red;
        selectedItem = 0;
        settingButton = false;
    }

    public IEnumerator SelectItem(int item)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log("Previous Item: " + selectedItem);
        Debug.Log("New Item: " + item);
        transform.GetChild(currentButton).GetChild(selectedItem).GetComponent<ShopItem>().HideDescription();
        selectedItem = item;
        transform.GetChild(currentButton).GetChild(selectedItem).GetComponent<ShopItem>().ShowDescription();
        settingButton = false;
    }

    public void OnMainButtonClick()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        if(currentButton == 0)
        {
            // Click on shop item
            transform.GetChild(currentButton).GetChild(selectedItem).GetComponent<Button>().onClick.Invoke();
            itemsBought++;
            if (itemsBought == GetComponent<Shop>().itemsInShop)
            {
                Debug.Log("All items bought");
                shopHasItems = false;
                StartCoroutine(SelectButton(1));
                itemsBought = 0;
            }
            else
            {
                selectedItem = 0;
                StartCoroutine(SelectItem(0));
            }
        }
        else
        {
            // Click of shop menu button
            transform.GetChild(currentButton).GetComponent<Button>().onClick.Invoke();
            shopHasItems = true;
            itemsBought = 0;
        }
    }
}
