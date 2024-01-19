
import toModel from "@/core/toModel";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";
import moment from "moment/moment";
import ModelClass from "@/core/ModelClass";

export default class {
    service = undefined;
    options = null;
    constructor(service, options) {
        this.service = service;
        this.options = options;
    }

    async getAll(){
        return this.service.getAll()
    }
    async getById(id){
        const document = (await this.service.getById(id)).data;
        return DocumentClass.toModel(document, DocumentClass)
    }
    async create(document){
        const invalid = document.validate({toast: this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        document.id = null;
        return this.service.create(document)
    }
    async update(document){
        const invalid = document.validate({toast: this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(document)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(documents){
        return this.service.deleteMultiple(documents)
    }

    async saveChanges(){
        console.log('save changes')
    }
}
