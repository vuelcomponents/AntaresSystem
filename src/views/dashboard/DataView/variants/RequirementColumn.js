import toLocaleStr from "@/utils/functions/toLocaleStr";
import moment from "moment";
const getStyle  = (params)=>{
    if(params.data.variant.variantType.id === 3){
        return {background:'rgba(194,176,104,0.23)'}
    }
    if(params.data.variant.variantType.id === 2){
        return {background:'rgba(107,194,104,0.23)'}
    }
    if(params.data.variant.variantType.id === 1){
        return {background:'rgba(150,104,194,0.23)'}
    }
}
export default () => {
    return [
        {
            headerCheckboxSelection: true,
            checkboxSelection: true,
            showDisabledCheckboxes: true,
            filter: false,
            width: 55,
            cellStyle:params=>getStyle(params),
        },
        {
            headerName:'Variant',
            field:'variant.code',
            editable:false,
            sort:'asc',
            cellStyle:params=>getStyle(params),
        },
        {
            headerName:"Comparator",
            field:"comparator",
            cellStyle:params=>getStyle(params),
            editable:false,
            valueGetter:params=>{
                if(params.data.variant.variantType.id === 3){
                    return 'N/A'
                }
                if(params.data.variant.variantType.id === 2){
                    return params.data?.comparator?.code
                }
                if(params.data.variant.variantType.id === 1){
                    return params.data?.comparator?.code
                }
            },
        },
        {
            headerName:'Value/VR',
            field:'variantRealisation.code',
            valueGetter:params=>{
              if(params.data.variant.variantType.id === 3){
                  return params.data.variantRealisation?.code
              }
              if(params.data.variant.variantType.id === 2){
                return moment(params.data.dateValue).format("DD/MM/YYYY")
              }
              if(params.data.variant.variantType.id === 1){
                return toLocaleStr(params.data.numericValue)
              }
            },
            cellStyle:params=>getStyle(params),
            editable:false,
        },

    ]
}
