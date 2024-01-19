import moment from "moment";
import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";

export default () => {
    return[
        {
            groupId: 'options',
            headerName: 'Options',

            children: [
                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                },
                {
                    headerName:"",
                    width: 15,
                    id:'goToUpdate',
                    headerClass: "mdi mdi-file-document biggerFont flexCenter",
                    cellClass:"mdi mdi-file-document biggerFont  active-update-cell-custom"
                },
                {
                    headerName: 'Status  ⥨',
                    cellRenderer:'StatusSwitchRenderer',
                    minWidth: 200,
                    field:'status',
                    storeModel:"Document",
                    editable:false,
                },

            ]
        },
        {
            headerName: 'Details',
            children: [
                {
                    headerName: 'Code',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Description',
                    field: 'description',
                    minWidth: 200,
                },
                {
                    headerName: 'Document type',
                    field: 'documentType',
                    owner:'document',
                    cellRenderer:'SelectionRenderer',
                    editable:false,
                    minWidth: 200,
                },
                {
                    headerName: 'Realisations',
                    collectionKey:'realisations',
                    valueGetter:params => params.data?.realisations?.length,
                    cellClass: `active-cell-custom`
                },
                {
                    headerName: 'File name',
                    field: 'fileName',
                    minWidth: 200,
                    editable:false
                },
                {
                    headerName:"",
                    width: 15,
                    id:'download',
                    headerClass: "mdi mdi-file-download biggerFont centerized",
                    cellStyle:(params)=>{
                        const _ =  params.data?.fileName.split('.')
                        const ext =  `.${_[_.length-1]}`
                        const imageRegex = /\.(jpg|jpeg|png|gif|bmp)$/i;
                        const pdfRegex = /\.(pdf)$/i;
                        var documentRegex = /\.(docx|doc|odt|txt|rtf|html|htm|xml|ppt|pptx|xls|xlsx|csv|json|yaml|md)$/i;
                        const style = {
                            color:'black',
                            cursor:'pointer',
                            textAlign:'left',

                        }
                        switch(true){
                            case imageRegex.test(ext):
                                style.color = 'green'
                                break;
                            case pdfRegex.test(ext):
                                style.color = 'red'
                                break;
                            case documentRegex.test(ext):
                                style.color = 'darkblue'
                                break;
                        }

                        return style;
                    },
                    cellClass:"mdi mdi-file-download boldText",
                    valueGetter:params=>{
                        const _ =  params.data?.fileName.split('.')
                        return `.${_[_.length-1]}`
                    }
                },
                {
                    collectionKey:'employees',
                    valueGetter:params => params.data?.employees?.length,
                    cellClass: `active-cell-custom`,
                    headerName: 'Employees',
                    field: 'employee',
                    minWidth: 200,
                    editable:false,
                },
                {
                    collectionKey:'companies',
                    valueGetter:params => params.data?.companies?.length,
                    cellClass: `active-cell-custom`,
                    headerName: 'Companies',
                    field: 'companies',
                    minWidth: 200,
                    editable:false,
                },
                // {
                //     headerName: 'Company',
                //     field: 'company',
                //     minWidth: 200,
                //     editable:false,
                //     owner:'document',
                //     cellRenderer:'SelectionRenderer',
                // },
                {
                    headerName: 'Date from',
                    field: 'dateFrom',
                    minWidth: 200,
                    cellRenderer: 'DateRenderer'
                },
                {
                    headerName: 'Date to',
                    field: 'dateTo',
                    minWidth: 200,
                    cellRenderer: 'DateRenderer'
                },
                {
                    headerName: 'Upload date',
                    field: 'uploadDate',
                    minWidth: 200,
                    editable:false,
                    valueGetter: params=>moment(params.data.uploadDate).format("DD/MM/YYYY")

                },
            ]
        },
    ]
}