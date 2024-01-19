import moment from "moment";

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
                    headerName: 'File name',
                    field: 'fileName',
                    minWidth: 200,
                    editable:false
                },
                {
                    headerName:"",
                    width: 15,
                    id:'download',
                    headerClass: "mdi mdi-file-download biggerFont flexCenter",
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
                    // tez dziala :D
                    // onCellClicked:()=>{
                    //     console.log('kutasorożec')},
                    cellClass:"mdi mdi-file-download biggerFont boldText",
                    valueGetter:params=>{
                        const _ =  params.data?.fileName.split('.')
                        return `.${_[_.length-1]}`
                    }
                },
                {
                    headerName: 'Employee',
                    field: 'employee',
                    minWidth: 200,
                    editable:false,
                    valueGetter:params=>{
                        if(params.data?.employee){
                            return `${params.data.employee?.firstName} ${params.data.employee?.lastName}`
                        }
                    }
                },
                {
                    headerName: 'Company',
                    field: 'company',
                    minWidth: 200,
                    editable:false,
                    valueGetter:params=>{
                        if(params.data?.company){
                            return `${params.data.company?.code}` + 'dasd'
                        }
                    }
                },
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