
const templateCheckbox = document.createElement('template');
templateCheckbox.innerHTML = `
<style>
    .checked {
        width: 30px; 
        height: 30px; 
        content: url("data:image/svg+xml,%3Csvg width='800px' height='800px' viewBox='0 0 24 24' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'%3E%3C!-- Uploaded to: SVG Repo, www.svgrepo.com, Generator: SVG Repo Mixer Tools --%3E%3Ctitle%3Eic_fluent_checkbox_checked_24_regular%3C/title%3E%3Cdesc%3ECreated with Sketch.%3C/desc%3E%3Cg id='🔍-Product-Icons' stroke='none' stroke-width='1' fill='none' fill-rule='evenodd'%3E%3Cg id='ic_fluent_checkbox_checked_24_regular' fill='%23212121' fill-rule='nonzero'%3E%3Cpath d='M18.25,3 C19.7687831,3 21,4.23121694 21,5.75 L21,18.25 C21,19.7687831 19.7687831,21 18.25,21 L5.75,21 C4.23121694,21 3,19.7687831 3,18.25 L3,5.75 C3,4.23121694 4.23121694,3 5.75,3 L18.25,3 Z M18.25,4.5 L5.75,4.5 C5.05964406,4.5 4.5,5.05964406 4.5,5.75 L4.5,18.25 C4.5,18.9403559 5.05964406,19.5 5.75,19.5 L18.25,19.5 C18.9403559,19.5 19.5,18.9403559 19.5,18.25 L19.5,5.75 C19.5,5.05964406 18.9403559,4.5 18.25,4.5 Z M10,14.4393398 L16.4696699,7.96966991 C16.7625631,7.6767767 17.2374369,7.6767767 17.5303301,7.96966991 C17.7965966,8.23593648 17.8208027,8.65260016 17.6029482,8.94621165 L17.5303301,9.03033009 L10.5303301,16.0303301 C10.2640635,16.2965966 9.84739984,16.3208027 9.55378835,16.1029482 L9.46966991,16.0303301 L6.46966991,13.0303301 C6.1767767,12.7374369 6.1767767,12.2625631 6.46966991,11.9696699 C6.73593648,11.7034034 7.15260016,11.6791973 7.44621165,11.8970518 L7.53033009,11.9696699 L10,14.4393398 L16.4696699,7.96966991 L10,14.4393398 Z' id='🎨Color'%3E%3C/path%3E%3C/g%3E%3C/g%3E%3C/svg%3E");
    }
    .unchecked {
        width: 30px;
        height: 30px;
        content: url("data:image/svg+xml,%3Csvg width='800px' height='800px' viewBox='0 0 24 24' version='1.1' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink'%3E%3C!-- Uploaded to: SVG Repo, www.svgrepo.com, Generator: SVG Repo Mixer Tools --%3E%3Ctitle%3Eic_fluent_checkbox_unchecked_24_regular%3C/title%3E%3Cdesc%3ECreated with Sketch.%3C/desc%3E%3Cg id='🔍-Product-Icons' stroke='none' stroke-width='1' fill='none' fill-rule='evenodd'%3E%3Cg id='ic_fluent_checkbox_unchecked_24_regular' fill='%23212121' fill-rule='nonzero'%3E%3Cpath d='M5.75,3 L18.25,3 C19.7687831,3 21,4.23121694 21,5.75 L21,18.25 C21,19.7687831 19.7687831,21 18.25,21 L5.75,21 C4.23121694,21 3,19.7687831 3,18.25 L3,5.75 C3,4.23121694 4.23121694,3 5.75,3 Z M5.75,4.5 C5.05964406,4.5 4.5,5.05964406 4.5,5.75 L4.5,18.25 C4.5,18.9403559 5.05964406,19.5 5.75,19.5 L18.25,19.5 C18.9403559,19.5 19.5,18.9403559 19.5,18.25 L19.5,5.75 C19.5,5.05964406 18.9403559,4.5 18.25,4.5 L5.75,4.5 Z' id='🎨Color'%3E%3C/path%3E%3C/g%3E%3C/g%3E%3C/svg%3E");
    }
    .checked:hover, .unchecked:hover {
        transform:scale(1.1);
    }
    [disabled] {
        background-color: #ddd;
        cursor: no-drop;
    }
</style>
`;

let MyCheckboxValue;
class MyCheckbox extends HTMLElement {

    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(templateCheckbox.content.cloneNode(true));

        this.img = document.createElement('img');
        this.input = document.createElement('input');
        // ----------------------------------------- img
        this.img.setAttribute('id', 'checkBox');
        this.shadowRoot.appendChild(this.input);
        this.shadowRoot.appendChild(this.img);
        MyCheckboxValue = this.getAttribute('checked');
        this.img.className = MyCheckboxValue == 'true' ? 'checked' : 'unchecked';
        this.img.setAttribute(this.hasAttribute('disabled') ? 'disabled' : null, null);
        // ----------------------------------------- input
        this.input.setAttribute('type', 'text');
        this.input.setAttribute('id', 'checkBoxBinder');
        this.input.setAttribute('hidden', '');
        this.input.name = this.getAttribute('name');
        this.input.value = MyCheckboxValue;


        if (!this.hasAttribute('disabled')) {
            this.img.addEventListener('click', (event) => {
                event.target.classList.toggle("checked");
                event.target.classList.toggle("unchecked");
                MyCheckboxValue = event.target.classList.contains('checked') ? true : false;
                this.input.value = MyCheckboxValue;
            })
        }

        this._form = null;
        this._handleFormData = this.handleFormData.bind(this);
    }

    handleFormData({ formData }) {
        if (!this.img.hasAttribute('disabled')) {
            formData.append(this.input.name, this.input.value);
        }
    }

    connectedCallback() {
        this._form = this.findContainingForm();
        if (this._form) {
            this._form.addEventListener('formdata', this._handleFormData);
        }
    }

    disconnectedCallback() {
        if (this._form) {
            this.img.removeEventListener('click', null);
            this._form.removeEventListener('formdata', this._handleFormData);
            this._form = null;
        }
    }

    findContainingForm() {
        const root = this.getRootNode();
        const forms = Array.from(root.querySelectorAll('form'));
        return forms.find((form) => form.contains(this)) || null;
    }
}

window.customElements.define('my-checkbox', MyCheckbox);


// usage
// <my-checkbox checked='true' name='x' disabled='true'></my-checkbox>
// <my-checkbox checked='false' name='y' disabled='false'></my-checkbox>
// <my-checkbox checked='true' name='z' disabled='false'></my-checkbox>
