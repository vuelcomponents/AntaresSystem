import getAllRows from "@/utils/grid/getAllRows";
import defaultColDef from "@/utils/grid/defaultColDef";
import CompaniesColumns from "@/views/dashboard/DataView/companies/CompanyColumns";
import mainOnCellValueChanged from "@/utils/grid/mainOnCellValueChanged";

export default class {
    onCellValueChanged;
    onCellClicked;
    loadList;
    onSelectionChanged;
    service;


    constructor(events, functions, service){
        this.onCellValueChanged = events.onCellValueChanged
        this.onCellClicked = events.onCellClicked
        this.onSelectionChanged = events.onSelectionChanged
        this.loadList = functions.loadList
        this.service = service;

    }

    selected = []
    api = null
    searchText = ''
    getAllRows = () => {
        let rowData = [];
        this.api.forEachNode(node => rowData.push(node.data));
        return rowData;
    }
    deleteSelectedRows()
    {
        const data = this.getAllRows().filter(d=> !this.selected.some(r=>r.id === d.id))
        this.api.setRowData(data)
    }
    updateRowById(id, data){
        this.api.forEachNode(node => {
            if(node.data.id === id){
                node.setData(data)
            }
        });
    }
    selectedToIds(){
        const ids = []
        this.selected.forEach(s=> ids.push({id:s.id}))
        return ids;
    }
    search(){
        this.api.setQuickFilter(this.searchText);
    }
    options = {
        getRowStyle: (params) => {
            // Warunek dla parzystych i nieparzystych wierszy
            if (params.node.rowIndex % 2 === 0) {
                // Parzysty - czerwony
                return { background: '#0e091e',color:'#fff',borderBottom:'solid 1px #ffffff20' };
            } else {
                // Nieparzysty - niebieski
                return { background: '#110b26',color:'#fff',borderBottom:'solid 1px #ffffff20' };
            }
        },
        onGridReady: async params => {
            this.api = params.api;
            await this.loadList()
            console.log('grid data', this.getAllRows())
        },
        onCellValueChanged: (params) => {
            if(!params.data){
                return;
            }
            if(this.onCellValueChanged){
                this.onCellValueChanged(params);
                return;
            }
            if(this.service) {
                mainOnCellValueChanged(params,{service:this.service})
            }
        },
        onCellClicked: (params) => {
            this.onCellClicked(params)
        },
        onSelectionChanged: (params) => {
            this.selected = params.api.getSelectedRows();
            this.onSelectionChanged(params)
        },
        defaultColDef,
        sideBar: {
            toolPanels: ['filters'],
        },
        // getRowId:  (params) => {
        //     return params.id;
        // },

    }
    data = []


}