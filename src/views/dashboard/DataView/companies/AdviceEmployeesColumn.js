import activeColumn from "@/utils/grid/activeColumn";
import {getThemeName} from "@/core/theme";

export default () => {
    return [

                {
                    headerName: 'First name',
                    field: 'employee.firstName',
                    minWidth: 200,

                },
                {
                    headerName: 'Last name',
                    field: 'employee.lastName',
                    minWidth: 200,
                },
                {
                    headerName: 'Employment',
                    valueGetter:params=>{
                        const l = params.data?.employee?.handledPositions
                        if(l>0) {
                            return `Hired, ${l} position${l>1 ? 's': ''}`
                        }
                        return 'Unemployed'
                    },
                    field:'employee.handledPositions',

                    minWidth: 200,
                },

    ]
}


