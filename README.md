# AuthoritySystem
###一个基本.net MVC4的权限管理系统
#####多个用户=》一个角色   一个角色=》多个权限
#####项目可以进行角色用户的增改删查，权限的添加，角色权限的更改，日志管理。例如：无用户管理的权限和广告页权限的用户，看不到这两个操作的选项。
***
#####解决方案的结构为四个project，一个MVC4的project，三个类库的project。
#####三个类库分别为
######IAuthorityManager：权限管理接口，自定义的Memebership接口
######Lib：对以上接口的实现，和重写AuthorizeAttribute的AuthorizCore方法
######Domain：和数据库连接，获取用户，角色，权限等数据
***
#####本人数据库的设计：
![](https://github.com/PengLL/AuthoritySystem/raw/master/ReadmeImage/sql.png)
######ps：在数据库中设置角色表的时候如果设置SuperAdmin字段为true，则该角色拥有所有的权限
######    删除角色本人用的是存储过程，因为涉及到删除属于该角色的所有用户，角色权限等，涉及了四张表。其他地方用的是级联。
![](https://github.com/PengLL/AuthoritySystem/raw/master/ReadmeImage/proc.png)
######    大家可以按照自己的需求设计数据库，但是在Domainproject中的sql语句需要相应进行更改。其他部分不需要更改。各个部分耦合度都比较低。
######    现在这个版本并不是十分健壮，后续会进行更改。
