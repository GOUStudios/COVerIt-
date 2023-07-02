using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetter : MonoBehaviour
{
    [SerializeField] GameObject _ponyTail;
    [SerializeField] GameObject[] _hairbase;
    [SerializeField] GameObject _femaleDetails;
    [SerializeField] GameObject[] _hairDetails;
    [SerializeField] GameObject _hairBand;
    [SerializeField] GameObject _tie;



    void Awake()
    {
        float scale = Random.Range(0.85f, 1.15f);

        this.transform.localScale = new Vector3(transform.localScale.x * scale, transform.localScale.y * scale, transform.localScale.z * scale);

        //decide if its man or woman
        bool isMan = (Random.value > 0.5f);

        _femaleDetails.SetActive(!isMan);

        //functions to determine the acceptable hairstyles
        if (isMan) setMan(); else setWoman();

        //decide if uses a tie or not.
        _tie.SetActive((Random.value > 0.5f));



    }

    private void setMan()
    {
        _ponyTail.SetActive(false);
        _hairBand.SetActive(false);

        //This one is always female
        _hairbase[0].SetActive(false);
        bool hairBase = Random.value > 0.5f;
        _hairbase[1].SetActive(hairBase);
        _hairbase[2].SetActive(!hairBase);//fixed now uses both hair bases

        _hairDetails[0].SetActive(Random.value > 0.85f);
        _hairDetails[1].SetActive(Random.value > 0.5f);

    }
    private void setWoman()
    {

        bool usesPonyTail = Random.value > 0.5f;



        float hairBaseSelection = Random.value;

        _hairbase[0].SetActive(hairBaseSelection < 1 / 3);
        _hairbase[1].SetActive(hairBaseSelection >= 1 / 3 && hairBaseSelection < 2 / 3);
        _hairbase[2].SetActive(hairBaseSelection >= 2 / 3);


        _ponyTail.SetActive(usesPonyTail || hairBaseSelection >= 2 / 3);

        //No good with hair base 3
        _hairBand.SetActive(hairBaseSelection < 2 / 3 && Random.value > 0.5f);

        //the first hairstyle needs this details
        _hairDetails[0].SetActive(hairBaseSelection < 1 / 3 || Random.value > 0.5f);

        _hairDetails[1].SetActive(!_hairBand.activeSelf && Random.value > 0.5f);

    }



}
