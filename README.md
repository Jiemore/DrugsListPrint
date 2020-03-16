# DrugsListPrint
### 药品单据打印.任务进度

> 2020年3月8日

~~1、主页面设计~~

~~2、添加页面设计~~

~~3、Access数据库ORM操作接口封装~~

~~4、Access数据库接口测试~~

~~5、程序运行时在没有找到数据库文件时创建数据库~~

> 2020年3月9日

~~6、添加数据 (bug：有些数据类型无法插入)~~

~~7、模糊查询药品数据~~

~~8、DataGridView数据集绑定以及操作~~
- DataGridView [删除][编辑][打印] 按钮的实现(Done)
- DataGridView 工作区打印数据转移到打印预览区(Done)
- DataGridView 打印区用户可以[调整顺序]
- DataGridView 打印预览区[分页]

> 2020年3月10日

9、打印、导出Excel
 - 创建RDLC报表
 - 数据绑定，渲染RDLC报表
 - 数据表分页与页小计

### 药品单据打印.数据库与视图解析

    表名：DrugsTable

标题  | 字段名 | 类型
---------|----------|---------
 主键 | DrugsID | 
 药品名称 | DrugsName | adVarWChar(255)
 规格 | Specification | adVarWChar(255)
 型号 | Model | C3
 单位 | Unti | C1
 单价 | Price | C1
 产地 | Origin | C1
 批准批号 | Batch | C2
 生产日期 | ProductionDate | C3
 有效期 | Validity | C3
 库存 | Stock | C2
 质量状况 | Quality | C1
 

 ### 药品单据打印.坑与解决方案
  1. 关于 Winform　下 ReportViewer 打印异常
      - [解决方案](https://www.cnblogs.com/sasbya/articles/986649.html)：删除  xxxxxx.pdb

  2. Rdlc报表出现空白页
     - [解决方案](https://blog.csdn.net/yang_629/article/details/7949318)：ConsumeConteinerWhitespace 的属性，默认是false，改成True

  3. 获取管理员权限
     - [解决方案](https://blog.csdn.net/u014597198/article/details/76614937)：启用ClickOnce安全设置
    
  4. 排版
     - [解决方案](https://blog.csdn.net/yixian2007/article/details/51697492)：RDLC报表纵向合并单元格的正确解决方案

