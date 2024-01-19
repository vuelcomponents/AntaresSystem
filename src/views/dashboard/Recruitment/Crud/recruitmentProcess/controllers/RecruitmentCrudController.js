import RecruitmentClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentClass";


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
        const recruitmentProcess = (await this.service.getById(id)).data
        return RecruitmentClass.toModel(recruitmentProcess, RecruitmentClass)
    }
    async create(recruitmentProcess){
        recruitmentProcess.id = null;
        if(recruitmentProcess.offer) {
            recruitmentProcess.offer = {id: recruitmentProcess.offer.id}
        }
        return this.service.create(recruitmentProcess)
    }
    async update(recruitmentProcess){
        const invalid = recruitmentProcess.validate({toast:this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        return this.service.update(recruitmentProcess)
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
