import {Service} from "@/services/Service";

export class VariantRealisationService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'variant-realisation'
    }

    async getAllByVariantId(variantId){
        return this._http.get(`${this.pathName}/get-by-variant-id/${variantId}`)
    }
}