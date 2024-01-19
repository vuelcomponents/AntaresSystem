export default (object, ModelClass)=>{
    const model = new ModelClass();
    Object.entries(object).forEach(e=>{
        model[e[0]] = e[1]
    })
    return model;
}