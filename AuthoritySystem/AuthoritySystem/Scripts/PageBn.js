var IPAddress = "http://localhost:5898/";

function CreateBn(MaxPage, appendId) {

    LastOrNextpageBn("glyphicon glyphicon-menu-left", 1, "last", appendId);
    if (MaxPage > 6) {
        for (var i = 1; i <= 6; i++) {
            SinglePageBn(i, appendId);
        }
        CreateNodeTool("button", "....", "dot", appendId);
        SinglePageBn(MaxPage, appendId);
    }
    else {
        for (var i = 1; i <= MaxPage; i++) {
            SinglePageBn(i, appendId);
        }
    }
    LastOrNextpageBn("glyphicon glyphicon-menu-right", 1, "next", appendId);
}


function SinglePage(value) {
    var intvalue = parseInt(value);
    var nodes = document.getElementsByClassName("pagebn");
    var maxpage = parseInt(nodes[nodes.length - 1].getAttribute("value"));
    var SmallerThanmax = parseInt(nodes[nodes.length - 2].getAttribute("value"));
    if (maxpage <= nodes.length);
    else {
        if (intvalue < nodes.length / 2) {
            document.getElementById("dot").style.display = "inline";
        }
        else if (intvalue >= nodes.length / 2 && intvalue < maxpage - nodes.length / 2 - 1) {
            document.getElementById("dot").style.display = "inline";

            for (var i = 0; i < nodes.length - 1; i++) {
                nodes[i].setAttribute("value", intvalue - parseInt((nodes.length - 1) / 2) + i);
                nodes[i].innerHTML = intvalue - parseInt((nodes.length - 1) / 2) + i;
            }
        }
        else {
            for (var i = 0; i < nodes.length; i++) {
                nodes[i].setAttribute("value", maxpage - nodes.length + 1 + i)
                nodes[i].innerHTML = maxpage - nodes.length + 1 + i;
            }
            document.getElementById("dot").style.display = "none";
        }
    }
    ColorChange(intvalue, nodes);
    document.getElementById("last").setAttribute("value", intvalue);
    document.getElementById("next").setAttribute("value", intvalue);

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("pageTable").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", IPAddress + "Home/PageChanging?page=" + intvalue, true);
    xmlhttp.send();
}
function LastOrNextPage(value, id) {
    var nodes = document.getElementsByClassName("pagebn");
    var smallestValue = parseInt(nodes[0].getAttribute("value"));




    var maxpage = parseInt(nodes[nodes.length - 1].getAttribute("value"));
    var smallerThanMax = parseInt(nodes[nodes.length - 2].getAttribute("value"));
    var intvalue = parseInt(value);
    if (id == "last") {
        if (intvalue > smallestValue);
        else {
            if (smallerThanMax == maxpage - 1)
                document.getElementById("dot").style.display = "inline";
            if (intvalue == 1);
            else {
                for (var i = 0; i < nodes.length - 1; i++) {
                    nodes[i].setAttribute("value", parseInt(nodes[i].getAttribute("value")) - 1);
                    var test = parseInt(nodes[i].getAttribute("value"));
                    nodes[i].innerHTML = parseInt(nodes[i].getAttribute("value"));
                }
            }
        }
        if (intvalue == 1);
        else
            intvalue--;
    }
    else {
        if (intvalue < smallerThanMax);
        else {
            if (smallerThanMax == maxpage - 2)
                document.getElementById("dot").style.display = "none";
            if (smallerThanMax != maxpage - 1) {
                for (var i = 0; i < nodes.length - 1; i++) {
                    nodes[i].setAttribute("value", parseInt(nodes[i].getAttribute("value")) + 1);
                    nodes[i].innerHTML = parseInt(nodes[i].getAttribute("value"));
                }
            }
        }
        if (intvalue == maxpage);
        else
            intvalue++;
    }
    ColorChange(intvalue, nodes);
    document.getElementById("last").setAttribute("value", intvalue);
    document.getElementById("next").setAttribute("value", intvalue);

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.status == 200 && xmlhttp.readyState == 4) {
            document.getElementById("pageTable").innerHTML = xmlhttp.responseText;
        }
    }
    xmlhttp.open("GET", IPAddress + "Home/PageChanging?page=" + intvalue, true);
    xmlhttp.send();
}
function ColorChange(pagebn, nodes) {
    for (var i = 0; i < nodes.length; i++) {
        if (parseInt(nodes[i].innerHTML) == pagebn) {
            nodes[i].style.color = "#033649";
            nodes[i].style.background = "#CDB380";
        }
        else {
            nodes[i].style.color = "";
            nodes[i].style.background = "";
        }

    }
}


function CreateNodeTool(type, text, idName, appendId) {
    para = document.createElement(type);
    textNode = document.createTextNode(text);
    para.setAttribute("id", idName);
    para.setAttribute("class", "btn btn-default");
    para.appendChild(textNode);
    document.getElementById(appendId).appendChild(para);
}
function SinglePageBn(text, appendId) {
    var para = document.createElement("button");
    var textNode = document.createTextNode(text);
    para.setAttribute("class", "pagebn btn btn-default");
    para.setAttribute("value", text);
    para.appendChild(textNode);
    para.onclick = function () {
        SinglePage(this.value);
    }
    document.getElementById(appendId).appendChild(para);
}
function LastOrNextpageBn(text, value, idName, appendId) {
    var para = document.createElement("button");
    var childpara = document.createElement("span");
    childpara.setAttribute("class", text);
    childpara.setAttribute("aria-hidden", true);
    para.setAttribute("value", value);
    para.setAttribute("id", idName);
    para.setAttribute("class", "btn btn-default")
    para.appendChild(childpara);
    para.onclick = function () {
        LastOrNextPage(this.value, this.id);
    }
    document.getElementById(appendId).appendChild(para);
}