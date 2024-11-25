using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowCompetenceMenu : MonoBehaviour
{
    [SerializeField] GameObject _competenceMenu;
    [SerializeField] GameObject _competenceButtonPrefab;

    public void ShowMenu(Entity entity)
    {
        List<Competence> competences = entity.getCompetence();
        for (int i = 0; i < competences.Count; i++)
        {
            GameObject competenceButton = Instantiate(_competenceButtonPrefab, _competenceMenu.transform);
            competenceButton.IsConvertibleTo<Button>(true);

            string text = competences[i].Name + " " + competences[i].Cost + " " + competences[i].Cooldown;
            competenceButton.GetComponentInChildren<Text>().text = text;
        }

        _competenceMenu.SetActive(true);
    }
}
