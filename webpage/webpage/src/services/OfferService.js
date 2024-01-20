import {Service} from "@/services/Service";

export class OfferService extends Service{
    constructor(emitter) {
        super(emitter);
    }
    async getAll(){
        return await this._http.get('offer');
    }

}