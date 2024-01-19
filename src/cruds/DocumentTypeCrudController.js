
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import toModel from "@/core/toModel";
import DocumentTypeClass from "@/views/dashboard/DataView/documentTypes/DocumentTypeClass";

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
        const documentType = (await this.service.getById(id)).data
        return CompanyClass.toModel(documentType, DocumentTypeClass)
    }
    async create(documentType){
        const invalid = documentType.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        documentType.id = null;
        return this.service.create(documentType)
    }
    async update(documentType){
        const invalid = documentType.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(documentType)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(companies){
        return this.service.deleteMultiple(companies)
    }

    async saveChanges(){
        console.log('save changes')
    }
}
