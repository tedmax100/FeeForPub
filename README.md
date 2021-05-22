Calculation Fee for enter Pub
===

### 需求說明如下  
當顧客進場時，  
如果是**女生**，則**免費**入場。  
若為**男生**，則根據 **ICheckInFee** 介面來取得門票的費用，  
並累計到 **inCome** 中。
透過 **GetInCome**() 方法取得這一次的門票收入總金額   
> 女生改成**每周4**, 為Ladie's Night, **當日免費入場**; 其餘日子都要收費


```plantuml
@startuml
interface ICheckInFee {
    Decimal GetFee(Customer)
}

class Customer {
    + Bool IsMale
    + Int Seq
}

class Pub {
    - readonly ICheckInFee _checkInFee
    - Decimal _inCome
    __ Constructor __
    + Void Pub(ICheckInFee)
    __ Public Methods __
    + Int CheckIn(List<Customer>)
    + Decimal GetInCome()
}

Pub "1" o-- ICheckInFee : have  >
Pub ..> "0..*" Customer : use many >
ICheckInFee ..> Customer : use >
@enduml
```
[PlantUMLWeb](http://www.plantuml.com/plantuml/uml/NP1FIyGm4CNl-HH3JwtIejT5oc8N9U2287WjCHbfC7ymcGYB-DtDbjHspIbv_5xUpDj74e6IDKmxml0k908V9fGVt3qWmZU3V1HAROM1HwGiraEAv2s6Xlqm9es84HQf6AxXtdi3F1w5mQ9mH_22dwldERqLl8M0GdbdJXVXe_ox5-nG1Xcr6tBSh8uZ3Dv52acI3_bPybwzLkUCUlDbipYoRhI48zBaLSpAPiY2rqywqjsoMDyKuh1smUSXwhc6ypBLRGM-RIzssC4alX2Wduckww6wwRghQcri1oaYME5E6TfQC_oFoi0UdKhM_080)
![](https://ptuml.hackmd.io/svg/NP51IyGm48Nl-HL3JwtIejT5ocANbO0BWk2rn7IM1jC4aucmYF_TfAIsQqwJn_VoPYPxJy9Hc3HJXj2TXKJWtOZoWviZ8dmpYEU0Kar2mnDI5CikUB8JkehzC2Qry1uMAHjkuT5Q3ToVXCQiS4FmYf-hvoMyPxm6XsAmHb-kmdlvLsTiQQHNfejnizhtq5dZoGL9riLhpdkpQaWPvUR9Qd54NIi99wJH3durfIOpNZuhJm_BO6sLYSqk-Dn4EQyX3LFS5s3h-cg67OpY2m7QcMYQ5egxfhafretj87Y4IPXBXBRM1F-ZMhP7CwJl-WK0)

### Tests
- 給定入場人數, 驗證收費人數
    - Arrange
      - 入場客戶為3男2女
    - Act
      - 收費人數 := CheckIn(客戶們)
    - Assert
        - excepted: 3人
- 給定入場人數與收費金額, 驗證收費總額
    - Arrange
        - 入場客戶為3男2女
        - 門票收費為100$
    - Act
        - CheckIn(客戶們)
        - 收費總額 := GetInCome()
    - Assert
        - excepted: 300$

- 給定入場人數, 驗證收費總額,  當日為非周四
    - Arrange
        - 入場客戶為3男2女
        - 當日為周五
        - 男生收費100$; 女生收費50$
    - Act
        - CheckIn(客戶們)
        - 收費總額 := GetInCome()
    - Assert
        - excepted: 400$
- 給定入場人數, 驗證收費總額,  當日為周四
    - Arrange
        - 入場客戶為3男2女
        - 當日為周四
        - 男生收費100$; 女生收費0$
    - Act
        - CheckIn(客戶們)
        - 收費總額 := GetInCome()
    - Assert
        - excepted: 300$
    
