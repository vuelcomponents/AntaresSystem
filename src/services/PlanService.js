import {Service} from "@/services/Service";

export class PlanService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'plan'
    }
    async getPlanToEmployee(employee){
        return this._http.get(`${this.pathName}/get-to-employee/${employee.id}`);
    }
}