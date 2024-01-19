import {Service} from "@/services/Service";

export class EmployeeService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'employee'
    }
    async advise(employee){
        return this._http.post(`${this.pathName}/advise`, employee)
    }
    async hire(employee){
        return this._http.post(`${this.pathName}/hire`, employee)
    }
    async fire(employee){
        return this._http.post(`${this.pathName}/fire`, employee)
    }
}