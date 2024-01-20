import {Service} from "@/services/Service";

export class RecruitmentService extends Service{
    constructor(emitter) {
        super(emitter);
    }
    async register(employee){
        return await this._http.post("employee/register")
    }
}