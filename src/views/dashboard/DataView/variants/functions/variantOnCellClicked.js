export default (params, $) =>{
    switch(params.colDef?.id){
        case "goToUpdate":
            $.router.push({name:'VariantUpdate', params:{id:params.data.id}})
            break;
    }
    if(params.colDef?.collectionKey && params.data?.variantType?.id === 3){
        if($.life) {
            $.life.param = params.colDef.collectionKey;
            $.life.id = params.data.id;
            console.log('life', $.life)
        }
    }
}