const ddlTemplate = document.createElement('template');
ddlTemplate.innerHTML = `
<style>
/********************************************************************* details summary */
.my-ddl{
display: inline-block;
}
.ddlWrapper{
  height: 55px;
  width: fit-content;
  block-size: fit-content;
  overflow: visible;
  position: relative;
}
/*.ddlBinder{display:none;}*/
.ddl {
  background-color: white;
  border: 1px solid #aaa;
  border-radius: 4px;
  padding: 0.5em 0.5em 0;
  width: 300px;
  /* pointer-events: none;  prevents click events */ 
  user-select: none; /* prevents text selection */  
}
.ddl[open] {
    padding: 0.5em 0.5em;
}
.ddl>summary {
  font-weight: bold;
  margin: -0.5em -0.5em 0;
  padding: 1em;
}
.ddl>summary:hover {
  background-color: #ddd;
}
/*details > summary {
  list-style: none;
}*/
details[open] summary {
    border-bottom: 1px solid #aaa;
    margin-bottom: 0.5em;
}
/********************************************************************* ul li */
.dllList{
  min-height: 5opx;
  max-height: 350px;
  overflow:hidden; 
  overflow-y:scroll;
}
.dllList>data{
  display: flex;
  justify-content: space-between;  
  align-items: center;
  padding: 0.5em;
  margin-right: 7px;
  cursor: pointer;
  border-bottom: 1px solid #ddd;
}
.dllList>data:last-child {
  border-bottom: 0;
}
.dllList>data[selected]{
  background-color: #999;
  color: #fff;
}
.dllList>data:hover:not([selected]){
  background-color: #ddd;
}
/*********************************** Ctrl Buttons */
.editIco{
  float:right;
  margin: 2px;
  cursor: pointer;
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='balck' class='bi bi-pencil-square' viewBox='0 0 16 16'%3E%3Cpath d='M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z'/%3E%3Cpath fill-rule='evenodd' d='M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z'/%3E%3C/svg%3E");
}
.editIco:hover{
  /*fill: #94d31b; */
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='red' class='bi bi-pencil-square' viewBox='0 0 16 16'%3E%3Cpath d='M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z'/%3E%3Cpath fill-rule='evenodd' d='M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z'/%3E%3C/svg%3E");
}
.deleteIco{
  float:right;
  margin: 2px;
  cursor: pointer;
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='black' class='bi bi-trash3' viewBox='0 0 16 16'%3E%3Cpath d='M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z'/%3E%3C/svg%3E");
}
.deleteIco:hover{
  /*fill: #94d31b; */
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='red' class='bi bi-trash3' viewBox='0 0 16 16'%3E%3Cpath d='M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5ZM11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0H11Zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5h9.916Zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5Z'/%3E%3C/svg%3E");
}
.addIco{
  float:right;
  margin: 2px;
  cursor: pointer;
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='balck' class='bi bi-plus-circle' viewBox='0 0 16 16'%3E%3Cpath d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z'/%3E%3Cpath d='M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z'/%3E%3C/svg%3E");
}
.addIco:hover{
  /*fill: #94d31b; */
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='red' class='bi bi-plus-circle' viewBox='0 0 16 16'%3E%3Cpath d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14zm0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16z'/%3E%3Cpath d='M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4z'/%3E%3C/svg%3E");
}
/***************************************** Scrolling Style */


/* width */
.dllList::-webkit-scrollbar {
  width: 5px;
}

/* Track */
.dllList::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
.dllList::-webkit-scrollbar-thumb {
  background: #888; 
}

/* Handle on hover */
.dllList::-webkit-scrollbar-thumb:hover {
  background: #555; 
}
</style>
<div class="my-ddl">
<div class="ddlWrapper">
<details class="ddl">
  <summary>
    <span class="ddlBtn">please Select</span> <img class="addIco"/>
  </summary>
  <div class="dllList"> </div>
</details>
</div>
<div>
`;

let ddlSetting; let ddlData;
class MyDdl extends HTMLElement {
    constructor() {
        super();

        this.showInfo = true;

        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(ddlTemplate.content.cloneNode(true));

        ddlSetting = this.getAttribute('ddlSetting');
        ddlData = this.getAttribute('ddlData');
        ddlSetting = JSON.parse(ddlSetting);
        ddlData = JSON.parse(ddlData);
    }

    ddlItem(val, txt) {
        if (ddlSetting.mode == "edit") {
            return `<data value="${val}"> ${txt} <span class="ddlCtrl"> <img onclick="${setting.onDelete}" class="deleteIco" />  <img onclick="${setting.onSave}" class="editIco" /> </span> </data>`;
        } else { return `<data value="${val}"> ${txt} </data>`; };
    };


    connectedCallback() {

        if (ddlSetting.mode == 'read') { this.shadowRoot.querySelector(".addIco").remove(); }
        ddlData.forEach((itm) => {
            this.shadowRoot.querySelector(".dllList").innerHTML += this.ddlItem(itm.val, itm.txt);
        })

        this.shadowRoot.querySelector(".ddl").addEventListener("click", (e) => {
            if (e.target.tagName == "DATA") {
                if (this.shadowRoot.querySelector('.dllList>data[selected]') != null) { this.shadowRoot.querySelector('.dllList>data[selected]').removeAttribute('selected'); }
                e.target.setAttribute('selected', '');
                this.shadowRoot.querySelector(".ddlBtn").innerText = e.target.innerText;
            }
        });

    }

    disconnectedCallback() {
        this.shadowRoot.querySelector(".ddl").removeEventListener();
    }

}

window.customElements.define('my-ddl', MyDdl);