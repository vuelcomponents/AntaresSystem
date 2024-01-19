import {Service} from "@/services/Service";

export class RecruitmentService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'recruitment'
    }
    async advance(params){
        return await this._http.post(`${this.pathName}/advance`, params.data)
    }
}