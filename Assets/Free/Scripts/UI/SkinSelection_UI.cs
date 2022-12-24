using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelection_UI : MonoBehaviour
{
    [SerializeField] private int[] priceForSkin;
    [SerializeField] private bool[] skinPurchased;
                     private int skind_Id;


    [Header("Componoents")]
    [SerializeField] private TextMeshProUGUI bankText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;

    private void SetupSkinInfo()
    {
        skinPurchased[0] = true;

        for( int i = 1; i < skinPurchased.Length; i++)

        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;

            if (skinUnlocked)
                skinPurchased[i] = true;
        }
        bankText.text = PlayerPrefs.GetInt("TotalFruitsCollected").ToString();


        selectButton.SetActive(skinPurchased[skind_Id]);
        buyButton.SetActive(!skinPurchased[skind_Id]);

        if (!skinPurchased[skind_Id])
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skind_Id];

        anim.SetInteger("skinId", skind_Id);    
    }

    public bool EnoughMoney()
    {
        int totalFuits = PlayerPrefs.GetInt("TotalFruitsCollected");

        if (totalFuits > priceForSkin[skind_Id])
        {
            totalFuits = totalFuits - priceForSkin[skind_Id];

            PlayerPrefs.SetInt("TotalFruitsCollected", totalFuits);

            AudioManager.instance.PlaySFX(7);
            return true;
        }

        AudioManager.instance.PlaySFX(6);
        return false;
    }

    public void NextSkin()
    {
        AudioManager.instance.PlaySFX(8);
        skind_Id++;
        if (skind_Id > 3)
            skind_Id = 0;

        SetupSkinInfo();
    }
    public void PreviousSkin()
    {
        AudioManager.instance.PlaySFX(8);

        skind_Id--;

        if (skind_Id < 0)
            skind_Id = 3;

        SetupSkinInfo();
    }

    public void Buy()

    {
        if(EnoughMoney())
    {
        PlayerPrefs.SetInt("SkinPurchased" + skind_Id, 1);
        SetupSkinInfo();
    }
      else
          Debug.Log("NotEnoughMoney");

    }       
    public void Select()
    {
        PlayerManager.instance.choosenSkinId = skind_Id;

    }
    public void SwitchSelectButton(GameObject newButton)
    {
       selectButton = newButton; 
    }

    private void OnEnable()
    {
        SetupSkinInfo();
    }
    private void OnDisable()
    {
        selectButton.SetActive(false);
    }
}
    

