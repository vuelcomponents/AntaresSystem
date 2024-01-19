import activeColumn from "@/utils/grid/activeColumn";
import doubleValueSetter from "@/utils/grid/doubleValueSetter";

export default () => {
    return [
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
                    headerName: "",
                    width: 15,
                    id: 'goToUpdate',
                    headerClass: "mdi mdi-atom-variant biggerFont flexCenter",
                    cellClass: "mdi mdi-atom-variant biggerFont  active-update-cell-custom"
                }
            ]
        },
        {

            headerName: 'Details',
            children: [
                {
                    headerName: 'Code*',
                    field: 'code',
                    minWidth: 200,

                },
                {
                    headerName: 'Description*',
                    field: 'description',
                    minWidth: 200,
                },
                {
                    headerName: 'Model',
                    field: 'storeModel',
                    valueGetter:params=> params.data?.storeModel?.code,
                    minWidth: 200,
                    editable:false,
                },
                {
                    headerName: 'Variant Type',
                    field: 'variantType.code',
                    minWidth: 200,
                    editable:false,

                },
                {
                    headerName: 'Variant Realisations',
                    field: 'variantRealisations',
                    minWidth: 200,
                    collectionKey:'variantRealisations',
                    cellClass:'active-update-cell-custom',
                    editable:false,
                    valueGetter: params =>{
                        if(params.data?.variantType?.id === 3) {
                            return params.data?.variantRealisations?.length
                        }else{
                            return 'N/A'
                        }
                    }
                }

            ]
        }
    ]
}


