using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomInstantiate : MonoBehaviour
{
    [SerializeField] private RectTransform button;
    [SerializeField] private Transform parent;
    private RectTransform[] buttonGameObject = new RectTransform[3];

    GameManager gm;


    int[] random = new int[3];
    bool is25 = false;
    bool is50 = false;
    bool is75 = false;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    
    public void RandomSpawn()
    {
        for(int i = 0; i<3; i++)
        {
            random[i] = Random.Range(0, 75);
            if (random[i] <= 25)
            {
                if(!is25)
                {
                    random[i] = -150;
                    is25 = true;
                }
                else if(is25 && !is50 || !is50 && is75)
                {
                    random[i] = 0;
                    is50 = true;
                }
                else if(is25 && !is75 || !is75 && is50)
                {
                    random[i] = 150;
                    is75 = true;
                }
            }
            else if (random[i] > 25 && random[i] <= 50)
            {
                if(!is50)
                {
                    random[i] = 0;
                    is50 = true;
                }

                else if (!is25 && is50 || !is25 && is75)
                {
                    random[i] = -150;
                    is25 = true;
                }
                else if(is50 && !is75 || is25 && !is75)
                {
                    random[i] = 150;
                    is75 = true;
                }
            }
            else if (random[i] > 50 && random[i] <= 75)
            {
                if(!is75)
                {
                    random[i] = 150;
                    is75 = true;
                }

                else if (!is25 && !is50 || !is25 && is50)
                {
                    random[i] = -150;
                    is25 = true;
                }

                else if (!is50 && is25 || !is50 && is25)
                {
                    random[i] = 0;
                    is50 = true;
                }
            }
        }
    }

    public void InstantiateSpawn()
    {
        Vector2 position = button.anchoredPosition;

        for(int i = 0; i<3; i++)
        {
            buttonGameObject[i] = Instantiate(button, new Vector2(0,0), Quaternion.identity, parent);
            buttonGameObject[i].anchoredPosition = position = new Vector2(random[i], 0);
            buttonGameObject[i].GetComponent<Button>().onClick.AddListener(() => destroyAll());
        }
    }

    public void destroyAll()
    {
        gm.isDestroy = true;
        is25 = false;
        is50 = false;
        is75 = false;

        for(int i = 0; i<3; i++)
        {
            Destroy(buttonGameObject[i].gameObject);
        }
    }

}
