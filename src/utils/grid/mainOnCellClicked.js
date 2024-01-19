export default (params, $)=>{
    switch(params.colDef.id){
        case "download":
            $.service.download(params.data.id, params.data.fileName)
            break;
    }

    if(params.colDef?.collectionKey){
        const collectionLock = params.colDef?.collectionLock
        if(collectionLock){
            switch(collectionLock){
                case "actionFunction":
                    if(params.data?.systemFunction?.code !== 'Mail'){
                        return;
                    }
            }
        }
        if($.life) {
            $.life.param = params.colDef.collectionKey;
            $.life.id = params.data.id;
            console.log('life', $.life)
        }
    }
}