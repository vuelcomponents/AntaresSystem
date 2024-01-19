export default class {
    ownerName;
    owner;
    object;
    identifier
    constructor( ownerName, ownerId, object , identifier){
        this.ownerName = ownerName;
        this.owner = {id:ownerId}
        this.object = object;
        this.identifier = identifier
    }
}