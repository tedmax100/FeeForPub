Calculation Fee for enter Pub
===

### 需求說明如下
```
當顧客進場時，
如果是女生，則免費入場。
若為男生，則根據 ICheckInFee 介面來取得門票的費用，
並累計到 inCome 中。
透過 GetInCome() 方法取得這一次的門票收入總金額
```

```plantuml
@startuml
interface ICheckInFee {
    decimal GetFee(Customer)
}

class Customer {
    double GetArea()
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
[PlantUMLWeb](http://www.plantuml.com/plantuml/uml/LP1FIyGm4CNl-HH3JwtIebUHij95AIXuybgooKY3-GF9H5Z4xsxQJTmsf-Jbz-RbvH28Uuf6CsK9_ISN2ECme_WQxJCY_318Iw9GXcjuGKfYFSH0pg1ls2zZGlCGe4Z5uE99uy8_UUJr1doFfyoqaAwai_gyIvp4_pvZnvm-AJiuSr6d2GPd0_aeoFbqNDLR-71ABXdrPcHJ74dNIi0Rqknak9f6Iv3n-bK5UYnj-YOJn-i7ZEiZBfMMCjLz1QvjTnqUOERV2D2lHDVrKDrrtKq5PN0YOa0mt9uJjBKky9vAm06jZ4R_0G00)
![](https://ptuml.hackmd.io/svg/NP51QyGW58Jl_ef7JabBmlQwbB0bIqkWXPvw3Qw-9LAZeC-5fVI_LrFJPEj9nsySHm-UkACmQQOCeJjpWT1t8uhFtZmZmXU3E2GADN4DBqXHBBlWoKxeAlRDcD3SUrYanM33IMFYdnpoSWE-XrDcQeXRqXfzlKaSnDyyOyUSFYZJnOnP7GRehF7aWY3hud9Mx-73AfaooisHrUA8kbO2tf16A_rgwWr1nijNvUbnQTDMcJYk3z3Fb_YjbCeK3mNOkhxfi8UHNn2WdOccQQ7ODSrTiJxJ7e97cBYvHcXhJV0_gcK7D3BzqGy0)

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