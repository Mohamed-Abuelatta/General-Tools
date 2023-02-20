const imgTemplate = document.createElement('template');
imgTemplate.innerHTML = `
<style>
#my-img{
display: inline-block;
}
#imgContainer {
  position: relative;
  width: fit-content;
  block-size: fit-content;
  margin: 50px auto;
  width: fit-content;
  block-size: fit-content;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
}
#imagePreview {
  background-repeat: no-repeat;
  background-position: center;
  background-size: 100%;
  background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' height='300px' width='300px' version='1.1' viewBox='-300 -300 600 600' font-family='Bitstream Vera Sans,Liberation Sans, Arial, sans-serif' font-size='72' text-anchor='middle'%3E%3Ccircle stroke='%23AAA' stroke-width='10' r='280' fill='%23FFF'/%3E%3Cswitch style='fill:%23444;'%3E%3Ctext id='trsvg3-bn' systemLanguage='bn'%3E%3Ctspan x='0' y='-8' id='trsvg1-bn'%3E&%23x99B;&%23x9AC;&%23x9BF; &%23x989;&%23x9AA;&%23x9B2;&%23x9AD;&%23x9CD;&%23x9AF;%3C/tspan%3E%3Ctspan x='0' y='80' id='trsvg2-bn'%3E&%23x9A8;&%23x9DF;%3C/tspan%3E%3C/text%3E%3Ctext id='trsvg3-id' systemLanguage='id'%3E%3Ctspan x='0' y='-8' id='trsvg1-id'%3EGAMBAR TAK%3C/tspan%3E%3Ctspan x='0' y='80' id='trsvg2-id'%3ETERSEDIA%3C/tspan%3E%3C/text%3E%3Ctext id='trsvg3-en' systemLanguage='en'%3E%3Ctspan x='0' y='-8' id='trsvg1-en'%3ENO IMAGE%3C/tspan%3E%3Ctspan x='0' y='80' id='trsvg2-en'%3EAVAILABLE%3C/tspan%3E%3C/text%3E%3Ctext id='trsvg3'%3E%3Ctspan x='0' y='-8' id='trsvg1'%3ENO IMAGE%3C/tspan%3E%3Ctspan x='0' y='80' id='trsvg2'%3EAVAILABLE%3C/tspan%3E%3C/text%3E%3C/switch%3E%3C/svg%3E");
}

#imgButton {
  position: absolute;
  box-shadow:0 0 0 200vmax rgba(0,0,0,.5);
  cursor: pointer;
  text-align: center;
  opacity: 0;
  transition: opacity .35s ease;
  width: 80%;
  padding: 10px 20px;
  text-align: center;
  color: white;
  background-color: white;
  border: solid 2px white;
  z-index: 1;
}
#imgButton::before {
  content: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='40' height='40' fill='black' class='bi bi-cloud-arrow-up' viewBox='0 0 16 16'%3E%3Cpath fill-rule='evenodd' d='M7.646 5.146a.5.5 0 0 1 .708 0l2 2a.5.5 0 0 1-.708.708L8.5 6.707V10.5a.5.5 0 0 1-1 0V6.707L6.354 7.854a.5.5 0 1 1-.708-.708l2-2z'/%3E%3Cpath d='M4.406 3.342A5.53 5.53 0 0 1 8 2c2.69 0 4.923 2 5.166 4.579C14.758 6.804 16 8.137 16 9.773 16 11.569 14.502 13 12.687 13H3.781C1.708 13 0 11.366 0 9.318c0-1.763 1.266-3.223 2.942-3.593.143-.863.698-1.723 1.464-2.383zm.653.757c-.757.653-1.153 1.44-1.153 2.056v.448l-.445.049C2.064 6.805 1 7.952 1 9.318 1 10.785 2.23 12 3.781 12h8.906C13.98 12 15 10.988 15 9.773c0-1.216-1.02-2.228-2.313-2.228h-.5v-.5C12.188 4.825 10.328 3 8 3a4.53 4.53 0 0 0-2.941 1.1z'/%3E%3C/svg%3E");
}
#imgContainer:hover #imgButton {
  opacity: 0.6;
}
</style>
<div id="my-img">
<div id="imgContainer">
  <img id="imagePreview" src="" />
  <button id="imgButton"></button>
  <input id="imgUploader" type="file" hidden />
</div>
</div>
`;

let noImgSize; let width;
class MyImg extends HTMLElement {
    constructor() {
        super();
        this.showInfo = true;

        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(imgTemplate.content.cloneNode(true));
        this.shadowRoot.querySelector('#imagePreview').src = this.getAttribute('src');
        width = this.hasAttribute('width') ? this.getAttribute('width') == '' ? 200 : this.getAttribute('width') : 200;
        this.shadowRoot.querySelector('#imagePreview').width = width;
        noImgSize = this.hasAttribute('noImgSize') ? this.getAttribute('noImgSize') == '' ? 200 : this.getAttribute('noImgSize') : 200;
        this.shadowRoot.querySelector('#imagePreview').onerror = this.noImg(noImgSize);
    }

    noImg(errSize) {
        this.shadowRoot.querySelector('#imagePreview').height = errSize;
        this.shadowRoot.querySelector('#imagePreview').width = errSize;
    }

    connectedCallback() {
        let imagePreview = this.shadowRoot.querySelector('#imagePreview');
        let imgButton = this.shadowRoot.querySelector("#imgButton");
        let imgUploader = this.shadowRoot.querySelector("#imgUploader");

        imgButton.addEventListener('click', () => {
            imgUploader.click();
        });
        imgUploader.addEventListener('change', (e) => {
            let file = e.target.files[0];
            let reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = function () {
                imagePreview.src = reader.result;
            }
        });
    }

    disconnectedCallback() {
        this.shadowRoot.querySelector("#imgButton").removeEventListener();
        this.shadowRoot.querySelector("#imgUploader").removeEventListener();
    }
}

window.customElements.define('my-img', MyImg);