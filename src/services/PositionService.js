import {Service} from "@/services/Service";

export class PositionService extends Service{
    constructor(emitter) {
        super(emitter);
        this.pathName = 'position'
    }

    async move(positionMove){
        return this._http.post(`${this.pathName}/move-move`, positionMove)
    }

}