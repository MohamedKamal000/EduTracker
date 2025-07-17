

const UserInput = document.getElementById("UserInput");
const SearchType = document.getElementById("searchType");
const Container = document.getElementById("StudentsContainer");
const SearchBtn = document.getElementById("Search-btn");

const NextBtn = document.getElementById("Next_Btn");
const PrevBtn = document.getElementById("Prev_Btn");


const NextItem = document.getElementById("Next");
const PrevItem = document.getElementById("Prev");

let StudentsCardArray;


let currentIndex = 0;
 

async function Search(e){
    e.preventDefault();
    
    let request = new Request
    (`/Student/SubmitSearch?SearchType=${SearchType.value}&Input=${UserInput.value}`
    ,{
        method: 'GET'
         }
    )
    
    let response = await fetch(request);

    let data = await response.json();

    InitializeCardsArray(data);
}


function InitializeCardsArray(data){
    let size = data.length;
    let MAX_CARDS_NUMBER = 5;
    let flooredSize = Math.floor(size / MAX_CARDS_NUMBER);
    if (size === 0){
        MakeEmpty();
        return;
    }
    
    
    let reminder = size % MAX_CARDS_NUMBER; // [ 0 -> 4 ]

    StudentsCardArray = new Array(flooredSize + (reminder !== 0 ? 1 : 0));
    
    for (let i = 0 ; i < flooredSize ; i++){
        let subArray = [];
        for (let j = 0 ; j < MAX_CARDS_NUMBER ; j++){
            subArray.push(CreateStudentRow(data[j + (i * MAX_CARDS_NUMBER)]));
        }
        
        StudentsCardArray[i] = subArray;
    }
    
    
    if (reminder !== 0 ){
        let subArray = [];
        for (let j = flooredSize ; j < flooredSize + reminder ; j++){
            subArray.push(CreateStudentRow(data[j]));
        }
        
        StudentsCardArray[StudentsCardArray.length - 1] = subArray;
    }
    
    currentIndex = 0;
    ShowGroupOfIndex(0);
}


function ShowGroupOfIndex(index){
    Container.innerHTML = "";
    
    for (let std in StudentsCardArray[index]){
        Container.appendChild(StudentsCardArray[index][std]);
    }
    AdjustButtons();
}


function GoNext(){
    let nextIndex = currentIndex + 1;
    
    if (nextIndex >= StudentsCardArray.length){
        return;
    }
    
    currentIndex++;
    ShowGroupOfIndex(nextIndex);
    AdjustButtons();
}



function GoPrevious(){
    let nextIndex = currentIndex - 1;

    if (nextIndex < 0){
        return;
    }


    currentIndex--;
    ShowGroupOfIndex(nextIndex);
    AdjustButtons();
}


function AdjustButtons(){
    if (currentIndex < StudentsCardArray.length - 1 && currentIndex > 0){
        NextItem.className = "page-item";
        PrevItem.className = "page-item";
    }
    
    else if (currentIndex === 0){
        NextItem.className = "page-item";
        PrevItem.className = "page-item disabled";
    }
    
    else {
        NextItem.className = "page-item disabled";
        PrevItem.className = "page-item";
    }
}

function MakeEmpty(){
    StudentsCardArray = [];
    Container.innerHTML = "";
    NextItem.className = "page-item disabled";
    PrevItem.className = "page-item disabled";
    currentIndex = 0;
}


function  CreateStudentRow(std){
    const a = document.createElement("a");
    a.href = `/Student/Details/${std["id"]}`;
    a.className = "list-group-item list-group-item-action";
    a.textContent = `Name: ${std["studentName"]}, Id: ${std["studentId"]}`;
    return a;
}


SearchBtn.onclick = Search;
NextBtn.onclick = GoNext;
PrevBtn.onclick = GoPrevious;