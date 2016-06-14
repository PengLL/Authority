# AuthoritySystem
###一个基本.net MVC4的权限管理系统
#####多个用户=》一个角色   一个角色=》多个权限
#####在数据库中设置角色表的时候如果设置SuperAdmin字段，则该角色拥有所有的权限
*
#####解决方案的结构为四个project，一个MVC4的project，三个类库的project。
#####三个类库分别为
*IAuthorityManager：权限管理接口，自定义的Memebership接口
*Lib：对以上接口的实现，和重写AuthorizeAttribute的AuthorizCore方法
*Domain：和数据库连接，获取用户，角色，权限等数据
