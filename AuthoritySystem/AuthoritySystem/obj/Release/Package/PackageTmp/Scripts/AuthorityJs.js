var Ipaddress = "http://192.168.2.178:92";


function GetCheckedItems()
{
    var checkedBoxs = document.getElementsByClassName("cb");
    var CheckedBoxsSelected=new Array();
    var count=0;
    for(var i=0;i<checkedBoxs.length;i++)
    {
        if (checkedBoxs[i].checked)
        {
            CheckedBoxsSelected[count] = checkedBoxs[i].value;
            count++;
        }
           
    }
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange=function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4);
    }
    xmlhttp.open("GET", Ipaddress+"/Authority/AuthorityChanged?AuthorityArray=" + CheckedBoxsSelected,true);
    xmlhttp.send();
}
function UserUpdate(userName)
{

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", Ipaddress+"/Authority/UserUpdate?userName="+userName, true);
    xmlhttp.send();

}
function RoleChoose()
{
    var node = document.getElementById("selected");
    var roleName = node.options[node.selectedIndex].text;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("POST", Ipaddress+"/Authority/RoleChoose", true);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.send("roleName="+roleName);

    location.href = Ipaddress+"/Authority/UserManage";
}
function UserDelete(userId)
{
    if (confirm("确认删除该用户吗"))
    {
        location.href = Ipaddress+"/Authority/UserDelete?userId=" + userId;
    }   
}
function RoleDelete(roleId)
{
    if (confirm("确认删除该角色吗")) {
        location.href = Ipaddress+"/Authority/RoleDelete?roleId=" + roleId;
    }
}
function Search()
{
    var text = document.getElementById("searchText").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", Ipaddress+"/Authority/UsersSearch?text=" + text, true);
    xmlhttp.send();
}
function RolesAuthorities()
{
    var checkedBoxs = document.getElementsByClassName("cb");
    var authoritiesId = new Array();
    var count = 0;
    for (var i = 0; i < checkedBoxs.length; i++) {
        if (checkedBoxs[i].checked) {
            authoritiesId[count] = checkedBoxs[i].value;
            count++;
        }
    }
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4)
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
    }
    xmlhttp.open("POST", Ipaddress+"/Authority/RoleAuthoritiesChoose", true);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.send("authoritiesId="+authoritiesId);

    location.href = Ipaddress+"/Authority/RoleManage";
}
function AuthorityChanged(roleName)
{
    location.href = Ipaddress+"/Authority/AuthorityChanged?roleName=" + roleName;
}
function SeacrhRoles()
{
    var text = document.getElementById("searchText").value;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", Ipaddress+"/Authority/RolesSearch?text=" + text, true);
    xmlhttp.send();
}
function RolesUsers(roleName)
{
    location.href=Ipaddress+"/Authority/RolesUsers?roleName=" +roleName;
}
function RolesAuthorityCheck(roleName)
{
    location.href = Ipaddress+"/Authority/AuthorityCheck?roleName=" + roleName;
}
function UsersRoleChange()
{
    var node = document.getElementById("selected");
    var roleName = node.options[node.selectedIndex].text;
    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("AjaxPart").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("POST", Ipaddress+"/Authority/UsersRoleChange", true);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.send("roleName=" + roleName);

    location.href = Ipaddress+"/Authority/UserManage";
}
function AddAuthority()
{
    var node = document.getElementById("selectedUrl");
    var url = node.options[node.selectedIndex].text;
    document.getElementById("urls").value = url;
}
function AuthorityUpdate(authorityName)
{
    location.href = Ipaddress + "/Authority/AuthorityUpdate?authorityName=" + authorityName;
}
function AuthorityDelete(authorityName)
{
    if (confirm("确认删除该权限吗"))
    location.href = Ipaddress + "/Authority/AuthorityDelete?authorityName=" + authorityName;
}