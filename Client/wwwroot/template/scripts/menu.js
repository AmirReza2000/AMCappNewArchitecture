/*let barsElem = document.querySelector('.bars')
let backBtn = document.querySelector('.back-button')
let hamburgerMenu = document.querySelector('.hamburger-menu')
let controllMenu = document.querySelector('.controll-bar')

barsElem.addEventListener('click', function () {
    hamburgerMenu.style.display = 'block'
})

backBtn.addEventListener('click', function () {
    hamburgerMenu.style.display = 'none'
})

// controllMenu.addEventListener('click', function (event) {
//     if (event.target.dataset.name == 'selectable') {
//         event.target.classList.toggle('selected-elem')
//     }
// })

controllMenu.addEventListener('click', function (event) {
    if (event.target.classList[0] == 'position') {
        event.target.classList.toggle('selected-elem')

        if (event.target.dataset.mode == 'off') {
            event.target.dataset.mode = 'on'
        } else {
            event.target.dataset.mode = 'off'
        }
    }
    console.log(event.target);
})*/
/*
console.log(document.querySelectorAll('button.position'))
console.log(document.getElementsByClassName("seatButton"))*/
/*var elems = document.getElementsByClassName("seatButton");*/
// let elems=document.querySelectorAll('button.position')
// for (var i = 0; i < elems.length; i++) {

//   elems[i].onclick = function(event) {
//     /*
//     var color = window.getComputedStyle(this, null)

//                 .getPropertyValue("background-color");
//     */
//     console.log(event)
//     this.classList.toggle('selected-elem')


//     /*
//     console.log(color)

//     if(color === 'rgb(25, 155, 25)')

//     {
//       console.log('سلام سفید')
//       this.classList.remove('selected-elem')
//       this.classList.add('buttonIndex')
//     }
//     else{
//       console.log(' سلام سبز')
//       this.classList.remove('buttonIndex')
//       this.classList.add('selected-elem')
//     }
//     */





//   };
// };
document.querySelectorAll('button.position')
    .forEach((button, index) => {

        button.addEventListener('click',
            () => {
                DotNet.invokMethodAsync('Client','thistast')
            }
        )
    })
function openFullScreen() {
    var elem = document.documentElement;
    if (elem.requestFullscreen) {
        elem.requestFullscreen();
    } else if (elem.mozRequestFullScreen) { /* Firefox */
        elem.mozRequestFullScreen();
    } else if (elem.webkitRequestFullscreen) { /* Chrome, Safari and Opera */
        elem.webkitRequestFullscreen();
    } else if (elem.msRequestFullscreen) { /* IE/Edge */
        elem.msRequestFullscreen();
    }
} function closeFullscreen() {
    if (document.exitFullscreen) {
        document.exitFullscreen();
    } else if (document.webkitExitFullscreen) { /* Safari */
        document.webkitExitFullscreen();
    } else if (document.msExitFullscreen) { /* IE11 */
        document.msExitFullscreen();
    }
}
function myFunction1() {
    document.querySelectorAll('button.position')
        .forEach((button, index) => {

            button.addEventListener('click',
                () => {
                    button.classList.toggle('selected-elem')
                }
            )
        })

}
function changeButtomStutus(id) {
    var button = document.querySelector(`button[id='${id}']`)
    button.classList.toggle('selected-elem')
    //DotNet.invokeMethod('Client', 'DifferentMethodName')
   }
window.registerEventListener = () => {
    //document.getElementsByClassName('tasti')[0].addEventListener('click',  () => {
    //    DotNet.invokeMethod('Client', 'DifferentMethodName')

    //})
    //document.querySelectorAll('button.position')
    //    .forEach((button, index) => {
    //        button.addEventListener('click',
    //            () => {
    //                button.classList.toggle('selected-elem')
    //            }
    //        )
    //    });
    //var buttons = window.document.querySelectorAll('button.position')
    //console.log(buttons.)
    //for (var i = 0; i < buttons.length; i++) {
    //    console.log(buttons[i])
    //}
}
var r = document.getElementsByClassName('button.position')
