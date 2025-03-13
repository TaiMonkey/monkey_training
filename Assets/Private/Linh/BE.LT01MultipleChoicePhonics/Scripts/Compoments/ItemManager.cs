using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonAns;
    private HorizontalLayoutGroup horizontal;

    private List<ButtonDemo1> listButton;
    private Coroutine coroutine;

    void Start()
    {
        listButton = new List<ButtonDemo1>();
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(buttonAns, transform, false);
            obj.transform.localScale = Vector3.one;
            ButtonDemo1 buttonDemo1 = obj.GetComponent<ButtonDemo1>();
            listButton.Add(buttonDemo1);
        }
        horizontal = GetComponent<HorizontalLayoutGroup>();
        coroutine = StartCoroutine(DisableLayout());


    }
    private IEnumerator DisableLayout()
    {
        yield return new WaitForEndOfFrame();
        horizontal.enabled = false;
        foreach (var item in listButton)
        {
            item.orgPos = item.transform.position;
            item.FadeButton(1f);
        }
    }

}
*/