using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainScene : MonoBehaviour
{
    public EnemyMsg enemyMsg;
    public Image towerRnage;
    public Image towerLiist;
    public Vector2 buildTowerOffset;
    public BaseLand baseLand;
    public Transform currentLandTransform;

    public Button buildLaserBtn;
    public Button buildArrowBtn;
    public GameObject laserTowerPrefab;
    public GameObject arrowTowerPrefab;
    [SerializeField] private Vector3 LaserToweroffset;
    [SerializeField] private Vector3 ArrowToweroffset;

    public enum TowerType
    {
        Laser, Arrow
    }

    void Start()
    {
        enemyMsg.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Seletion();
        }
    }

    public void Seletion()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // ����� UI �¼����ڴ���ֱ�ӷ��أ���ִ�����߼�ⲿ��
                return;
            }

            if (hit.collider.GetComponent<Tower>())
            {
                //��ʾ���Ĺ�����Χ
                Tower tower = hit.collider.GetComponent<Tower>();
                Vector3 towerPos = tower.transform.position;
                int currentLvl = tower.currentLvl;
                towerRnage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(tower.data[currentLvl].levelRange * 2, tower.data[currentLvl].levelRange * 2);
                towerRnage.gameObject.GetComponent<RectTransform>().anchoredPosition3D = towerPos;
                Debug.Log("Select a tower");
                towerRnage.gameObject.SetActive(true);
            }
            else if (hit.collider.GetComponent<Enemy>())
            {
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                enemyMsg.gameObject.SetActive(true);
                enemyMsg.enemyMsgText.text = "ATK: " + enemy.enemyATK;
                Debug.Log("Select a enemy");
            }
            else if (hit.collider.GetComponent<BaseLand>())
            {
                var land = hit.collider.GetComponent<BaseLand>();
                Debug.Log("Land is selected" + land.name);
                Vector3 vector3 = hit.collider.gameObject.transform.position;
                Vector2 screen = Camera.main.WorldToScreenPoint(vector3);//�ؿ����Ļ����

                towerLiist.rectTransform.position = screen + buildTowerOffset;//���ݵؿ����Ļ��������˵���rectTransform

                currentLandTransform = hit.collider.transform;
                baseLand = currentLandTransform.GetComponent<BaseLand>();

                Tower tower = currentLandTransform.GetComponentInChildren<Tower>();//������������Tower

                if (tower == null)//���û��������򿪽����б���δ��ťע�Ὠ���¼�
                {
                    towerLiist.gameObject.SetActive(true);

                    buildLaserBtn.onClick.AddListener(() => BuildTower(TowerType.Laser));
                    buildArrowBtn.onClick.AddListener(() => BuildTower(TowerType.Arrow));
                }
                
                else if (tower != null)//�������
                {

                }
            }
            else
            {
                ClosePanel();
                currentLandTransform = null;
                baseLand = null;
            }
        }
    }

    void ClosePanel()
    {
        enemyMsg.gameObject.SetActive(false);
        towerRnage.gameObject.SetActive(false);//��������ΧIndicator
        StartCoroutine(CloseTowerList());
    }

    IEnumerator CloseTowerList()
    {
        yield return null; // �ȴ�һ֡
        towerLiist.gameObject.SetActive(false);
    }

    public void BuildTower(TowerType towerType)
    {
        if (baseLand != null && !baseLand.hasTower)
        {
            GameObject towerPrefab = null;
            Vector3 towerOffset = Vector3.zero;
            float towerPrice = 0;

            switch (towerType)
            {
                case TowerType.Laser:
                    towerPrefab = laserTowerPrefab;
                    towerOffset = LaserToweroffset;
                    towerPrice = towerPrefab.GetComponent<Tower>().buildCost;
                    break;

                case TowerType.Arrow:
                    towerPrefab = arrowTowerPrefab;
                    towerOffset = ArrowToweroffset;
                    towerPrice = towerPrefab.GetComponent<Tower>().buildCost;
                    break;
            }

            if (towerPrefab != null&&towerPrice<=GameManager.money)
            {
                GameManager.money -= towerPrice;

                GameObject tower = Instantiate(towerPrefab, currentLandTransform.position, Quaternion.identity);
                tower.transform.localScale = towerPrefab.transform.localScale;
                tower.transform.SetParent(currentLandTransform);
                tower.transform.localPosition = towerOffset;

                Debug.Log("Current Money="+GameManager.money);

                baseLand.hasTower = true;
            }
        }
    }
    public void LevelUp(Tower currentTower)
    {
        if (currentTower.data[currentTower.currentLvl].levelPrice <= GameManager.money)
        {
            GameManager.money -= currentTower.data[currentTower.currentLvl].levelPrice;
            currentTower.currentLvl++;
        }
        else Debug.Log("No money");
    }
}
