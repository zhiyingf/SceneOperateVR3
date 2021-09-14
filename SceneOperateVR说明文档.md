## SceneOperateVR说明文档

> 1. 用户任务
> 2. 核心功能
> 3. 实现操作
> 4. 练习使用
> 5. 自由发挥



#### 1.用户任务

+ 用户使用10min左右学习如何使用VR，以及了解SceneOperateVR核心功能与基本操作
+ 自由体验5min，记录游玩过程中的帧率情况
+ 给定一个规定形状模型，让用户快速搭建出来
+ 自由发挥，创建一个自己喜欢的模型
+ 体验结束



#### 2. 核心功能

在VR中交互式[CSG](https://en.wikipedia.org/wiki/Constructive_solid_geometry)建模，获取3D模型（[*.obj](https://en.wikipedia.org/wiki/Wavefront_.obj_file)）。

CSG建模分为三种布尔运算操作：

+ Union : 			 Mres = Ma ∪ Mb
+ Intersection :    Mres = Ma ∩ Mb
+ Difference :          Mres = Ma ∩ Mb

![image-20210913191849669](C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913191849669.png)

​																													Ma									Mb

![image-20210913191720635](C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913191720635.png)

#### 3. 实现操作

+ 认识HTC VIVE的VR手柄

  + <img src="C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913193712349.png" alt="image-20210913193712349" style="zoom: 80%;" />

+ 获取模型（用于实现CSG）:

  + 模型选择列表：按下**菜单键**打开列表，再按一下菜单键可以关闭列表

  <img src="C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913192650373.png" alt="image-20210913192650373" style="zoom:80%;" />

  + 选择物体：使用射线选中所需要的物体，扣动**扳机键**

<img src="C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913193232913.png" alt="image-20210913193232913" style="zoom: 67%;" />

+ 抓取模型：将手柄伸入（接触）模型，同时按下**扳机键**

+ 缩放模型：将两个手柄同时伸入模型，同时按下扳机键不放，然后两只手柄分别向左右相反方向移动

  + 选择缩放方向：在以上操作的基础上，触摸**右手柄触控板**，选择X，Y，Z，A

    + 分别沿X，Y，Z轴三个方向缩放，或者X，Y，Z同时缩放（A表示X，Y，Z同时缩放）

      <img src="C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913195524967.png" alt="image-20210913195524967" style="zoom: 67%;" />

+ SceneOperate（CSG操作）：首先按照顺序选择模型以及对应的布尔运算操作，然后将模型放置在合适的位置，更新（执行CSG操作）

  + 按照顺序选择模型以及对应的布尔运算操作：将手柄伸入（接触）模型，按下触控板十字键
    + <img src="C:\Users\IRC\Desktop\png\booleanOperation.jpg" alt="booleanOperation" style="zoom: 33%;" />
  + 摆放模型：按下**扳机键**抓取物体摆放
  + 执行CSG操作：按下**手柄按钮**即时更新



#### 4. 练习使用

使用布尔运算构建一个椅子

目标：通过CSG操作构建如下椅子

![image-20210913202852986](C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913202852986.png)

+ 获取模型：

![image-20210913202704824](C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913202704824.png)

+ 选择模型（并操作）：

![image-20210913203921500](C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913203921500.png)

+ 执行CSG操作

  <img src="C:\Users\IRC\AppData\Roaming\Typora\typora-user-images\image-20210913204542728.png" alt="image-20210913204542728" style="zoom: 67%;" />

#### 5. 自由发挥

来创建一个自己喜欢的3D模型吧！
