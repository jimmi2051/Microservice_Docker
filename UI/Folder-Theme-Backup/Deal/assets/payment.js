function clickShowFooter (a) {
    var x = document.getElementById(a)
    if (x.style.display == 'block') {
      x.style.display = 'none'
    }else {
      x.style.display = 'block'
    }
  }