import moment from "moment";
import StatusClass from "@/views/dashboard/DataView/status/StatusClass";
import toModel from "@/core/toModel";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";

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
        return StatusClass.toModel(employee, StatusClass)
    }
    async create(employee){
        return this.service.create(employee)
    }
    async update(employee){
        const invalid = employee.validate({toast: this.options.toast})
        if(invalid){
            return {
                invalid
            };
        }
        employee.dateOfBirth = moment(employee.dateOfBirth).format('DD/MM/YYYY')
        return this.service.update(employee)
    }
    async delete(id){
        return this.service.delete(id)
    }
    async deleteMultiple(employees){
        return this.service.deleteMultiple(employees)
    }

    async saveStatusChanges(){
        console.log('save changes')
    }
}
