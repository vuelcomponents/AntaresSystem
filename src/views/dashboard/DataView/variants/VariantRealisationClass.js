import ModelClass from "@/core/ModelClass";

export default class extends ModelClass{
    code ='';
    description ='';
    variant = null;
    text=[
        'code',
        'description'
    ]
    numeric = []
    required=[
        'code',
        'description'
    ]
    headers= {
        code: 'Code',
        description:'Description'
    }
    objects = [

    ]
    collections=[]
    create = []
    select = []
    delete = []
    update = []
    dates=[]

}