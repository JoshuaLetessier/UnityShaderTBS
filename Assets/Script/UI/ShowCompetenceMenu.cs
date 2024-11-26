using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowCompetenceMenu : MonoBehaviour
{
    [SerializeField] GameObject _competenceMenu;
    [SerializeField] GameObject _competenceButtonPrefab;
    [SerializeField] GameObject _textPrefab;
    [SerializeField] public Entity _entity;

    [SerializeField] GameObject _blocCompetence;
    [SerializeField] GameObject _blocDamage;
    [SerializeField] GameObject _blocCost;
    [SerializeField] GameObject _blocCooldown;

    private void Start()
    {
        ShowMenu(_entity);
    }

    public void ShowMenu(Entity entity)
    {
        _entity = entity;
        List<Competence> competences = entity.getCompetence();
        for (int i = 0; i < competences.Count; i++)
        {
            GameObject competenceButton = Instantiate(_competenceButtonPrefab, _blocCompetence.transform);
            competenceButton.GetComponentInChildren<Text>().text = competences[i].Name;

            GameObject textDamage = Instantiate(_textPrefab, _blocDamage.transform);
            textDamage.GetComponent<Text>().text = "" + competences[i].Damage;

            GameObject textCost = Instantiate(_textPrefab, _blocCost.transform);
            textCost.GetComponent<Text>().text = "" + competences[i].Cost;

            GameObject textCooldown = Instantiate(_textPrefab, _blocCooldown.transform);
            textCooldown.GetComponent<Text>().text = "" + competences[i].Cooldown;


            //ajouter un décalge si i > 0
            if(i > 0) {
                competenceButton.transform.position = new Vector3(competenceButton.transform.position.x, competenceButton.transform.position.y - 50, competenceButton.transform.position.z);
                textDamage.transform.position = new Vector3(textDamage.transform.position.x, textDamage.transform.position.y - 50, textDamage.transform.position.z);
                textCost.transform.position = new Vector3(textCost.transform.position.x, textCost.transform.position.y - 50, textCost.transform.position.z);
                textCooldown.transform.position = new Vector3(textCooldown.transform.position.x, textCooldown.transform.position.y - 50, textCooldown.transform.position.z);
            }
        }

        if(_competenceMenu.activeSelf == false)
            _competenceMenu.SetActive(true);
    }
}
