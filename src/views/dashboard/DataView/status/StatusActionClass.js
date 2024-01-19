import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    actionFunction;
    statusActionTrigger;
    headers = {
        id: "Id",
        description:'Description',
        statusActionTrigger:'Trigger',
        actionFunction:"Function",
    }
    colors = [

    ]
    text= [
    ]
    required = [
        'actionFunction',
        'statusActionTrigger',

    ]
    dates = [
    ]
    numeric = [
    ]
    files = [
    ]
    collections = [
    ]
    create = [
    ]
    select = [

    ]
    delete = [
    ]
    update = [
    ]
    objects = [
        'statusActionTrigger',
        'actionFunction',
    ]
    $hide = {

    }
    $filters = {
        'actionFunction':(instance, functions, owner)=>{
            if(!functions){
                return functions
            }
            return functions.filter(v=>v.storeModel?.id === owner.storeModel?.id)
        },

    }

}