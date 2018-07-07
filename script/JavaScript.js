//פונקציה להפעלת הכפתורים של ביטול ושמירת שם וקטגוריה
//פונקציה זו גם מפעילה פונקציה אחרת המעדכנת את מוני התווים בשדות האלו
function SetButtonsActive() {
    //document.getElementById("item_name_back_btn").classList.add("cancleBtnActive");
   // document.getElementById("item_name_save_btn").classList.add("cancleBtnActive");
    
   // updateCharCount('item_name_tb', 'item_name_tb_Counter', 43.5, 37,'item_name_back_btn');
    //updateCharCount('item_category_tb', 'item_category_tb_Counter', 71.7, 65,'item_name_back_btn');
}

//פונקציה המעדכנת מוני תווים
function updateCharCount(SourceTbxId , CounterId, regRight, maxRight, btnID) {
    var CharCounter = document.getElementById(SourceTbxId).value.length;
    if (CharCounter<3) {
        document.getElementById(CounterId).innerHTML = "יש להזין לפחות 3 תווים";
        document.getElementById(CounterId).style.right = maxRight + "%";
        document.getElementById(btnID).style.opacity = 0;
        document.getElementById(CounterId).style.color = "red";
        document.getElementById(btnID).disabled = true;
    }
    else if (CharCounter == 30) {
        document.getElementById(CounterId).innerHTML = "הוזן מספר התווים המירבי";
        document.getElementById(CounterId).style.color = "red";
        document.getElementById(CounterId).style.right = maxRight + "%";
        document.getElementById(btnID).style.opacity = 1;
        document.getElementById(btnID).disabled = false;
    }
    else {
        document.getElementById(CounterId).innerHTML = CharCounter + "/30";
        document.getElementById(CounterId).style.right = regRight + "%";
        document.getElementById(btnID).style.opacity = 1;
        document.getElementById(btnID).disabled = false;
        document.getElementById(CounterId).style.color = "black";
    }

    if (document.getElementById("item_name_tb").value.length < 3 || document.getElementById("item_category_tb").value.length < 3) {
        document.getElementById("item_name_save_btn").style.opacity = 0;
        document.getElementById("item_name_save_btn").disabled = true;
    }
    if (SourceTbxId == "item_name_tb" || SourceTbxId == "item_category_tb") {
        document.getElementById("item_name_back_btn").style.opacity = 1;
        document.getElementById("item_name_back_btn").disabled = false;
    }
    //if (SourceTbxId == "newGameTxb" && CharCounter > 2) {
    //    document.getElementById("addGameBtn").style.opacity = 1;
    //    document.getElementById("addGameBtn").disabled = false;
   // }
   // else if(SourceTbxId == "newGameTxb" && CharCounter < 3){
   //     document.getElementById("addGameBtn").style.opacity = 0;
   //     document.getElementById("addGameBtn").disabled = true;
   // }
    
}
//פונקציה לשינוי סטטוס החשיפה של חלון האודות
function changeInfoPopup(doThat) {
    document.getElementById("infoPopupDarkScreen").style.display = doThat;
}

//פונקציה לשינוי סטטוס החשיפה של חלון הוראות
function changeinstructionPopup(doThat) {
       
    document.getElementById("instructionPopupDarkScreen").style.display = doThat;
}
function TogglePopup(popupID, displayState) {
    document.getElementById(popupID).style.display = displayState;
}

//פונקציה לשינוי תמונה בחלון הוראות
/*function changeInstruction(direction) {
   if (direction == "next") {
       if (instructionindex == 6) {
          instructionindex = 1;
      }
       else {
           instructionindex++;
        } 
        document.getElementById("instructionPopupImage").src = "images/instruction_" + instructionindex + ".jpg"
    }
    else if (direction == "prev") {
        if (instructionindex == 1) {
            instructionindex = 6;
        }
        else {
            instructionindex--;
        }
        document.getElementById("instructionPopupImage").src = "images/instruction_" + instructionindex + ".jpg"
    }
    
}*/

//function showCoords(event) {
//    var x = event.clientX;
//    var y = event.clientY;
 //   var coords = "X coords: " + x + ", Y coords: " + y;
 //   alert(coords);
//}

function MagImgOver(itemId) {
    
    var elemnt = document.getElementById(itemId);
    elemnt.classList.toggle("bigphoto");
    
}
