import {Service} from "@/services/Service";

export class CompanyService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'company'
    }
    async advise(company){
        return this._http.post(`${this.pathName}/advise`, company)
    }
}