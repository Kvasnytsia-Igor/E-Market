function openNav() {
  document.getElementById("mySidebar").style.width = "100%";
  document.getElementById("main").style.marginLeft = "250px";
  document.getElementById("main").style.display = "none";
}
function closeNav() {
  document.getElementById("mySidebar").style.width = "0";
  document.getElementById("main").style.marginLeft = "0";
  document.getElementById("main").style.display = "block";
}
