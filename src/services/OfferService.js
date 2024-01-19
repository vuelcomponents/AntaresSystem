import {Service} from "@/services/Service";

export class OfferService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'offer'
    }
}