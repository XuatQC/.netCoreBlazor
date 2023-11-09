function SetAllInputCheckBox(isTrue) {
    let a = document.querySelectorAll("input[type=checkbox]");
    console.log(isTrue);
    for (let index = 0; index < a.length; index++) {
        if (a[index].disabled != true) {
            a[index].checked = isTrue;
        }
    }
}
function CheckCheckboxByList(listOfCheckBox) {
    let listCheckBox = document.querySelectorAll("input[type=checkbox]");
    console.log(listOfCheckBox);
    listCheckBox.forEach((x) => {
        if (listOfCheckBox.includes(parseInt(x.id))) {
            console.log(x.id);
            document.getElementById(x.id).checked = true;
        }
    })
    
}