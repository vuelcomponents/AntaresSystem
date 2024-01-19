export default class {
    toast;
    message;
    icon;
    constructor(timeout = 1500){
        this.timeout = timeout
        const el = document.createElement('div')
        el.style.cssText = `
           min-width:350px;
           min-height:40px;
           padding-block:4px;
           font-size:0.8em;
           border-radius:2px;
           padding: 3px;
           position:fixed;
           left:20px;
           top:20px;
           z-index:1000;
           background:red;
           color:#fff;
           display:flex;
           padding-inline:5px;
           align-items:center; 
        `
        const span = document.createElement('span')
        const i = document.createElement('i')
        i.classList.add('mdi')
        i.style.marginRight='10px'
        this.icon = i ;
        el.appendChild(this.icon)
        this.message = span
        el.appendChild(this.message)
        this.toast = el;
    }

    error(message){
        this.icon.classList.add('mdi-alert-circle')
        this.toast.style.background='#e1143a'
        this.message.innerText = message
        document.body.appendChild(this.toast)
        this.clear('mdi-alert-circle')
    }
    info(message){
        this.icon.classList.add('mdi-information')
        this.toast.style.background='#1455e1'
        this.message.innerText = message
        document.body.appendChild(this.toast)
        this.clear('mdi-information')
    }
    warn(message){
        this.icon.classList.add('mdi-alert')
        this.toast.style.background='#FCAE1E'
        this.message.innerText = message
        document.body.appendChild(this.toast)
        this.clear('mdi-alert')
    }
    success(message){
        this.icon.classList.add('mdi-check')
        this.toast.style.background='#5dd73c'
        this.message.innerText = message
        document.body.appendChild(this.toast)
        this.clear('mdi-check')
    }
    clear(className){
        setTimeout(()=>{
            this.icon.classList.remove(className)
            this.message.innerText= ''
            this.toast.remove()
        }, this.timeout)
    }
}