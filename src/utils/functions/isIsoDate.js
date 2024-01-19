import moment from "moment";

const isoDateFormat = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d*)?$/;

function isIsoDateString(value) {
    return value && typeof value === "string" && isoDateFormat.test(value);
}
function handleDates(params, format, toApi){
    console.log('wykonane')
    if(typeof params === "object"){
        for(const [key, value] of Object.entries(params)){
            if (value instanceof Date) {
                if (moment(value).isValid()) {
                    params[key] = moment(value).format(format);
                } else {
                    params[key] = null;
                }
                continue;
            }
            if (isIsoDateString(value)) {
                if (moment(value, moment.ISO_8601).isValid()) {
                    params[key] = moment(value, moment.ISO_8601).format(format);
                } else {
                    params[key] = null;
                }
                continue;
            }
            if (/^\d{2}\/\d{2}\/\d{4}$/.test(value)) {
                const dateValue = moment(value,format);
                if (dateValue.isValid()) {
                    params[key] = dateValue.format(format);
                } else {
                    params[key] = null;
                }
                continue;
            }
            // if(key.toLocaleLowerCase().includes('date')){
            //     params[key] = moment(value, moment.ISO_8601).format(format)
            //     continue;
            // }
            if(value && typeof value === 'object' ){
                handleDates(value, format)
            }
        }
    }
}
export {isIsoDateString, handleDates}