import ModelClass from "@/core/ModelClass";


export default class extends ModelClass{
    id;
    code;
    description;
    storeModel;
    transitionTo;
    priority;
    color = "56bd7f";
    statusActions;
    headers = {
        id: "Id",
        code:"Code",
        description:'Description',
        storeModel:"Destiny",
        transitionTo:"Transition to",
        priority:"Priority",
        color:"Color",
        statusActions:"Actions"
    }
    colors = [
        'color'
    ]
    text= [
        'code',
        'description',
    ]
    required = [
        'code',
        'storeModel'
    ]
    dates = [

    ]
    numeric = [
        'priority'
    ]
    files = [
    ]
    collections = [
        'statusActions',
        'transitionTo'
    ]
    create = [
        'statusActions'
    ]
    select = [
        'transitionTo'
    ]
    delete = [
        'statusActions',
        'transitionTo'
    ]
    update = [
    ]
    objects = [
        'storeModel',
    ]
    $hide = {
        storeModel:()=>{
            return this.id
        },
        statusActions: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
        transitionTo: (options)=>{
            if(!this.id){
                return true;
            }
            if(this.id === 0){
                return true;
            }
            return false;
        },
    }
    $filters = {
        transitionTo:(model,collection, owner)=>{
            if(!owner){
                return;
            }
            return collection.filter(tt=>{
                if(tt.id === owner.id){
                    return false;
                }
                if(tt.storeModel.id === owner.storeModel?.id){
                    return true;
                }
                return false;
            })
        }
    }

}