
import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import toModel from "@/core/toModel";
import MailClass from "@/views/dashboard/DataView/mails/MailClass";

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
        const mail = (await this.service.getById(id)).data
        return MailClass.toModel(mail, MailClass)
    }
    async create(mail){
        mail.id = null;
        return this.service.create(mail)
    }
    async update(mail){
        const invalid = mail.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(mail)
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
