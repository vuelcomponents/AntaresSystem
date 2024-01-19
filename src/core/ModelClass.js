import arrayBufferToBase64 from "@/utils/functions/arrayBufferToBase64";
import moment from "moment/moment";

export default class {
    // służy do filtorwania inputow, glownie wystepujacych w popupach kolekcji CollectionManager
    $filters = {}
    // :disabled
    $lock = {}
    // :hidden
    $hide = {}
    // :hidden dla inputow glownych
    dates = []
    numeric = []
    collections = []
    textArea = []
    create = []
    select = []
    delete = []
    booleans = []
    update = []
    objects = []
    headers = {}
    text= []
    required = []
    files = []
    colors = []
    extensions = {
        image:'image/*',
        images:'image/*'
    }
    validate(options){
        let invalid = null;
        for(const required of this.required){
            if(!this[required]){
                if(!invalid){
                    invalid = []
                }
                invalid.push(required)
            }
        }
        if(invalid){
            if(options.toast) {
                options.toast.error(this.getHeaders(invalid, "string") + ' cannot be empty')
            }
        }
        return invalid;
    }
    getInputs(set, form, requiredOnly){
        var inputs = Object.keys(this).filter(key=>this[set].includes(key))
        if(requiredOnly){
            inputs = inputs.filter(i=> this.required.includes(i))
        }
        return inputs
    }
    setParam(param, selection){
        this[param] = selection
    }
    getHeaders(props, option){
        const headers = []
        props.forEach(p=>{
            const header = this.headers[p]
            if(header){
                headers.push(header)
            }
        })
        switch(option){
            case "string":
                return JSON.stringify(headers)
                    .replaceAll("[","")
                    .replaceAll("]","")
                    .replaceAll(",",", ")
            case "first":
                return headers[0]
        }
        return headers;
    }

    release(){
        const { text, headers,collections, collectionsServices, dates, objects, objectsServices, required, classList,
                $hide, $filters, $lock, numeric, create,  files,  select, update,
            ...release } = this;
        return release;
    }
    decycle(keys) {
        const obj = this.release();
        const seen = new WeakSet();
        const _ = JSON.parse(JSON.stringify(obj, function (key, val) {
            if (val != null && typeof val === "object") {
                if (seen.has(val)) {
                    return;
                }
                seen.add(val);
            }
            if(keys){
                for(const _key of keys){
                       if(_key === key){
                           return {id:val?.id};
                       }
                }
            }
            return val;
        }));
        this.files.forEach(file=>{
            if(this[file]){
                _[file] = this[file]
            }
        })
        return _;
    }
    setFile(event, param) {
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onloadend = () => {
            const base64String =  arrayBufferToBase64(reader.result);
            this[param] = base64String;
            this[param.replace('Data', 'Name')] = file.name
        };

        reader.readAsArrayBuffer(file);
    }
    getFileName(param){
        if(!param || typeof param !== "string"){
            return null;
        }
        return this[param.replace('Data', 'Name')]
    }

    static toModel(object, ModelClass){
        const model = new ModelClass();
        Object.entries(object).forEach(e=>{
        model[e[0]] = e[1]
    })
    return model;
    }

}