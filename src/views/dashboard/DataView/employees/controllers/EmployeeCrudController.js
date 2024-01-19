import moment from "moment";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
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
        const employee = (await this.service.getById(id)).data;
        const emp =  EmployeeClass.toModel(employee, EmployeeClass)
        return emp;
    }
    async create(employee){
        employee.id = null;
        return this.service.create(employee)
    }
    async update(employee){
        return this.service.update(employee)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(employees){
        return this.service.deleteMultiple(employees)
    }

    async saveEmployeeChanges(){
        console.log('save changes')
    }
}
