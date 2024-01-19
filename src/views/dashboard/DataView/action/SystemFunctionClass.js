import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    code;
    storeModels;
    systemFunction;
    headers = {
        id: "Id",
        code:"Code",
    }
    colors = [
    ]
    text= [
        'code',
    ]
    required = [
    ]
    dates = [

    ]
    numeric = [
    ]
    files = [
    ]
    collections = [
        'storeModels'
    ]
    create = [
    ]
    select = [
        'storeModels'
    ]
    delete = [
    ]
    update = [
    ]
    objects = [
    ]
    $hide = {
    }
    $filters = {
    }

}