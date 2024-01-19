import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";
import toLocaleStr from "@/utils/functions/toLocaleStr";

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
                headerName: "",
                width: 15,
                id: 'goToUpdate',
                headerClass: "mdi mdi-function biggerFont flexCenter",
                cellClass: "mdi mdi-function biggerFont  active-update-cell-custom"
            },
            {
                headerName: 'Code',
                minWidth: 200,
                field:'code',
            },
            {
                headerName: 'Description',
                minWidth: 200,
                field:'description',
            },
            {
                headerName: 'Model',
                minWidth: 200,
                field:'storeModel',
                valueGetter:params => params.data?.storeModel?.code,
                editable:false
            },
            {
                headerName: 'System function',
                minWidth: 200,
                field:'systemFunction',
                valueGetter:params => params.data?.systemFunction?.code,
                editable:false
            },
            {
                headerName: 'Requirements',
                collectionKey:'requirements',
                // collectionLock: 'actionFunction',
                valueGetter:params => params.data?.requirements?.length,
                cellClass: `active-cell-custom`
            },

        ]
    }

]
}