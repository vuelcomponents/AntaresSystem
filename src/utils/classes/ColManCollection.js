export default class {
    ownerName;
    owner;
    collection;
    identifier;
    constructor( ownerName, ownerId, collection, identifier ){
        this.ownerName = ownerName;
        this.owner = {id:ownerId}
        this.collection = collection;
        this.identifier = identifier
    }
}