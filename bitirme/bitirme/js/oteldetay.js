const rippleElements = document.getElementsByClassName("hotel__cta-button");

for(let i = 0; i < rippleElements.length; i++) {
  rippleElements[i].onclick = function(e){
    let X = e.pageX - this.offsetLeft;
    let Y = e.pageY - this.offsetTop;
    let rippleDiv = document.createElement("div");
    rippleDiv.classList.add('ripple');
    rippleDiv.setAttribute("style","top:"+Y+"px; left:"+X+"px;");
    this.appendChild(rippleDiv);
    setTimeout(function(){
      rippleDiv.parentElement.removeChild(rippleDiv);
    }, 900);
  }
}