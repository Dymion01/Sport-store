"use strict";

var x = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

x.on("count",function (count) {
    var target = document.getElementById("counter");
    target.innerHTML = count+"elo";
});
x.start().catch(function (err) {
    return console.error(err.toString());
});