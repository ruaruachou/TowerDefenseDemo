# TowerDefenseDemo

Translate方法，需要先把方向转化成单位向量normalized，然后再乘以速度和deltaTime。

多层数据嵌套，可以先创建一个没有Mono的序列化[Serializable]类，然后在其他脚本中实例化该脚本。如敌人生成的各种数据

设置image大小的方法：gameObject.GetComponent<RectTransform>().sizeDelta=new Vector2(radius,radius);

用封装的思维更新攻击目标：实时遍历满足条件的潜在目标，但仅在当前目标丢失时才更新下一个目标

解决UI和3D物体重叠，导致UI被射线穿过，UI无法点击的解决方法：
加入3D物体的点击方法中，最前面加入判断
```
if (EventSystem.current.IsPointerOverGameObject())
        {
            // 如果有 UI 事件正在处理，直接返回，不执行射线检测部分
            return;
        }
        
``` 

为按钮添加动态参数的语法
```
 buildLaserBtn.onClick.AddListener(() => BuildTowerLaser(currentLandTransform));

 ```

 关闭Panel和触发事件同时进行，导致无法关闭正常关闭的解决方案
 将本条Panel.SetActive(false)单独用携程控制
 ```
  IEnumerator CloseTowerList()
    {
        yield return null; // 等待一帧
        towerLiist.gameObject.SetActive(false);
    }
```