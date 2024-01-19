
import StageClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/StageClass";

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
        const stage = (await this.service.getById(id)).data
        return StageClass.toModel(stage, StageClass)
    }
    async create(stage){
        const invalid = stage.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        stage.id = null;
        return this.service.create(stage)
    }
    async update(stage){
        const invalid = stage.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(stage)
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
