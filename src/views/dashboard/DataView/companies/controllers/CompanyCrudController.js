
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import toModel from "@/core/toModel";

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
        const company = (await this.service.getById(id)).data
        return CompanyClass.toModel(company, CompanyClass)
    }
    async create(company){
        const invalid = company.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        company.id = null;
        return this.service.create(company)
    }
    async update(company){
        const invalid = company.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(company)
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
