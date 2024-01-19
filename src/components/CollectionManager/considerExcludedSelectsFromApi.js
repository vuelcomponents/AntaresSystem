const considerExcludedSelectsFromApiDelete = (owner, collectionKey, collection,grid, setCollection)=>{
    // if(!owner.id  || owner?.id === 0){
    //         const cleanCollection = collection.filter(c=>!grid.selected.some(_c=>_c.id === c.id))
    //         setCollection(cleanCollection, collectionKey)
    //         grid.api.setRowData(cleanCollection)
    //         return true;
    // }
    return false;
}
const considerExcludedSelectsFromApiSelect = (owner, collectionKey, collection,grid, setCollection, select)=>{
    // if(!owner.id || owner?.id === 0){
    //         if( !collection.includes(select)){
    //             const newCollection = collection
    //             newCollection.push(select)
    //             setCollection(newCollection, collectionKey)
    //             grid.api.setRowData(newCollection)
    //         }
    //         return true;
    //     }
    return false;
}
export {considerExcludedSelectsFromApiSelect, considerExcludedSelectsFromApiDelete}