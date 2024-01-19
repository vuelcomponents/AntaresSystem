import ModelClass from "@/core/ModelClass";

export default class extends ModelClass{
    id;
    typeName;
    headers = {
        id: "Id",
        typeName:"Type name"
    }
    text= [
        'typeName'
    ]
    numeric = []
    required = []
    dates = []
    collections = []
    objects = []
    create = []
    select = []
    delete = []
    update = []
}