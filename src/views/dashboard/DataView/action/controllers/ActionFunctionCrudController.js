
import ActionFunctionClass from "@/views/dashboard/DataView/action/ActionFunctionClass";

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
        const actionFunction = (await this.service.getById(id)).data;
        const acf =  ActionFunctionClass.toModel(actionFunction, ActionFunctionClass)
        return acf;
    }
    async create(actionFunction){
        actionFunction.id = null;
        return this.service.create(actionFunction)
    }
    async update(actionFunction){
        return this.service.update(actionFunction)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(actionFunctions){
        return this.service.deleteMultiple(actionFunctions)
    }

    async saveEmployeeChanges(){
        console.log('save changes')
    }
}
