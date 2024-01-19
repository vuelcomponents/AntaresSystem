import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";

export default (recruitment) => {
    return [
        {      groupId: 'Recruitment Process',
            headerName: 'Recruitment Process',
            children:[
                {
                    headerCheckboxSelection: true,
                    checkboxSelection: true,
                    showDisabledCheckboxes: true,
                    filter: false,
                    width: 55,
                    editable:false,
                },
                {
                    headerName: 'Last Name*',
                    field: 'lastName',
                    minWidth: 100,
                    editable:false,
                },
                {
                    headerName: 'Name*',
                    field: 'firstName',
                    minWidth: 100,
                    editable:false,

                },
                {
                    headerName: 'Personal ID',
                    field: 'pesel',
                    minWidth: 100,
                    editable:false,
                },
                {
                    headerName:"Document Stage",
                    minWidth: 180,
                    id:'documentStage',
                    valueGetter:params=>{
                        const valid =
                            params.data.documents.some(d=>
                                 recruitment.documentTypes.some(dt=>dt.id===d?.documentType?.id))
                        || !recruitment.documentTypes || recruitment.documentTypes.length<1
                        console.log('validyty', valid)
                        console.log('employee documents', params.data.documents)
                        console.log('recruitment document types', recruitment.documentTypes)
                    },
                    // headerClass: "mdi mdi-arrow-right biggerFont flexCenter",
                    cellClass:(params)=> {
                        const valid =
                            params.data.documents.some(d=>
                                recruitment.documentTypes.some(dt=>dt.id===d?.documentType?.id))
                            || !recruitment.documentTypes || recruitment.documentTypes.length<1
                        if(valid)return "mdi mdi-emoticon-happy-outline biggerFont active-update-cell-custom"
                        else return "mdi mdi-emoticon-sad-outline biggerFont active-update-cell-custom"
                    },
                    cellStyle:(params)=> {
                        const valid =
                            params.data.documents.some(d=>
                                recruitment.documentTypes.some(dt=>dt.id===d?.documentType?.id))
                            || !recruitment.documentTypes || recruitment.documentTypes.length<1
                        if(valid)return {background:'#60bb42!important'}
                        else return {background:'darkred!important'}
                    },
                    suppressSizeToFit: true,
                    editable:false,
                },
                {
                    headerName:"Matching",
                    minWidth: 180,
                    id:'matchingStage',
                    // headerClass: "mdi mdi-arrow-right biggerFont flexCenter",
                    // cellClass:"mdi mdi-arrow-right biggerFont active-update-cell-custom",
                    // cellStyle:()=> {return {background:'#60bb42!important'}},
                    valueGetter:params=> params.data.tempAdvise,
                    cellRenderer:'AdviseRowRenderer',
                    single:true,
                    suppressSizeToFit: true,
                    editable:false,
                },
                {
                    headerName:"Contact Stage",
                    minWidth: 180,
                    id:'contactStage',
                    // headerClass: "mdi mdi-arrow-right biggerFont flexCenter",
                    // headerClass: "mdi mdi-arrow-right biggerFont flexCenter",
                    cellClass:(params)=> {
                        const valid = false;
                        if(valid)return "mdi mdi-emoticon-happy-outline biggerFont active-update-cell-custom"
                        else return "mdi mdi-emoticon-sad-outline biggerFont active-update-cell-custom"
                    },
                    cellStyle:(params)=> {
                        const valid = false;
                        if(valid)return {background:'#60bb42!important'}
                        else return {background:'darkred!important'}
                    },
                    suppressSizeToFit: true,
                    editable:false,
                },
                // {
                //     headerName:"Decide Stage",
                //     minWidth: 180,
                //     id:'decideStage',
                //     // headerClass: "mdi mdi-arrow-right biggerFont flexCenter",
                //     cellClass:"mdi mdi-arrow-right biggerFont active-update-cell-custom",
                //     cellStyle:()=> {return {background:'#60bb42!important'}},
                //     suppressSizeToFit: true,
                //     editable:false,
                // },

                {
                    headerName: 'Email*',
                    field: 'email',
                    minWidth: 200,
                    editable:false,
                },
                {
                    headerName: 'Tax ID',
                    field: 'bsn',
                    minWidth: 200,
                    editable:false,
                },
                {
                    headerName: 'Status',
                    field: 'status.code',
                    minWidth: 200,
                    editable:false,
                },
                {
                    headerName:"",
                    width: 15,
                    id:'fire',
                    headerClass: "mdi mdi-cancel biggerFont flexCenter",
                    cellClass:"mdi mdi-cancel biggerFont active-update-cell-custom",
                    cellStyle:()=> {return {background:'darkred!important'}}
                },
                {
                    headerName:"",
                    width: 15,
                    id:'hire',
                    headerClass: "mdi mdi mdi-briefcase-outline biggerFont flexCenter",
                    cellClass:"mdi mdi mdi-briefcase-outline biggerFont active-update-cell-custom",
                    cellStyle:()=> {return {background:'darkgreen!important'}}
                },
            ],
        },

    ];
}