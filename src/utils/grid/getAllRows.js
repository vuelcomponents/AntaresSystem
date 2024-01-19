export default (api) => {
    if(api) {
        let rowData = [];
        api.forEachNode(node => rowData.push(node.data));
        return rowData;
    }
}